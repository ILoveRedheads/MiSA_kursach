using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace LocalityGuiClient;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly HttpClient _httpClient = new HttpClient();

    public ObservableCollection<LocalityDto> Localities { get; } = new ObservableCollection<LocalityDto>();

    public MainWindow()
    {
        InitializeComponent();
        LocalitiesGrid.ItemsSource = Localities;
        LocalitiesGrid.SelectionChanged += LocalitiesGrid_OnSelectionChanged;
    }

    private async void LoadButton_OnClick(object sender, RoutedEventArgs e)
    {
        await LoadLocalitiesAsync();
    }

    private async Task LoadLocalitiesAsync()
    {
        try
        {
            StatusText.Text = "Загрузка...";

            var baseUrl = BaseUrlTextBox.Text.Trim().TrimEnd('/');
            var requestUrl = $"{baseUrl}/api/GetLocalities";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                StatusText.Text = $"Ошибка: {(int)response.StatusCode}";
                MessageBox.Show($"Не удалось получить данные. Код: {(int)response.StatusCode}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<LocalityDto[]>(json) ?? Array.Empty<LocalityDto>();

            Localities.Clear();
            foreach (var item in items)
            {
                Localities.Add(item);
            }

            StatusText.Text = $"Загружено записей: {Localities.Count}";
            if (Localities.Count > 0)
            {
                LocalitiesGrid.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            StatusText.Text = "Ошибка загрузки";
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void LocalitiesGrid_OnSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (LocalitiesGrid.SelectedItem is LocalityDto loc)
        {
            NameText.Text = loc.Name;
            TypeText.Text = loc.Type;
            ResidantsText.Text = loc.NumberResidantsTh.ToString("F2");
            BudgetText.Text = loc.BudgetMlrd.ToString("F2");
            SquareText.Text = loc.SquareKm.ToString("F2");
            MayorText.Text = loc.Mayor;
        }
        else
        {
            NameText.Text = string.Empty;
            TypeText.Text = string.Empty;
            ResidantsText.Text = string.Empty;
            BudgetText.Text = string.Empty;
            SquareText.Text = string.Empty;
            MayorText.Text = string.Empty;
        }
    }

    public class LocalityDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double NumberResidantsTh { get; set; }
        public double BudgetMlrd { get; set; }
        public double SquareKm { get; set; }
        public string Mayor { get; set; }
    }
}