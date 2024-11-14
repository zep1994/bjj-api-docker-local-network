using BjjTrainer.Models.Training;
using BjjTrainer.Services.Training;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.Training
{
    [QueryProperty(nameof(SessionId), "SessionId")]
    public partial class TrainingSessionDetailViewModel : BaseViewModel
    {
        private readonly TrainingSessionService _trainingSessionService;
        private TrainingSession _trainingSession;

        public TrainingSession TrainingSession
        {
            get => _trainingSession;
            set => SetProperty(ref _trainingSession, value);
        }

        private int _sessionId;
        public int SessionId
        {
            get => _sessionId;
            set
            {
                _sessionId = value;
                LoadTrainingSession(_sessionId);
            }
        }


        public TrainingSessionDetailViewModel(TrainingSessionService trainingSessionService)
        {
            _trainingSessionService = trainingSessionService;
        }

        public async Task LoadTrainingSession(int sessionId)
        {
            TrainingSession = await _trainingSessionService.GetTrainingSessionByIdAsync(sessionId);
        }
    }
}