using OtpCode.Api.Options;
using OtpCode.Api.Repositories.Interfaces;

namespace OtpCode.Api.Repositories.Providers;

public class OtpCodeRepository : GenericSpcRepository, IOtpCodeRepository
{
    private readonly DatabaseConnect _databaseConnect;

    public OtpCodeRepository(DatabaseConnect databaseConnect) : base(databaseConnect)
    {
        _databaseConnect = databaseConnect;
    }
}