using BjjTrainer.ViewModels.TrainingLogs;
using BjjTrainer.Views.Components;
using Syncfusion.Maui.DataForm;
using System.Collections.ObjectModel;

namespace BjjTrainer.Views.Training
{
    public partial class UpdateTrainingLogPage : ContentPage
    {
        private UpdateTrainingLogViewModel _viewModel;

        public UpdateTrainingLogPage(int logId)
        {
            InitializeComponent();
            _viewModel = new UpdateTrainingLogViewModel(logId);  // Assign the ViewModel
            BindingContext = _viewModel;  // Set BindingContext to ViewModel

            // Handle custom editor assignment during item generation
            dataForm.GenerateDataFormItem += OnGenerateDataFormItem;

            // Subscribe to move selection updates
            MessagingCenter.Subscribe<MoveSelectionModal, List<int>>(this, "MovesUpdated", (sender, selectedIds) =>
            {
                _viewModel.UpdateSelectedMoves(new ObservableCollection<int>(selectedIds));
            });
        }

        // Edit Moves Modal
        private async void OnEditMovesClicked(object sender, EventArgs e)
        {
            // Ensure Moves collection is loaded before opening modal
            if (_viewModel.Moves == null || !_viewModel.Moves.Any())
            {
                await DisplayAlert("Error", "No moves available to edit.", "OK");
                return;
            }

            // Extract selected move IDs to pass to the modal
            var selectedMoveIds = new ObservableCollection<int>(
                _viewModel.Moves
                    .Where(m => m.IsSelected)
                    .Select(m => m.Id)
            );

            var modal = new MoveSelectionModal(selectedMoveIds);
            await Navigation.PushModalAsync(modal);
        }

        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            dataForm.Commit();  // Commit the changes

            if (dataForm.Validate())  // Validate the form
            {
                var viewModel = BindingContext as UpdateTrainingLogViewModel;
                bool success = await viewModel.UpdateLogAsync();

                if (success)
                {
                    await DisplayAlert("Success", "Training log updated successfully.", "OK");
                    await Navigation.PushAsync(new TrainingLogListPage());
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update log.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Validation Error", "Please check the form for errors.", "OK");
            }
        }

        // Ensure numeric fields in DataForm use proper keyboard
        private void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null &&
               (e.DataFormItem.FieldName == "TrainingTime" ||
                e.DataFormItem.FieldName == "RoundsRolled" ||
                e.DataFormItem.FieldName == "Submissions" ||
                e.DataFormItem.FieldName == "Taps"))
            {
                if (e.DataFormItem is DataFormTextEditorItem textEditorItem)
                {
                    textEditorItem.Keyboard = Keyboard.Numeric;
                    textEditorItem.Background = Colors.DarkSlateGray;
                }
            }
        }
    }
}
