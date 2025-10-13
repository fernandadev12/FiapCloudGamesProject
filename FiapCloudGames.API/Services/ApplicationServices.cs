namespace FiapCloudGames.API.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {

            return services;
        }

        public static IApplicationBuilder AutenticateServices(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
