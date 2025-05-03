using AutoMapper;
using MicrosLabel.Domain.Aggregates;
using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Domain.Events;
using MicrosLabel.Infrastructure.Configurations;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Linq.Expressions;

namespace MicrosLabel.Infrastructure.CosmosDb;

public sealed class CosmosDbRepository<TAggregate, TEntity> where TAggregate : Aggregate where TEntity : BaseEntity
{
    private readonly Database CosmosDb;
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public CosmosDbRepository(Database cosmosDb, IEventService eventService)
    {
        CosmosDb = cosmosDb ?? throw new ArgumentNullException(nameof(CosmosDb));
        _eventService = eventService;
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AllowNullCollections = true;

            cfg.CreateMap<LabelConfigurationEntity, LabelConfigurations>();
            cfg.CreateMap<LabelConfigurations, LabelConfigurationEntity>();

            cfg.CreateMap<LabelsEntity, LabelsModel>();
            cfg.CreateMap<LabelsModel, LabelsEntity>();

            cfg.CreateMap<ClientsEntity, ClientsModel> ();
            cfg.CreateMap<ClientsModel, ClientsEntity>();
        }));
    }

    private async Task<Container> ContainerAsync(string containerName, string partitionKeyName)
    {
        return await CosmosDb.CreateContainerIfNotExistsAsync(containerName, $"/{partitionKeyName}");
    }

    public async Task<TAggregate> AddItemAsync(TAggregate item)
    {
        item.DomainEvents.ToList().ForEach(_eventService.AddEvent);
        item.DomainEvents.Clear();

        var entity = _mapper.Map<TEntity>(item);

        var container = await ContainerAsync(entity.ContainerName, entity.PartitionKeyName);
        TEntity result = await container.CreateItemAsync(entity, entity.PartitionKey);
        return _mapper.Map<TAggregate>(result);
    }

    public async Task UpdateItemAsync(TAggregate item)
    {
        item.DomainEvents.ToList().ForEach(_eventService.AddEvent);
        item.DomainEvents.Clear();

        var entity = _mapper.Map<TEntity>(item);
        var container = await ContainerAsync(entity.ContainerName, entity.PartitionKeyName);
        await container.UpsertItemAsync(entity, entity.PartitionKey);
    }

    public async Task DeleteItemAsync(TAggregate item)
    {
        item.DomainEvents.ToList().ForEach(_eventService.AddEvent);
        item.DomainEvents.Clear();

        var entity = _mapper.Map<TEntity>(item);
        var container = await ContainerAsync(entity.ContainerName, entity.PartitionKeyName);
        await container.DeleteItemAsync<TEntity>(entity.Id, entity.PartitionKey);
    }

    public async Task<TAggregate?> GetItemAsync(string containerName, string partitionKeyName, Expression<Func<TEntity, bool>> filter)
    {
        var container = await ContainerAsync(containerName, partitionKeyName);
        return _mapper.Map<TAggregate>(container.GetItemLinqQueryable<TEntity>(true)
            .Where(filter)
            .AsEnumerable()
            .FirstOrDefault());
    }
    public async Task<IEnumerable<TAggregate>> GetAllItemsAsync(string containerName, string partitionKeyName)
    {
        var container = await ContainerAsync(containerName, partitionKeyName);
        var query = container.GetItemLinqQueryable<TEntity>();
        var results = await FilterAsync(query);
        return results.Select(_mapper.Map<TAggregate>);
    }

    public async Task<IEnumerable<TAggregate>> AllFilteredAsync(string containerName, string partitionKeyName, Expression<Func<TEntity, bool>> filter)
    {
        var container = await ContainerAsync(containerName, partitionKeyName);
        var query = container.GetItemLinqQueryable<TEntity>().Where(filter);
        var results = await FilterAsync(query);
        return results.Select(_mapper.Map<TAggregate>);
    }

    private static async Task<IReadOnlyCollection<TEntity>> FilterAsync(IQueryable<TEntity> query)
    {
        var iterator = query.ToFeedIterator();

        var items = new List<TEntity>();

        while (iterator.HasMoreResults)
        {
            var currentResultSet = await iterator.ReadNextAsync();
            foreach (var item in currentResultSet)
            {
                items.Add(item);
            }
        }

        return items;
    }
}
