using System.IO;
using System.Windows;
using Newtonsoft.Json;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Colors;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using iText.Kernel.Geom;

namespace SchwimmbadNachhaltigkeit
{

    public partial class MainWindow : Window
    {
        private const string DatabaseFile = "data.json";
        private List<NachhaltigkeitsDaten> datenListe;
        //private const string ApiKey = ""; 
        private const string OpenAiEndpoint = "https://api.openai.com/v1/chat/completions";

        public MainWindow()
        {
            InitializeComponent();
            datenListe = LoadData();
            UpdateListView();
        }

        private List<NachhaltigkeitsDaten> LoadData()
        {
            if (File.Exists(DatabaseFile))
            {
                string json = File.ReadAllText(DatabaseFile);
                return JsonConvert.DeserializeObject<List<NachhaltigkeitsDaten>>(json) ?? new List<NachhaltigkeitsDaten>();
            }
            return new List<NachhaltigkeitsDaten>();
        }

        private async Task<string> GetAISuggestions()
        {
            string inputText = "Bitte geben Sie Verbesserungsvorschläge basierend auf den folgenden Daten: \n";
            foreach (var daten in datenListe)
            {
                inputText += $"Datum: {daten.Datum:dd.MM.yyyy}, Wasserverbrauch: {daten.Wasserverbrauch}, " +
                             $"Reinigungsmittel: {daten.Reinigungsmittel}, Solarenergie: {daten.SolarEnergie} kWh\n";
            }
            return await GetImprovementSuggestionsFromAI(inputText);
        }

        private async Task<string> GetImprovementSuggestionsFromAI(string inputText)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

                var requestBody = new
                {
                    model = "gpt-4",
                    messages = new[]
                    {
                        new { role = "system", content = "Gib mir Verbesserungsvorschläge basierend auf den folgenden Daten." },
                        new { role = "user", content = inputText }
                    }
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(OpenAiEndpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Fehler bei der Anfrage: {response.StatusCode} - {errorResponse}");
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);
                return jsonResponse.choices[0].message.content.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler bei der OpenAI Anfrage: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }

        private void SaveData()
        {
            string json = JsonConvert.SerializeObject(datenListe, Formatting.Indented);
            File.WriteAllText(DatabaseFile, json);
        }

