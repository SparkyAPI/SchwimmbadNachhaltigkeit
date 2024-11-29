
# SchwimmbadNachhaltigkeit 🌍🏊‍♂️

## Übersicht

Dieses Projekt dient der Verwaltung von Nachhaltigkeitsdaten eines Schwimmbads, mit einer Funktion zur Analyse und Verbesserung der Nachhaltigkeit basierend auf historischen Daten. Es enthält eine WPF-Anwendung, die es ermöglicht, Daten zu erfassen (wie Wasserverbrauch, verwendete Reinigungsmittel und Solarenergie), diese zu speichern und in einem PDF-Bericht zusammenzufassen. Optional werden Verbesserungsvorschläge durch OpenAI generiert. 💡📊

## Funktionen 🚀

- **Daten erfassen**: Erfassung von Wasserverbrauch, Reinigungsmitteln und Solarenergie.
- **PDF-Export**: Generiert einen detaillierten PDF-Bericht, der alle Daten enthält.
- **OpenAI-Integration**: Generiert Verbesserungsvorschläge basierend auf den erfassten Daten.
- **Daten speichern**: Die Daten werden lokal im JSON-Format gespeichert.
- **Datenverwaltung**: Daten können hinzugefügt oder gelöscht werden.

## Systemanforderungen ⚙️

- .NET 6 oder höher
- Visual Studio 2022 oder höher
- NuGet-Pakete:
  - `Newtonsoft.Json`
  - `iText7`

## Setup und Installation 🛠️

### 1. Voraussetzungen 📋

Stelle sicher, dass du die folgenden Programme installiert hast:

- [Visual Studio](https://visualstudio.microsoft.com/)
- .NET 6 oder höher
- Die folgenden NuGet-Pakete:

```bash
Install-Package Newtonsoft.Json
Install-Package itext7
Install-Package MySQL.Data
```

### 2. Projekt herunterladen 💻

Clone das Repository oder lade den Quellcode als ZIP herunter:

```bash
git clone https://github.com/dein-username/SchwimmbadNachhaltigkeit.git
```

### 3. API-Schlüssel 🔑

Für die OpenAI-Integration benötigst du einen API-Schlüssel. Registriere dich auf [OpenAI](https://beta.openai.com/signup/) und erhalte deinen Schlüssel. Setze ihn dann in der Konstante `ApiKey` in der Datei `MainWindow.xaml.cs` ein:

```csharp
private const string ApiKey = "dein-openai-api-key-hier";
```

### 4. Anwendung ausführen ▶️

Öffne das Projekt in Visual Studio und starte die Anwendung. Sie wird die Benutzeroberfläche mit den verschiedenen Funktionen anzeigen.

## Funktionen im Detail 📑

### Daten erfassen 📝

Die Anwendung ermöglicht das Erfassen von Daten wie:

- **Wasserverbrauch (in Litern)**: Erfassung des Wasserverbrauchs im Schwimmbad.
- **Reinigungsmittel**: Spezifikation der verwendeten Reinigungsmittel.
- **Solarenergie (in kWh)**: Erfassung der Menge an Solarenergie, die verwendet wird.

### PDF-Bericht 📃

Ein detaillierter PDF-Bericht wird erstellt, der die gesammelten Daten sowie optional AI-generierte Verbesserungsvorschläge enthält. Der Bericht wird im Standardordner "Dokumente" unter dem Namen `Nachhaltigkeitsbericht_yyyyMMdd_HHmmss.pdf` gespeichert.

### OpenAI-Integration 🤖

Basierend auf den gesammelten Daten gibt die Anwendung Verbesserungsvorschläge zur Verbesserung der Nachhaltigkeit. Diese Vorschläge werden mit Hilfe der OpenAI GPT-4 API generiert.

### Datenverwaltung 🗂️

- **Daten hinzufügen**: Manuelles Hinzufügen von Datensätzen.
- **Daten löschen**: Löschen von Datensätzen aus der Anzeige und der gespeicherten Datei.

### Ladeanzeige und Benutzeroberfläche ⏳

Während der Anfrage an OpenAI wird eine Ladeanzeige eingeblendet, die dem Benutzer anzeigt, dass der Prozess läuft. Während dieser Zeit sind die Schaltflächen deaktiviert, um doppelte Anfragen zu verhindern.

## Wichtige Klassen 🏗️

### MainWindow.xaml.cs

Die Hauptlogik der Anwendung befindet sich in der `MainWindow.xaml.cs`. Diese Klasse verwaltet die Benutzeroberfläche, verarbeitet Daten und stellt die Verbindung zu OpenAI her. Zu den wichtigsten Methoden gehören:

- `GetAISuggestions()`: Holt die Verbesserungsvorschläge von der OpenAI API.
- `SaveData()`: Speichert die Daten im JSON-Format.
- `ExportToPDF_Click()`: Exportiert die gesammelten Daten als PDF.
- `ShowLoadingIndicator()` und `HideLoadingIndicator()`: Steuern die Anzeige des Ladeindikators.

### NachhaltigkeitsDaten

Die Klasse `NachhaltigkeitsDaten` speichert die erfassten Informationen über den Wasserverbrauch, die verwendeten Reinigungsmittel und die Solarenergie.

```csharp
public class NachhaltigkeitsDaten
{
    public double Wasserverbrauch { get; set; }
    public string Reinigungsmittel { get; set; }
    public double SolarEnergie { get; set; }
    public DateTime Datum { get; set; }
}
```

## Lizenz 📜

Dieses Projekt steht unter der MIT-Lizenz. Siehe [LICENSE](LICENSE) für Details.

---

Danke, dass du dieses Projekt nutzt! 🌿
