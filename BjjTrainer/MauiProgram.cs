using BjjTrainer.Services;
using BjjTrainer.Services.Events;
using BjjTrainer.Services.Lessons;
using BjjTrainer.Services.Moves;
using BjjTrainer.Services.TrainingGoals;
using BjjTrainer.Services.Trainings;
using BjjTrainer.Services.Users;
using BjjTrainer.ViewModels;
using BjjTrainer.ViewModels.Components;
using BjjTrainer.ViewModels.Events;
using BjjTrainer.ViewModels.Lessons;
using BjjTrainer.ViewModels.Moves;
using BjjTrainer.ViewModels.TrainingGoals;
using BjjTrainer.ViewModels.TrainingLogs;
using BjjTrainer.ViewModels.Users;
using BjjTrainer.Views.Components;
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
            SyncfusionLicenseProvider.RegisterLicense("MzYzMTg4OEAzMjM4MmUzMDJlMzBoT0NzMTFjZkl2L2hyU1RTTDRrTTZIODVLRFdHclNNVndSdTRSRTd1dUtBPQ==");
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register services and view models
            builder.Services.AddSingleton<ApiService>();

            builder.Services.AddTransient<LessonsViewModel>();
            builder.Services.AddTransient<LessonSectionViewModel>();
            builder.Services.AddTransient<LessonsPage>();
            builder.Services.AddTransient<LessonSectionPage>();

            builder.Services.AddTransient<FavoritesViewModel>();
            builder.Services.AddTransient<FavoritesPage>();

            // Register SubLessons
            builder.Services.AddSingleton<SubLessonService>();
            builder.Services.AddTransient<SubLessonViewModel>();
            builder.Services.AddTransient<SubLessonDetailsViewModel>();
            builder.Services.AddTransient<SubLessonPage>();
            builder.Services.AddTransient<SubLessonDetailsPage>();

            builder.Services.AddTransient<MainPageViewModel>();

            //Register Training Goal
            builder.Services.AddSingleton<TrainingService>();
            builder.Services.AddSingleton<TrainingGoalService>();
            builder.Services.AddTransient<TrainingGoalViewModel>();
            builder.Services.AddTransient<TrainingGoalListViewModel>();
            builder.Services.AddTransient<TrainingGoalListPage>();
            builder.Services.AddTransient<UpdateTrainingGoalPage>();
            builder.Services.AddTransient<UpdateTrainingGoalViewModel>();
            builder.Services.AddTransient<TrainingGoalPage>();
            builder.Services.AddTransient<ShowTrainingGoalPage>();
            builder.Services.AddTransient<ShowTrainingGoalViewModel>();

            // Training Log
            builder.Services.AddTransient<TrainingLogFormPage>();
            builder.Services.AddTransient<TrainingLogListPage>();
            builder.Services.AddTransient<TrainingLogListViewModel>();
            builder.Services.AddTransient<ShowTrainingLogViewModel>();
            builder.Services.AddTransient<ShowTrainingLogPage>();
            builder.Services.AddTransient<UpdateTrainingLogPage>();
            builder.Services.AddTransient<UpdateTrainingLogViewModel>();


            // Register Calendar
            builder.Services.AddSingleton<EventService>();
            builder.Services.AddTransient<CalendarPage>();
            builder.Services.AddTransient<ShowEventViewModel>();
            builder.Services.AddTransient<UpdateEventViewModel>();
            builder.Services.AddTransient<ShowEventPage>();
            builder.Services.AddTransient<UpdateEventPage>();
            builder.Services.AddTransient<CreateEventPage>();
            // Register Users
            builder.Services.AddSingleton<UserProgressService>();
            builder.Services.AddTransient<UserProgressViewModel>();
            builder.Services.AddTransient<UserProgressPage>();
            builder.Services.AddSingleton<UserService>();

            // Register Moves
            builder.Services.AddSingleton<MoveService>();
            builder.Services.AddTransient<MovesViewModel>();
            builder.Services.AddTransient<MovesPage>();
            builder.Services.AddTransient<MoveSelectionModal>();
            builder.Services.AddTransient<MoveSelectionViewModel>();
           



            return builder.Build();
        }
    }
}