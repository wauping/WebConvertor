using System.Net;
using System.Xml.Linq;

namespace WebConvertor;

public class Parser
{
    public float ParseXML(string selectedDate, string charCode)
    {
        string url = $"https://www.cbr.ru/scripts/XML_daily.asp?date_req={selectedDate}";

        WebClient client = new();
        string xmlData = client.DownloadString(url);

        XDocument doc = XDocument.Parse(xmlData);

        string selectedCurrency = (
            from currency in doc.Descendants("Valute")
            where currency.Element("CharCode").Value == charCode
            select currency.Element("Value").Value
        ).FirstOrDefault();

        if (selectedCurrency != null)
        {
            return float.Parse(selectedCurrency);
        }
        else
        {
            return 0;
        }
    }
}
