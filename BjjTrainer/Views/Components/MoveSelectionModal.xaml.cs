using BjjTrainer.Models.Moves.BjjTrainer.Models.DTO.Moves;
using BjjTrainer.ViewModels.Components;
using BjjTrainer.Services.Trainings;
using System.Collections.ObjectModel;
using System.Linq;

namespace BjjTrainer.Views.Components
{
    public partial class MoveSelectionModal : ContentPage
    {
        public MoveSelectionViewModel ViewModel { get; private set; }
        private readonly TrainingService _trainingService;
        private readonly ObservableCollection<int> _selectedMoveIds;

        public MoveSelectionModal(ObservableCollection<int> selectedMoveIds)
        {
            InitializeComponent();
            _trainingService = new TrainingService();
            _selectedMoveIds = selectedMoveIds;

            LoadAllMovesAsync();
        }

        // Load all moves and pre-select those already linked to the training log
        private async void LoadAllMovesAsync()
        {
            try
            {
                // Fetch all moves from the API
                var allMoves = await _trainingService.GetAllMovesAsync();
                var moveDtos = new ObservableCollection<UpdateMoveDto>(allMoves);

                // Pre-select moves that match the selectedMoveIds
                foreach (var move in moveDtos)
                {
                    if (_selectedMoveIds.Contains(move.Id))
                    {
                        move.IsSelected = true;
                    }
                }

                ViewModel = new MoveSelectionViewModel(moveDtos);
                BindingContext = ViewModel;

                // Force ListView to refresh and reflect selection
                MoveListView.ItemsSource = null;
                MoveListView.ItemsSource = ViewModel.Moves;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading moves: {ex.Message}");
                await DisplayAlert("Error", $"Failed to load moves: {ex.Message}", "OK");
            }
        }

        // Toggle checkbox selection when tapping the list item
        private void OnMoveTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is UpdateMoveDto move)
            {
                move.IsSelected = !move.IsSelected;

                // Refresh the UI immediately
                ((ListView)sender).ItemsSource = null;
                ((ListView)sender).ItemsSource = ViewModel.Moves;
            }
        }

        // Send selected move IDs when modal closes
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var selectedIds = ViewModel.Moves
                .Where(m => m.IsSelected)
                .Select(m => m.Id)
                .ToList();

            MessagingCenter.Send(this, "MovesUpdated", selectedIds);
        }

        // Handle confirm button click to send selected move IDs
        private void OnConfirmButtonClicked(object sender, EventArgs e)
        {
            var selectedMoveIds = ViewModel.GetSelectedMoveIds();
            MessagingCenter.Send(this, "SelectedMovesUpdated", selectedMoveIds);
            Navigation.PopModalAsync();
        }
    }
}
