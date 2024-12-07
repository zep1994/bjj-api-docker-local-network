using BjjTrainer.Services;
using BjjTrainer.Services.Lessons;
using BjjTrainer.Services.Moves;
using BjjTrainer.Services.Training;
using BjjTrainer.Services.Users;
using BjjTrainer.ViewModels;
using BjjTrainer.ViewModels.Moves;
using BjjTrainer.ViewModels.Users;
using BjjTrainer.Views.Lessons;
using BjjTrainer.Views.Moves;
using BjjTrainer.Views.Users;

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

            // Register services and view models
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddTransient<LessonsViewModel>();
            builder.Services.AddTransient<LessonSectionViewModel>();
            builder.Services.AddTransient<LessonsPage>();
            builder.Services.AddTransient<LessonSectionPage>();

            builder.Services.AddTransient<FavoritesViewModel>();
            builder.Services.AddTransient<FavoritesPage>();

            // Register Services
            builder.Services.AddSingleton<SubLessonService>();

            // Register ViewModels
            builder.Services.AddTransient<SubLessonViewModel>();
            builder.Services.AddTransient<SubLessonDetailsViewModel>();

            builder.Services.AddTransient<MainPageViewModel>();

            // Register Pages
            builder.Services.AddTransient<SubLessonPage>();
            builder.Services.AddTransient<SubLessonDetailsPage>();

            //Register Training
            builder.Services.AddSingleton<TrainingService>();

            // Register Users
            builder.Services.AddSingleton<UserProgressService>();
            builder.Services.AddTransient<UserProgressViewModel>();
            builder.Services.AddTransient<UserProgressPage>();

            // Register Moves
            builder.Services.AddSingleton<MoveService>();
            builder.Services.AddTransient<MovesViewModel>();
            builder.Services.AddTransient<MovesPage>();



            return builder.Build();
        }
    }
}