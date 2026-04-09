namespace RestWithASPNET10Erudio.Configurations
{
	public static class CorsConfig
	{
		private static string[] GetAllowedOrigins(
			IConfiguration configuration)
		{
			return configuration.GetSection("Cors:Origins")
				.Get<string[]>() ?? Array.Empty<string>();
		}

		public static IServiceCollection AddCorsConfiguration(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("LocalPolicy",
					policy => policy
						.WithOrigins("http://localhost:3000")
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());

				options.AddPolicy("MultipleOriginPolicy",
					policy => policy
						.WithOrigins(
							"http://localhost:3000",
							"http://localhost:8080",
							"https://erudio.com.br"
						)
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());

				options.AddPolicy("DefaultPolicy",
					policy => policy
						.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
			});
			return services;
		}

		public static IApplicationBuilder UseCorsConfiguration(
	this IApplicationBuilder app,
	IConfiguration configuration)
		{
			// ❌ app.UseCors("DefaultPolicy");  — permite qualquer origem
			app.UseCors("MultipleOriginPolicy"); // ✅ só as origens configuradas
			return app;
		} 
	}
}