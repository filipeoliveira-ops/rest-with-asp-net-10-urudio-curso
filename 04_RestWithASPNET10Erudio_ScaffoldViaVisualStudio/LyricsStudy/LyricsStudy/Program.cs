using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

class Program
{
	static async Task Main()
	{
																										// Instancia o cliente para fazer a requisição
		using HttpClient client = new HttpClient();

		Console.WriteLine("--- Lyrics for Study (Español/Inglés) ---");

		Console.Write("Artista: ");
		string artista = Console.ReadLine();

		Console.Write("Música: ");
		string musica = Console.ReadLine();

																										// Montando a URL da API
		string url = $"https://api.lyrics.ovh/v1/{artista}/{musica}";

		try
		{
			Console.WriteLine("\nBuscando letra... aguarde.");
			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();

				using JsonDocument doc = JsonDocument.Parse(jsonResponse);
				string letra = doc.RootElement.GetProperty("lyrics").GetString();

																											// Cria o arquivo .txt com o nome da música
				string nomeArquivo = $"{artista}_{musica}.txt".Replace(" ", "_");

				await File.WriteAllTextAsync(nomeArquivo, letra);

				Console.WriteLine("\nSUCESSO!");
				Console.WriteLine($"A letra foi salva em: {Path.GetFullPath(nomeArquivo)}");
				Console.WriteLine("\n--- TRECHO DA LETRA ---");
				Console.WriteLine(letra.Length > 200 ? letra.Substring(0, 200) + "..." : letra);
			}
			else
			{
				Console.WriteLine("\nErro: Música não encontrada ou API fora do ar.");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"\nErro inesperado: {ex.Message}");
		}

		Console.WriteLine("\nPressione qualquer tecla para sair...");
		Console.ReadKey();
	}
}