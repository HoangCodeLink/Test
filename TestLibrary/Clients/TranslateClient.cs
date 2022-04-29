using Microsoft.Extensions.Configuration;

namespace TestLibrary.Clients;

public class TranslateClient
{
    private readonly string _url;
    private readonly IHttpClientFactory _clientFactory;

    public TranslateClient(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _url = configuration.GetSection("URLs:Translate").Value;
    }

    /// <summary>
    /// Translate the input text from a language to another
    /// </summary>
    /// <param name="from">Source Language</param>
    /// <param name="to">Target Language</param>
    /// <param name="text">Text to be translated</param>
    /// <returns>Translated text</returns>
    public async Task<string> Translate(string from, string to, string text)
    {
        var client  = _clientFactory.CreateClient();
        client.BaseAddress = new Uri(_url);

        using var response = await client.GetAsync($"single?client=gtx&sl={from}&tl={to}&dt=t&q={text}");
        
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsAsync<List<dynamic>>();
            var result = res?[0][0][0] ?? "";
            return result;
        }

        return "";
    }
}