namespace TMS.Api
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Other services configuration

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });
    }
}
