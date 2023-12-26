using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
using System.Globalization;

namespace WebConvertor
{
    public partial class MainPage : ContentPage
    {

        private Picker pickerFrom;
        private Picker pickerTo;

        private Entry entryFromValue;
        private Entry entryToValue;

        private ExchangeRatesService exchangeRatesService;

        public MainPage()
        {
            InitializeComponent();

            pickerFrom = PickerFrom;
            pickerTo = PickerTo;

            entryFromValue = EntryFrom;
            entryToValue = EntryTo;

            exchangeRatesService = new ExchangeRatesService();

        }

        private void OnPickerFromSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedFromCurrency = pickerFrom.SelectedItem as string;

            if (selectedFromCurrency != null)
            {
                Debug.WriteLine("Selected Currency From: " + selectedFromCurrency);
            }
        }

        private void OnPickerToSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedToCurrency = pickerTo.SelectedItem as string;

            if (selectedToCurrency != null)
            {
                Debug.WriteLine("Selected Currency To: " + selectedToCurrency);
            }
        }

        private void OnEntryFromValueChanged(object sender, TextChangedEventArgs e)
        {
            var enteredFromValue = entryFromValue.Text;

            if (!string.IsNullOrEmpty(enteredFromValue))
            {
                Debug.WriteLine("Entered Value From: " + enteredFromValue);
            }
        }

        private void OnEntryToValueChanged(object sender, TextChangedEventArgs e)
        {
            var enteredToValue = entryToValue.Text;

            if (!string.IsNullOrEmpty(enteredToValue))
            {
                Debug.WriteLine("Entered Value To: " + enteredToValue);
            }
        }

        private async void OnGetExchangeRatesClicked(object sender, EventArgs e)
        {
            // Получаем выбранную дату из DatePicker
            var selectedDate = DateSelector.Date;
            var formattedDate = selectedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Запрашиваем курсы валют на указанную дату
            var exchangeRates = await exchangeRatesService.GetExchangeRatesOnDateAsync(formattedDate);

            if (exchangeRates != null)
            {
                // Обновляем метки с данными об обменных курсах
                DateLabel.Text = "Date: " + exchangeRates.Date;
                UsdValueLabel.Text = "USD Value: " + exchangeRates.Valute.USD.Value;
                // Другие действия с данными...
            }
        }
    }
}
