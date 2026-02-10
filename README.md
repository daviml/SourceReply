# Source Reply

**Source Reply** (formerly CodeAware Triage) is an AI-assisted triage system built with .NET 10 and Blazor.

The main goal is to accelerate ticket and issue analysis using the Gemini model to process descriptions and correlate them with relevant code snippets (RAG - Retrieval-Augmented Generation).

## Key Features

- **Ticket Import**: Load Excel files (`.xlsx`) containing the list of tickets.
- **Code Context (RAG)**: Upload code files (`.cs`, `.js`, etc.) so the AI has system context when analyzing issues.
- **AI Analysis**: The system uses the Google Gemini API to suggest a classification (Bug, Infra, Question) and a possible solution.
- **Multi-Platform**: 
  - **Desktop**: Native Windows application via WPF (WebView2).
  - **Web**: Blazor WebAssembly application running in the browser.
- **Internationalized UI**: Fully English UI for standardization.

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (Preview or final)
- A Google Gemini API Key (AI Studio).

## Configuration

Before running, you need to configure your API key.

1. Navigate to the project folder you want to run (e.g., `CodeAwareTriage.Wpf` or `CodeAwareTriage.Client`).
2. Create or edit the `appsettings.json` file.
3. Add your key:
   ```json
   {
     "GEMINI_API_KEY": "YOUR_API_KEY_HERE"
   }
   ```
   > **Note**: For the WebAssembly project (`Client`), the file must be in `wwwroot/appsettings.json`.

## How to Run

### Desktop Version (Recommended)
To run the application as a native Windows program:

```bash
dotnet run --project CodeAwareTriage.Wpf
```

### Web Version
To run in the browser:

```bash
dotnet run --project CodeAwareTriage.Client
```
The terminal will display the local URL (usually `http://localhost:5xxx`).

## Project Structure

- **CodeAwareTriage.UI**: Shared components library (Razor Class Library) containing all interface and presentation logic.
- **CodeAwareTriage.Wpf**: Desktop Host (WPF) that consumes the shared UI.
- **CodeAwareTriage.Client**: WebAssembly Host (Browser) that consumes the shared UI.
- **CodeAwareTriage.Core**: Core business logic and interfaces.
