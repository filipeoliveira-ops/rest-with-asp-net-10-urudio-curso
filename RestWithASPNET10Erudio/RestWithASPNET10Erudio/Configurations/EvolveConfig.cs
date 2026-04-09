using EvolveDb;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using Serilog;

namespace RestWithASPNET10Erudio.Configurations
{
	public static class EvolveConfig
	{
		private const bool V = true;

		public static List<string> Locations { get; private set; }
		public static bool IsEraseDisabled { get; private set; }

		public static IServiceCollection AddEvolveConfiguration(
			this IServiceCollection services,
			IConfiguration configuration,
			IWebHostEnvironment environment)
		{
			if (environment.IsDevelopment())
			{
				var connectionString = configuration.GetConnectionString(
					"MySQLServerSqlConnectionStrings");
				if (string.IsNullOrEmpty(connectionString))
				{
					throw new ArgumentNullException(
						"Connection string 'MSSQLServerSQLConnectionStrings");
				}

				try
				{
					ExecuteMigrations(connectionString);
					
				}
				catch (Exception ex)
				{
					Log.Error(ex, "An error ocurred while migrating the database.");
					throw;
				}
			}
			return services;
		}


			public static void ExecuteMigrations(string connectionString)
		{
			using var evolveConnection = new SqlConnection(connectionString);
			var evolve = new Evolve(
				evolveConnection,
				msg => Log.Information(msg))
			{
				Locations = new List<string> { "db/migrations", "db/dataset" },
				IsEraseDisabled = true,
			};
			evolve.Migrate();
		} 
	}	
}




	
			