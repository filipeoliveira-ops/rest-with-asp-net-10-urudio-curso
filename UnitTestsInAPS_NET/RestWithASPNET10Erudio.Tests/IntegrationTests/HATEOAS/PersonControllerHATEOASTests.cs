using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Identity.Client;
using RestWithASPNET10Erudio.Data.DTO.V1;
using RestWithASPNET10Erudio.Tests.IntegrationTests.Tools;

namespace RestWithASPNET10Erudio.Tests.IntegrationTests.HATEOAS
{
	[TestCaseOrderer(
		TestConfigs.TestCaseOrdererFullName,
		TestConfigs.TestCaseOrdererAssembly)]
	public class PersonControllerHATEOASTests : IClassFixture<SqlServerFixture>
	{
		private readonly HttpClient _httpClient;
		private static PersonDTO _person = null!;


		public PersonControllerHATEOASTests(SqlServerFixture sqlFixture)
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

		private void AssertLinkPattern(string content, string rel)
		{
			var pattern = $@"""rel"":\s*""{rel}"".*?""href"":\s*""https?://.+/api/person/v1.*?""";
			Regex.IsMatch(content, pattern).Should().BeTrue($"Link with rel='{rel}' should exist and have valid href");
		}

		[Fact(DisplayName = "01 - Create Person")]
		[TestPriority(1)]
		public async Task CreatePerson_ShouldContainHateoasLinks()
		{
			var request = new PersonDTO
			{
				FirstName = "David",
				LastName = "Heinemeier",
				Address = "Copenhagen, Denmark",
				Gender = "Male",
				Enabled = true,
			};

			var response = await _httpClient.PostAsJsonAsync(
				"/api/person/v1", request);

			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			_person = await response.Content.ReadFromJsonAsync<PersonDTO>();

			AssertLinkPattern(content, "collection");
			AssertLinkPattern(content, "self");
			AssertLinkPattern(content, "create");
			AssertLinkPattern(content, "update");
			AssertLinkPattern(content, "delete");

		}


		[Fact(DisplayName = "02 - Update Person")]
		[TestPriority(2)]
		public async Task UpdatePerson_ShouldContainHateoasLinks()
		{
			_person!.LastName = "Heinemeier Hansson";

			var response = await _httpClient.PutAsJsonAsync(
				"/api/person/v1", _person);

			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			_person = await response.Content.ReadFromJsonAsync<PersonDTO>();

			AssertLinkPattern(content, "collection");
			AssertLinkPattern(content, "self");
			AssertLinkPattern(content, "create");
			AssertLinkPattern(content, "update");
			AssertLinkPattern(content, "delete");
		}

		[Fact(DisplayName = "03 - Disable Person By Id")]
		[TestPriority(3)]
		public async Task DisablePersonById_ShouldContainHateoasLinks()
		{
			var response = await _httpClient.PatchAsync(
				$"/api/person/v1/{_person!.Id}", null);

			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();

			_person = await response.Content.ReadFromJsonAsync<PersonDTO>();

			AssertLinkPattern(content, "collection");
			AssertLinkPattern(content, "self");
			AssertLinkPattern(content, "create");
			AssertLinkPattern(content, "update");
			AssertLinkPattern(content, "delete");
		}

		[Fact(DisplayName = "04 - Get Person By Id")]
		[TestPriority(4)]
		public async Task GetPersonById_ShouldContainHateoasLinks()
		{
			var response = await _httpClient.GetAsync(
				$"/api/person/v1/{_person!.Id}");

			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();

			_person = await response.Content.ReadFromJsonAsync<PersonDTO>();

			AssertLinkPattern(content, "collection");
			AssertLinkPattern(content, "self");
			AssertLinkPattern(content, "create");
			AssertLinkPattern(content, "update");
			AssertLinkPattern(content, "delete");
		}

		[Fact(DisplayName = "05 - Find All Persons")]
		[TestPriority(5)]
		public async Task FindAllPersons_ShouldReturnPersons()
		{
			var response = await _httpClient.GetAsync("/api/person/v1");

			response.EnsureSuccessStatusCode();
			var persons = await response.Content.ReadFromJsonAsync<List<PersonDTO>>();

			persons.Should().NotBeNull();
			persons.Should().NotBeEmpty();
		}
	}
}
	