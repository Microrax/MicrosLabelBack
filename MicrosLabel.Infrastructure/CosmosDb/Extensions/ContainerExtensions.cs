using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Infrastructure.CosmosDb.Extensions
{
    public static class ContainerExtensions
    {
        public static async Task<T?> QuerySingleOrDefaultAsync<T>(this Container container, QueryDefinition query)
        {
            using var feed = container.GetItemQueryIterator<T>(query);
            var result = default(T);

            while (feed.HasMoreResults)
            {
                var response = await feed.ReadNextAsync();
                result = response.SingleOrDefault();
            }

            return result;
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(this Container container, QueryDefinition query)
        {
            using var feed = container.GetItemQueryIterator<T>(query);
            var result = new List<T>();

            while (feed.HasMoreResults)
            {
                var response = await feed.ReadNextAsync();
                result.AddRange(response.AsEnumerable());
            }

            return result;
        }
    }
}
