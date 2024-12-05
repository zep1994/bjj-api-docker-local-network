using BjjTrainer.Services.Moves;
using BjjTrainer.ViewModels.Moves;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BjjTrainer.ViewModels.Training
{
    public class TrainingLogFormViewModel : INotifyPropertyChanged
    {
        private readonly MoveService _moveService;
        public ObservableCollection<MoveViewModel> Moves { get; private set; }

        private string _notes;
        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        private double? _trainingTime = null; // Allow nullable for optional input
        public double? TrainingTime
        {
            get => _trainingTime;
            set => SetProperty(ref _trainingTime, value);
        }

        private int? _roundsRolled = null;
        public int? RoundsRolled
        {
            get => _roundsRolled;
            set => SetProperty(ref _roundsRolled, value);
        }

        private int? _submissions = null;
        public int? Submissions
        {
            get => _submissions;
            set => SetProperty(ref _submissions, value);
        }

        private int? _taps = null;
        public int? Taps
        {
            get => _taps;
            set => SetProperty(ref _taps, value);
        }


        private DateTime _date = DateTime.Now; // Default to today's date
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public TrainingLogFormViewModel()
        {
            _moveService = new MoveService();
            Moves = new ObservableCollection<MoveViewModel>();
            LoadMovesAsync();
        }

        private async Task LoadMovesAsync()
        {
            try
            {
                var moves = await _moveService.GetMovesAsync();
                Moves.Clear();
                foreach (var move in moves)
                {
                    Console.WriteLine($"Move Loaded: {move.Name}"); // Debugging
                    Moves.Add(move);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading moves: {ex.Message}");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
