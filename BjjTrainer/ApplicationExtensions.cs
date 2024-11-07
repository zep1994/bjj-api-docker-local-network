namespace BjjTrainer
{
    public static class ApplicationExtensions
    {
        public static IServiceProvider Services(this Application application) => application.Handler.MauiContext.Services;
    }
}
