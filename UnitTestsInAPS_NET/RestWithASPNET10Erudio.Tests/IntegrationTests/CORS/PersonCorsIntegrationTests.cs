using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RestWithASPNET10Erudio.Data.DTO.V1;
using RestWithASPNET10Erudio.Tests.IntegrationTests.Tools;

namespace RestWithASPNET10Erudio.Tests.IntegrationTests.CORS
{
	[Collection("Sequential")]
	[TestCaseOrderer("RestWithASPNET10Erudio.Tests.IntegrationTests.Tools.PriorityOrderer", "RestWithASPNET10Erudio.Tests")]
	public class PersonCorsIntegrationTests
	{
		private readonly HttpClient _httpClient;
		private static PersonDTO _person = null!;

		public PersonCorsIntegrationTests(SqlServerFixture sqlFixture)
		{
			var factory = new CustomWebApplicationFactory<Program>(
				sqlFixture.ConnectionString);

			_httpClient = factory.CreateClient(
				new WebApplicationFactoryClientOptions
				{
					BaseAddress = new Uri("https://localhost")
				}
			);
		}

		private void AddOriginHeader(string origin)
		{
			_httpClient.DefaultRequestHeaders.Remove("Origin");
			_httpClient.DefaultRequestHeaders.Add("Origin", origin);
		}

		[Fact(DisplayName = "01 - Create Person With Allowed Origin")]
		[TestPriority(1)]
		public async Task CreatePerson_WithAllowed_Origin_ShouldReturnCreated()
		{
			AddOriginHeader("https://erudio.com.br");
			var request = new PersonDTO
			{
				FirstName = "Richard",
				LastName = "Stallman",
				Address = "New York City - New York - USA",
				Gender = "Male"
			};

			var response = await _httpClient
				.PostAsJsonAsync("api/person/v1", request);

			response.EnsureSuccessStatusCode();

			// ✅ Origem permitida — header CORS deve estar presente
			response.Headers.Contains("Access-Control-Allow-Origin")
				.Should().BeTrue();

			var created = await response.Content
				.ReadFromJsonAsync<PersonDTO>();

			created.Should().NotBeNull();
			created.Id.Should().BeGreaterThan(0);
			_person = created;
		}

		[Fact(DisplayName = "02 - Create Person With Disallowed Origin")]
		[TestPriority(2)]
		public async Task CreatePerson_WithDisallowedOrigin_ShouldReturnForbiden()
		{
			AddOriginHeader("https://semeru.com.br");                       // aqui é o 'Postman'; (só que na IDE)
			var request = new PersonDTO		
			{
				FirstName = "Richard",
				LastName = "Stallman",	
				Address = "New York City - New York - USA",
				Gender = "Male"
			};

			var response = await _httpClient
				.PostAsJsonAsync("api/person/v1", request);

			// ✅ Origem não permitida — header CORS deve estar ausente
			response.Headers.Contains("Access-Control-Allow-Origin")
				.Should().BeFalse();
		}

		[Fact(DisplayName = "03 - Get Person By ID With Allowed Origin")]
		[TestPriority(3)]
		public async Task FindPersonById_WithAllowedOrigin_ShouldReturnOk()
		{
			AddOriginHeader("https://erudio.com.br");

			var response = await _httpClient
				.GetAsync($"api/person/v1/{_person.Id}");                   // aqui é o 'Postman'; (só que na IDE)

			response.EnsureSuccessStatusCode();

			// ✅ Origem permitida — header CORS deve estar presente
			response.Headers.Contains("Access-Control-Allow-Origin")
				.Should().BeTrue();

			var found = await response.Content
				.ReadFromJsonAsync<PersonDTO>();

			found.Should().NotBeNull();
			found.Id.Should().Be(_person.Id);
			found.FirstName.Should().Be("Richard");
			found.LastName.Should().Be("Stallman");
			found.Address.Should().Be("New York City - New York - USA");
		}

		[Fact(DisplayName = "04 - Get Person By ID With Disallowed Origin")]
		[TestPriority(4)]
		public async Task FindByIdPerson_WithDisallowedOrigin_ShouldReturnForbiden()
		{
			AddOriginHeader("https://semeru.com.br");

			var response = await _httpClient
				.GetAsync($"api/person/v1/{_person.Id}");

			// ✅ Origem não permitida — header CORS deve estar ausente
			response.Headers.Contains("Access-Control-Allow-Origin")
				.Should().BeFalse();
		}
	}
} 