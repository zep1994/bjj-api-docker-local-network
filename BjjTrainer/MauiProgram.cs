using BjjTrainer.Services;
using BjjTrainer.Services.Lessons;
using BjjTrainer.ViewModels;
using BjjTrainer.ViewModels.Lessons;
using BjjTrainer.Views.Lessons;

namespace BjjTrainer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Configure HttpClient with a base URL
            builder.Services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = DeviceInfo.Platform == DevicePlatform.Android
                    ? new Uri("http://10.0.2.2:5057/api/") // Android Emulator base URL
                    : new Uri("http://localhost:5057/api/"); // Localhost URL for other platforms
            });

            // Register services and view models
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddTransient<LessonsViewModel>();
            builder.Services.AddTransient<LessonSectionViewModel>();
            builder.Services.AddTransient<LessonsPage>();
            builder.Services.AddTransient<LessonSectionPage>();
            // Register Services
            builder.Services.AddSingleton<SubLessonService>();

            // Register ViewModels
            builder.Services.AddTransient<SubLessonViewModel>();

            // Register Pages
            builder.Services.AddTransient<SubLessonPage>();
            builder.Services.AddTransient<SubLessonDetailsPage>();



            return builder.Build();
        }
    }
}