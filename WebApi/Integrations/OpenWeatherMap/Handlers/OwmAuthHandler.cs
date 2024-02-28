using System.Web;
using Microsoft.Extensions.Options;
using WebApi.Models.Weather.Configuration;

namespace WebApi.Integrations.OpenWeatherMap.Handlers;

public class OwmAuthHandler : DelegatingHandler
{
    private readonly OwmConfiguration OwmConfiguration;

    public OwmAuthHandler(IOptions<OwmConfiguration> owmOptions)
    {
        OwmConfiguration = owmOptions.Value;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var uriBuilder = new UriBuilder(request.RequestUri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query.Add("appid", OwmConfiguration.ApiKey);
        uriBuilder.Query = query.ToString();
        request.RequestUri = uriBuilder.Uri;

        return base.SendAsync(request, cancellationToken);
    }
}