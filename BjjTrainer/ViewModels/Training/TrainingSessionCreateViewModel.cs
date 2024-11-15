using BjjTrainer.Models.Training;
using BjjTrainer.Services.Training;
using BjjTrainer.Services.Users;
using MvvmHelpers;
using System.Diagnostics;

namespace BjjTrainer.ViewModels.Training
{
    public partial class TrainingSessionCreateViewModel : BaseViewModel
    {
        private readonly TrainingSessionService _trainingSessionService;

        public string Name { get; set; }
        public TimeSpan? Duration { get; set; } = TimeSpan.FromHours(1);
        public DateOnly? Date { get; set; } = DateOnly.MinValue;
        public string? Location { get; set; }
        public string? Notes { get; set; }

        public string? TagsInput { get; set; } // For comma-separated tags input
        public string? AreasTrainedInput { get; set; } // For comma-separated areas trained input
        public string? MovesTrainedInput { get; set; } // For comma-separated moves trained input
        public string? LessonMovesInput { get; set; } // For comma-separated lesson moves input

        public string? TypeOfTraining { get; set; }
        public int? IntensityLevel { get; set; } = 5;
        public int? Rating { get; set; } = 3;

        public Command SaveTrainingSessionCommand { get; }

        public TrainingSessionCreateViewModel()
        {
            _trainingSessionService = new TrainingSessionService();
            SaveTrainingSessionCommand = new Command(async () => await SaveTrainingSession());
        }

        private async Task SaveTrainingSession()
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(Name))
            {
                Debug.WriteLine("Error: Name and Duration are required fields.");
                return;
            }

            var token = Preferences.Get("AuthToken", string.Empty);
            var userId = UserService.GetUserIdFromToken(token);

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User is not authenticated.", "OK");
                return;
            }

            try
            {
                var newSession = new TrainingSession
                {
                    Name = Name,
                    Duration = Duration,
                    Date = (DateOnly)Date,
                    Location = Location,
                    Notes = Notes,
                    Tags = TagsInput?.Split(',').Select(t => t.Trim()).ToList(),
                    TypeOfTraining = TypeOfTraining,
                    AreasTrained = AreasTrainedInput?.Split(',').Select(a => a.Trim()).ToList(),
                    MovesTrained = MovesTrainedInput?.Split(',').Select(m => m.Trim()).ToList(),
                    LessonMoves = LessonMovesInput?.Split(',').Select(l => l.Trim()).ToList(),
                    IntensityLevel = IntensityLevel,
                    Rating = Rating,
                    UserId = userId
                };

                // Call service to save the session
                await _trainingSessionService.CreateTrainingSessionAsync(newSession);

                // On success, show message and navigate to TrainingSessionListView
                await Application.Current.MainPage.DisplayAlert("Success", "Training session created successfully.", "OK");
                await Shell.Current.GoToAsync("//TrainingSessionListPage");
            }
            catch (Exception ex)
            {
                // Log the error and show failure message
                Debug.WriteLine($"Failed to create training session: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to create training session. Please check your input and try again.", "OK");
            }
            
        }
    }
}
