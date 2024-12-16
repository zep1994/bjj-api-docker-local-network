using BjjTrainer.Models.Moves;
using BjjTrainer.Services.Trainings;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.TrainingLogs;

public class ShowTrainingLogViewModel : BaseViewModel
{
    private readonly TrainingService _trainingService;

    public DateTime Date { get; set; }
    public double TrainingTime { get; set; }
    public int RoundsRolled { get; set; }
    public int Submissions { get; set; }
    public int Taps { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string SelfAssessment { get; set; } = string.Empty;
    public ObservableCollection<Move> Moves { get; set; } = new();

    public Command NavigateBackCommand { get; }

    public ShowTrainingLogViewModel()
    {
        _trainingService = new TrainingService();
        NavigateBackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public async Task LoadLogDetailsAsync(int logId)
    {
        IsBusy = true;

        try
        {
            Console.WriteLine($"Attempting to fetch training log with ID: {logId}"); // Debugging output

            var log = await _trainingService.GetTrainingLogByIdAsync(logId);

            if (log == null)
            {
                Console.WriteLine($"No training log found for ID: {logId}"); // Debugging output
                return;
            }

            Console.WriteLine($"Training log retrieved: {log.Notes}"); // Debugging output

            Date = log.Date;
            TrainingTime = log.TrainingTime;
            RoundsRolled = log.RoundsRolled;
            Submissions = log.Submissions;
            Taps = log.Taps;
            Notes = log.Notes ?? string.Empty;
            SelfAssessment = log.SelfAssessment ?? string.Empty;

            Moves.Clear();
            foreach (var move in log.Moves)
            {
                Moves.Add(move);
            }

            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(TrainingTime));
            OnPropertyChanged(nameof(RoundsRolled));
            OnPropertyChanged(nameof(Submissions));
            OnPropertyChanged(nameof(Taps));
            OnPropertyChanged(nameof(Notes));
            OnPropertyChanged(nameof(SelfAssessment));
            OnPropertyChanged(nameof(Moves));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading training log details: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
