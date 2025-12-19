# CodeAware Triage System

An AI-assisted triage system built with .NET 10 Blazor WebAssembly.

## Overview

This project uses Blazor WebAssembly to provide a client-side interface for triaging requests, powered by AI semantic analysis.

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Visual Studio 2022 (Preview) or VS Code

## Getting Started

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/daviml/CodeAware-Triage-System.git
    cd CodeAware-Triage-System
    ```

2.  **Configuration:**
    - The project requires a Gemini API Key.
    - Navigate to `CodeAwareTriage.Client/wwwroot/`.
    - Create a copy of `appsettings.json` and name it `appsettings.Development.json` (or just edit `appsettings.json` if running locally and strictly not committing it, though `appsettings.Development.json` is preferred and git-ignored).
    - Add your API key:
      ```json
      {
        "GEMINI_API_KEY": "YOUR_ACTUAL_API_KEY_HERE"
      }
      ```

3.  **Run the application:**
    Navigate to the client project and run:
    ```bash
    dotnet run --project CodeAwareTriage.Client
    ```

4.  **Access:**
    Open your browser to the URL shown in the console (typically `http://localhost:5272` or similar).

## Project Structure

- **CodeAwareTriage.Client**: The Blazor WebAssembly UI application.
- **Core**: Shared business logic and interfaces.
- **UI**: Shared UI components (RCL).
