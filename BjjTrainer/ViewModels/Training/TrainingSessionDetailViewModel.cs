using BjjTrainer.Models.Training;
using BjjTrainer.Services.Training;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.Training
{
    [QueryProperty(nameof(SessionId), "SessionId")]
    public partial class TrainingSessionDetailViewModel : BaseViewModel
    {
        private readonly TrainingSessionService _trainingSessionService;

        public int SessionId { get; set; }
        public TrainingSession Session { get; private set; }

        public TrainingSessionDetailViewModel(TrainingSessionService trainingSessionService)
        {
            _trainingSessionService = trainingSessionService;
        }

        public async Task LoadSessionAsync()
        {
            Session = await _trainingSessionService.GetTrainingSessionByIdAsync(SessionId);
            OnPropertyChanged(nameof(Session));
        }
    }


}