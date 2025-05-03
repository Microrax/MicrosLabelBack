using Dapper;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Domain.Aggregates.Prints;
using MicrosLabel.Infrastructure.Sql;
using MicrosLabel.Reads.Queries;
using Microsoft.Azure.Cosmos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http.Headers;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Infrastructure.Repositories.QueryRepositories
{
    public class LabelPrintQueryRepository : ILabelPrintQueryRepository
    {
        private readonly DbConnectionFactory _sqlDb;

        public LabelPrintQueryRepository(DbConnectionFactory dbConnectionFactory)
        {
            _sqlDb = dbConnectionFactory;
           
        }

        public async Task<string?> GetByIdAsync(string clientCode, string sku, string zpl, string sql)
        {

            sql = sql.Replace("@ClientCode", clientCode);
            sql = sql.Replace("@Sku", sku);

            var conn = _sqlDb.CreateConnection();
            var res = conn.Query(sql);

            string sql2 = "";

            foreach (object item in res)
            {
                sql2 = sql2 + item;
            }

            

            string[] discrimnatorSql = { ".", "", ",", "", "", "", "DapperRow", "=", "'", "{{", "}}", "}", "{" };
            string[] discrimnatorSql2 = { ".", " ", ",", " ", " ", "", "DapperRow", "=", "'", "{{", "}}", "}", "{", " ", " ", " " };
            string[] columnSql = sql2.Split("FROM").First().Split(discrimnatorSql, StringSplitOptions.None);
            IDictionary<string, string> columns = new Dictionary<string, string>();
            int cont = 0;

            sql2 = sql2.Replace("@ClientCode", "'" + clientCode + "'");
            sql2 = sql2.Replace("@Sku", "'" + sku + "'");
            var excpt = columnSql.Where(e => !discrimnatorSql.Any(f => f == e));
            var excpt2 = columnSql.Where(e => !discrimnatorSql2.Any(f => f == e));

            string auxword = "";
            foreach (string word in excpt2)
            {
                if (cont % 2 == 0)
                {
                    columns.Add(word.TrimStart().TrimEnd(), "");
                    auxword = word.TrimStart().TrimEnd();
                }
                else
                {
                    columns[auxword]= word.TrimStart().TrimEnd();
                }
                cont++;
            }

            string newstr = columns.Aggregate(zpl, (current, value) => current.Replace("{{" + value.Key + "}}", value.Value));
            return newstr;
        }
    }
}
