namespace OtpCode.Api.Repositories.Interfaces;

public interface IGenericSpcRepository
{
    Task Execute(string spcQuery, object param = null);
    Task<T1> Single<T1>(string spcQuery, object param = null);
    Task<List<T1>> List<T1>(string spcQuery, object param = null);
    IQueryable AsQueryable<T1>(string spcQuery, object param = null);
    Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> List<T1, T2>(string spcQuery, object param = null);
    Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> List<T1, T2, T3>(string spcQuery, object param = null);
}