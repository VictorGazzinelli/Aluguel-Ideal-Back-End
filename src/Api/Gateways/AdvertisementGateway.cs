using AluguelIdeal.Api.Database;
using AluguelIdeal.Api.Entities;
using AluguelIdeal.Api.Gateways.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Gateways
{
    public class AdvertisementGateway : BaseRepository, IAdvertisementGateway
    {
        public AdvertisementGateway(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory, "db-aluguel-ideal-dev")
        {
        }

        public async Task<int> InsertAsync(Advertisement advertisement)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Title", advertisement.Title, DbType.String);

            string command = @"
                INSERT INTO advertisement(id, title)
                VALUES (DEFAULT, @Title)
            ";

            return await ExecuteCommandAsync(command, dynamicParameters);
        }

        public async Task<IEnumerable<Advertisement>> GetAllAsync(CancellationToken cancellationToken)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Title", "Hello World", DbType.String);

            string command = @"
                INSERT INTO ""Advertisement"" (id, title)
                VALUES (DEFAULT, @Title)
            ";

            int result1 =  await ExecuteInsertCommandReturningIdAsync(command, dynamicParameters);

            IEnumerable<Advertisement> result = new Advertisement[] {
                new Advertisement()
                {
                    Id = 1,
                    Title = "Title1",
                },
                new Advertisement()
                {
                    Id = 2,
                    Title = "Title2",
                }
            };
            return result;
        }
    }
}
