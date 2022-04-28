using System.Text.Json;

var sourceLang = "EN";
var targetLang = "VI";

var searchValue = "Hello, world!";
var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sourceLang}&tl={targetLang}&dt=t&q={Uri.EscapeDataString(searchValue)}";
var client = new HttpClient();
var response = await client.GetAsync(url);
var result = await response.Content.ReadAsStringAsync();
var translatedText = JsonSerializer.Deserialize<List<dynamic>>(result)?[0][0][0];
Console.WriteLine(translatedText);
