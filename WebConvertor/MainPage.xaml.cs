using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace WebConvertor
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _convertedValue;
        public string ConvertedValue
        {
            get { return _convertedValue; }
            set
            {
                _convertedValue = value;
                OnPropertyChanged("ConvertedValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



    public partial class MainPage : ContentPage
    {

        private Picker pickerFrom;
        private Picker pickerTo;

        private Entry entryFromValue;

        private DateTime selectedDate;

        private Parser xmlFileParser;

        public MainPage()
        {
            InitializeComponent();

            BindingContext  = new ViewModel();
            xmlFileParser   = new Parser();

            pickerFrom      = PickerFrom;
            pickerTo        = PickerTo;

            entryFromValue  = EntryFrom;
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

        private void OnGetExchangeRatesClicked(object sender, EventArgs e)
        {
            // Получаем выбранную дату из DatePicker
            selectedDate = DateSelector.Date;
            string formattedDate = selectedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Запрашиваем курсы валют на указанную дату
            if(pickerFrom.SelectedItem != null &&  pickerTo.SelectedItem != null)
            {
                var currencyRateFrom    = xmlFileParser.ParseXML(formattedDate, pickerFrom.SelectedItem.ToString());
                var currencyRateTo      = xmlFileParser.ParseXML(formattedDate, pickerTo.SelectedItem.ToString());

                 if (currencyRateFrom != 0 && currencyRateTo != 0)
                 {
                    var newValue = float.Parse(entryFromValue.Text) * currencyRateFrom / currencyRateTo;

                    var viewModel = (ViewModel)BindingContext;
                    viewModel.ConvertedValue = newValue.ToString();
                    OnPropertyChanged("ConvertedValue");
                 }                      
            }
        }
    }
}
