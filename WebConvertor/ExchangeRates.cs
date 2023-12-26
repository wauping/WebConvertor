using Flurl;
using Flurl.Http;
using System.Diagnostics;

namespace WebConvertor;

public class ExchangeRatesService
{
    public async Task<Root> GetExchangeRatesOnDateAsync(string selectedDate)
    {
        try
        {
            var url = $"https://www.cbr-xml-daily.ru/archive/{selectedDate}/daily_json.js";
            var exchangeRates = await url.GetJsonAsync<Root>();
            Debug.WriteLine(exchangeRates);
            return exchangeRates;
            
        }
        catch (FlurlHttpException ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }
}
