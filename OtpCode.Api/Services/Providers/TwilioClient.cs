using Microsoft.Extensions.Options;
using OtpCode.Api.Options;
using Twilio.Clients;
using Twilio.Http;
using HttpClient = Twilio.Http.HttpClient;

namespace OtpCode.Api.Services.Providers;

public class TwilioClient : ITwilioRestClient
{
    private readonly ILogger<TwilioClient> _logger;
    private readonly TwilioConfig _twilioConfig;
    private readonly ITwilioRestClient _twilioRestClient;

    public TwilioClient(ILogger<TwilioClient> logger,
        IOptions<TwilioConfig> options, System.Net.Http.HttpClient httpClient)
    {
        _logger = logger;
        _twilioConfig = options.Value;
        _twilioRestClient = new TwilioRestClient(_twilioConfig.AccountSID, _twilioConfig.AuthToken,
            httpClient: new SystemNetHttpClient(httpClient));
    }

    public Response Request(Request request)
    {
        return _twilioRestClient.Request(request);
    }

    public async Task<Response> RequestAsync(Request request)
    {
        return await _twilioRestClient.RequestAsync(request);
    }

    public string AccountSid => _twilioRestClient.AccountSid;
    public string Region => _twilioRestClient.Region;
    public HttpClient HttpClient => _twilioRestClient.HttpClient;
}