        private void UpdateListView()
        {
            DataListView.ItemsSource = null;
            DataListView.ItemsSource = datenListe;
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WasserverbrauchInput.Text) ||
                string.IsNullOrWhiteSpace(ReinigungsmittelInput.Text) ||
                string.IsNullOrWhiteSpace(SolarEnergieInput.Text))
            {
                MessageBox.Show("Bitte alle Felder ausfüllen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(WasserverbrauchInput.Text, out double wasserverbrauch) ||
                !double.TryParse(SolarEnergieInput.Text, out double solarenergie))
            {
                MessageBox.Show("Wasserverbrauch und Solarenergie müssen Zahlen sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var daten = new NachhaltigkeitsDaten
            {
                Wasserverbrauch = wasserverbrauch,
                Reinigungsmittel = ReinigungsmittelInput.Text,
                SolarEnergie = solarenergie,
                Datum = DateTime.Now
            };

            datenListe.Add(daten);
            SaveData();
            UpdateListView();

            WasserverbrauchInput.Clear();
            ReinigungsmittelInput.Clear();
            SolarEnergieInput.Clear();
        }

        private async void ExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pdfPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"Nachhaltigkeitsbericht_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
                using var writer = new PdfWriter(pdfPath);
                using var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                var exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var fontPath = System.IO.Path.Combine(exeDirectory, "fonts", "Roboto-Regular.ttf");

                if (!File.Exists(fontPath))
                {
                    MessageBox.Show($"Die Schriftart-Datei 'Roboto-Regular.ttf' wurde nicht gefunden.\nGesuchter Pfad: {fontPath}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var bodyFont = PdfFontFactory.CreateFont(fontPath, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                var boldFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(exeDirectory, "fonts", "Roboto-Bold.ttf"), PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);

                pdf.SetDefaultPageSize(PageSize.A4);
                document.SetMargins(20, 20, 20, 20);
                var headerBackgroundColor = new DeviceRgb(40, 40, 40);
                var footerBackgroundColor = new DeviceRgb(60, 60, 60);

                var header = new Paragraph("Nachhaltigkeitsbericht")
                    .SetFont(boldFont)
                    .SetFontSize(22)
                    .SetFontColor(ColorConstants.WHITE)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetBackgroundColor(headerBackgroundColor)
                    .SetMarginBottom(10);
                document.Add(header);

                var info = new Paragraph($"Erstellt am: {DateTime.Now:dd.MM.yyyy} von {Environment.UserName}")
                    .SetFont(bodyFont)
                    .SetFontSize(10)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetFontColor(ColorConstants.GRAY);
                document.Add(info);

                var table = new Table(new float[] { 3, 4, 3, 3 }).UseAllAvailableWidth()
                    .SetBackgroundColor(new DeviceRgb(245, 245, 245));

                table.AddHeaderCell(new Cell().Add(new Paragraph("Datum").SetFont(boldFont).SetFontColor(ColorConstants.WHITE)).SetBackgroundColor(headerBackgroundColor));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Wasserverbrauch (Liter)").SetFont(boldFont).SetFontColor(ColorConstants.WHITE)).SetBackgroundColor(headerBackgroundColor));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Reinigungsmittel").SetFont(boldFont).SetFontColor(ColorConstants.WHITE)).SetBackgroundColor(headerBackgroundColor));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Solarenergie (kWh)").SetFont(boldFont).SetFontColor(ColorConstants.WHITE)).SetBackgroundColor(headerBackgroundColor));

                // Tabelleninhalt
                foreach (var daten in datenListe)
                {
                    table.AddCell(new Paragraph(daten.Datum.ToString("dd.MM.yyyy")).SetFont(bodyFont));
                    table.AddCell(new Paragraph($"{daten.Wasserverbrauch}").SetFont(bodyFont));
                    table.AddCell(new Paragraph(daten.Reinigungsmittel).SetFont(bodyFont));
                    table.AddCell(new Paragraph($"{daten.SolarEnergie}").SetFont(bodyFont));
                }
                document.Add(table.SetMarginBottom(20));

                if (AiSuggestionCheckBox.IsChecked == true)
                {
                    var aiSuggestions = await GetAISuggestions();
                    var aiHeader = new Paragraph("AI-generierte Verbesserungsvorschläge")
                        .SetFont(boldFont)
                        .SetFontSize(14)
                        .SetFontColor(new DeviceRgb(255, 140, 0));
                    document.Add(aiHeader);

                    if (!string.IsNullOrEmpty(aiSuggestions))
                    {
                        var suggestionsText = new Paragraph(aiSuggestions)
                            .SetFont(bodyFont)
                            .SetFontSize(12)
                            .SetFontColor(ColorConstants.BLACK);
                        document.Add(suggestionsText);
                    }
                    else
                    {
                        document.Add(new Paragraph("Fehler beim Abrufen der AI-Vorschläge.")
                            .SetFont(bodyFont)
                            .SetFontSize(12)
                            .SetFontColor(ColorConstants.RED));
                    }
                }

                var footer = new Paragraph("Dieser Bericht ist vertraulich. Bitte teilen Sie ihn nur mit autorisierten Personen.")
                    .SetFont(bodyFont)
                    .SetFontSize(8)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontColor(ColorConstants.GRAY)
                    .SetBackgroundColor(footerBackgroundColor);
                document.Add(footer);

                MessageBox.Show($"Bericht erfolgreich erstellt: {pdfPath}", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Erstellen der PDF: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteData_Click(object sender, RoutedEventArgs e)
        {
            if (DataListView.SelectedItem != null)
            {
                var selectedData = (NachhaltigkeitsDaten)DataListView.SelectedItem;
                datenListe.Remove(selectedData);
                SaveData();
                UpdateListView();
                MessageBox.Show("Datensatz erfolgreich gelöscht.", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie einen Datensatz aus, den Sie löschen möchten.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public class NachhaltigkeitsDaten
    {
        public double Wasserverbrauch { get; set; }
        public string Reinigungsmittel { get; set; }
        public double SolarEnergie { get; set; }
        public DateTime Datum { get; set; }
    }
}
