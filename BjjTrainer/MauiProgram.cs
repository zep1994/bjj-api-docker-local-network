using BjjTrainer.Lessons.Services;
using BjjTrainer.ViewModels;
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

            // Register services and view models for DI
            builder.Services.AddSingleton<LessonService>();
            builder.Services.AddTransient<LessonsViewModel>();
            builder.Services.AddTransient<LessonsPage>();


            return builder.Build();
        }
    }
}