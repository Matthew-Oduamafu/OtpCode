using System.Data;
using Dapper;
using OtpCode.Api.Options;
using OtpCode.Api.Repositories.Interfaces;

namespace OtpCode.Api.Repositories.Providers;

public class GenericSpcRepository : IGenericSpcRepository
{
    private readonly DatabaseConnect _databaseConnect;

    public GenericSpcRepository(DatabaseConnect databaseConnect)
    {
        _databaseConnect = databaseConnect;
    }

    public async Task Execute(string spcQuery, object param = null)
    {
        using (var con = _databaseConnect.CreateConnection())
        {
            await con.ExecuteAsync(spcQuery, param, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<T1> Single<T1>(string spcQuery, object param = null)
    {
        using (var con = _databaseConnect.CreateConnection())
        {
            return await con.QueryFirstOrDefaultAsync<T1>(spcQuery, param, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<List<T1>> List<T1>(string spcQuery, object param = null)
    {
        using (var con = _databaseConnect.CreateConnection())
        {
            return (await con.QueryAsync<T1>(spcQuery, param, commandType: CommandType.StoredProcedure)).ToList();
        }
    }

    public IQueryable AsQueryable<T1>(string spcQuery, object param = null)
    {
        using (var con = _databaseConnect.CreateConnection())
        {
            return con.Query<T1>(spcQuery, param, commandType: CommandType.StoredProcedure).AsQueryable();
        }
    }

    public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> List<T1, T2>(string spcQuery, object param = null)
    {
        using (var con = _databaseConnect.CreateConnection())
        {
            var multi = await con.QueryMultipleAsync(spcQuery, param, commandType: CommandType.StoredProcedure);
            var result1 = multi.Read<T1>().AsEnumerable();
            var result2 = multi.Read<T2>().AsEnumerable();
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(result1, result2);
        }
    }

    public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> List<T1, T2, T3>(string spcQuery,
        object param = null)
    {
        using (var con = _databaseConnect.CreateConnection())
        {
            var multi = await con.QueryMultipleAsync(spcQuery, param, commandType: CommandType.StoredProcedure);
            var result1 = multi.Read<T1>().AsEnumerable();
            var result2 = multi.Read<T2>().AsEnumerable();
            var result3 = multi.Read<T3>().AsEnumerable();
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(result1, result2, result3);
        }
    }
}