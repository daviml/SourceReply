using CodeAwareTriage.UI.Models;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace CodeAwareTriage.UI.Services;

public class GeminiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private const string SYSTEM_INSTRUCTION = @"Você é um Agente de Suporte Técnico N1 especializado em análise de código.
Analise o chamado do usuário e os trechos de código fornecidos como contexto (RAG).
Sua resposta deve seguir estritamente o formato JSON solicitado.

Regras de Negócio:
1. Identifique se o erro é lógico (Bug), infraestrutura (Infra) ou dúvida.
2. Cite o nome do arquivo analisado se encontrar algo relevante.
3. Dê um veredito curto e técnico.";

    public GeminiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<(string Verdict, string Type)> AnalyzeTicketAsync(string ticketDescription, List<CodeFile> contextFiles)
    {
        var apiKey = _configuration["GEMINI_API_KEY"];
        if (string.IsNullOrEmpty(apiKey))
        {
            return ("API Key não encontrada. Configure em appsettings.json.", "Error");
        }

        var contextText = string.Join("\n\n---\n\n", contextFiles.Select(f => $"FILE: {f.Name}\nCONTENT:\n{f.Content.Substring(0, Math.Min(f.Content.Length, 1500))}"));
        var prompt = $@"
    DESCRIÇÃO DO PROBLEMA:
    {ticketDescription}

    CONTEXTO DE CÓDIGO FONTE RELEVANTE:
    {contextText}
";
        
        // This schema construction is simplified for the REST call
        var requestBody = new
        {
            contents = new[] {
                new { parts = new[] { new { text = prompt } } }
            },
            systemInstruction = new { parts = new[] { new { text = SYSTEM_INSTRUCTION } } },
            generationConfig = new {
                responseMimeType = "application/json",
                responseSchema = new {
                    type = "OBJECT",
                    properties = new {
                        verdict = new { type = "STRING", description = "O veredito técnico curto." },
                        type = new { type = "STRING", @enum = new[] { "Bug", "Infra", "Dúvida" } }
                    },
                    required = new[] { "verdict", "type" }
                }
            }
        };

        var model = "gemini-flash-latest"; // Updated to latest alias to avoid 404/deprecation issues
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, requestBody);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<JsonElement>();
            
            // Navigate the JSON response structure of Gemini API
            // candidates[0].content.parts[0].text
            var text = result.GetProperty("candidates")[0]
                             .GetProperty("content")
                             .GetProperty("parts")[0]
                             .GetProperty("text").GetString();

            var jsonResult = JsonSerializer.Deserialize<Dictionary<string, string>>(text); 

            return (jsonResult["verdict"], jsonResult["type"]);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AI Error: {ex}");
            return ($"Falha na AI: {ex.Message}", "Dúvida");
        }
    }
}
