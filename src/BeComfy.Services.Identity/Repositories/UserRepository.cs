using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BeComfy.Common.MSSQL;
using BeComfy.Services.Identity.Domain;
using Dapper;

namespace BeComfy.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly ISqlConnector _sqlConnector;

        public UserRepository(ISqlConnector sqlConnector)
        {
            _sqlConnector = sqlConnector;
        }
        
        public async Task AddAsync(User user)
        {
            using (var connection = _sqlConnector.CreateConnection())
            {
                connection.Open();

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", user.Id);
                queryParameters.Add("@Firstname", user.Firstname);
                queryParameters.Add("@Secondname", user.Secondname);
                queryParameters.Add("@Surname", user.Surname);
                queryParameters.Add("@Role", user.Role);
                queryParameters.Add("@Email", user.Email);
                queryParameters.Add("@Password", user.Password);
                queryParameters.Add("@CreatedAt", user.CreatedAt);
                queryParameters.Add("@UpdatedAt", user.UpdatedAt);

                await connection.ExecuteAsync("CreateUser", queryParameters, 
                    commandType: CommandType.StoredProcedure);

                connection.Close();
            }
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string email)
        {
            // TODO : DO NOT USE QUERY, USE TRY CATCH FOR CONNECTION EXCEPTIONS

            var sql = $"SELECT * FROM [Users].[dbo].[Identities] WHERE Email = '{email}'";
            using (var connection = _sqlConnector.CreateConnection())
            {
                var person = await connection.QueryAsync<User>(sql);
                return person.SingleOrDefault();
            }
        }

        public Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}