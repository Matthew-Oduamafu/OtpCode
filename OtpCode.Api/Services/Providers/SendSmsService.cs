using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OtpCode.Api.Options;
using OtpCode.Api.Services.Interfaces;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;

namespace OtpCode.Api.Services.Providers;

public class SendSmsService : ISendSmsService
{
    private readonly ILogger<SendSmsService> _logger;
    private readonly TwilioConfig _twilioConfig;
    private readonly ITwilioRestClient _twilioRestClient;

    public SendSmsService(ILogger<SendSmsService> logger, ITwilioRestClient twilioRestClient,
        IOptions<TwilioConfig> options)
    {
        _logger = logger;
        _twilioRestClient = twilioRestClient;
        _twilioConfig = options.Value;
    }

    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        try
        {
            _logger.LogDebug("Sending sms to {PhoneNumber}", phoneNumber);

            var twilioMessage = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber(_twilioConfig.TwilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber),
                client: _twilioRestClient);

            _logger.LogDebug("Sms sent to {PhoneNumber}", phoneNumber);
            _logger.LogDebug("twilioMessage {@TwilioMessage}", JsonConvert.SerializeObject(twilioMessage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending sms to {PhoneNumber}", phoneNumber);
            throw;
        }
    }
}