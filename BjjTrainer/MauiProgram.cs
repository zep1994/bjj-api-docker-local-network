using BjjTrainer.Services;
using BjjTrainer.Services.Lessons;
using BjjTrainer.Services.Moves;
using BjjTrainer.Services.TrainingGoals;
using BjjTrainer.Services.Trainings;
using BjjTrainer.Services.Users;
using BjjTrainer.ViewModels;
using BjjTrainer.ViewModels.Moves;
using BjjTrainer.ViewModels.TrainingGoals;
using BjjTrainer.ViewModels.TrainingLogs;
using BjjTrainer.ViewModels.Users;
using BjjTrainer.Views.Events;
using BjjTrainer.Views.Lessons;
using BjjTrainer.Views.Moves;
using BjjTrainer.Views.Training;
using BjjTrainer.Views.TrainingGoals;
using BjjTrainer.Views.Users;
using Syncfusion.Licensing;
using Syncfusion.Maui.Core.Hosting;

namespace BjjTrainer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .ConfigureSyncfusionCore();
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWH1fdHVURWRdVER0VkA=");
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
            builder.Services.AddSingleton<TrainingGoalService>();
            builder.Services.AddTransient<UserProgressViewModel>();
            builder.Services.AddTransient<TrainingGoalViewModel>();
            builder.Services.AddTransient<TrainingGoalListViewModel>();
            builder.Services.AddTransient<TrainingGoalListPage>();
            builder.Services.AddTransient<UpdateTrainingGoalPage>();
            builder.Services.AddTransient<UpdateTrainingGoalViewModel>();

            builder.Services.AddTransient<TrainingGoalPage>();
            builder.Services.AddTransient<TrainingLogFormPage>();
            builder.Services.AddTransient<TrainingGoalListPage>();
            builder.Services.AddTransient<TrainingGoalListViewModel>();
            builder.Services.AddTransient<UpdateTrainingLogPage>();
            builder.Services.AddTransient<UpdateTrainingLogViewModel>();


            // Register Calendar
            builder.Services.AddTransient<CalendarPage>();

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