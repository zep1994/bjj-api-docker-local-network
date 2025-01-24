using BjjTrainer.Messages;
using BjjTrainer.Models.Moves.BjjTrainer.Models.DTO.Moves;
using BjjTrainer.ViewModels.TrainingLogs;
using BjjTrainer.Views.Components;
using CommunityToolkit.Mvvm.Messaging;
using Syncfusion.Maui.DataForm;
using System.Collections.ObjectModel;

namespace BjjTrainer.Views.Training
{
    [QueryProperty(nameof(LogId), "logId")]
    public partial class UpdateTrainingLogPage : ContentPage
    {
        private UpdateTrainingLogViewModel? _viewModel;
        private int _logId;

        public int LogId
        {
            get => _logId;
            set
            {
                _logId = value;
                Console.WriteLine($"LogId set to: {_logId}");

                // Initialize ViewModel after LogId is assigned
                if (_logId > 0)
                {
                    _viewModel = new UpdateTrainingLogViewModel(_logId);
                    BindingContext = _viewModel;
                }
            }
        }

        public UpdateTrainingLogPage()
        {
            InitializeComponent();

            // Register messenger to handle move selection updates
            WeakReferenceMessenger.Default.Register<SelectedMovesUpdatedMessage>(this, HandleSelectedMovesUpdated);
        }

        private void HandleSelectedMovesUpdated(object recipient, SelectedMovesUpdatedMessage message)
        {
            if (_viewModel != null)
            {
                // Update the Moves collection in the ViewModel
                _viewModel.UpdateSelectedMoves(message.Moves);

                // Notify UI to refresh
                OnPropertyChanged(nameof(_viewModel.Moves));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel != null)
            {
                // Ensure data is loaded only once
                await _viewModel.LoadTrainingLogDetailsAsync();
                OnPropertyChanged(nameof(_viewModel.Moves));
            }
        }

        private async void OnEditMovesClicked(object sender, EventArgs e)
        {
            if (_viewModel != null)
            {
                await _viewModel.EditMovesAsync();
            }
        }

        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            // Commit the form and validate inputs
            dataForm.Commit();

            if (dataForm.Validate())
            {
                bool success = await _viewModel.UpdateLogAsync();

                if (success)
                {
                    await DisplayAlert("Success", "Training log updated successfully.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update log. Please try again.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Validation Error", "Please check the form for errors.", "OK");
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }

        private void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            // Customize data form items for specific fields
            if (e.DataFormItem is DataFormTextEditorItem textEditorItem &&
                (e.DataFormItem.FieldName == "TrainingTime" ||
                 e.DataFormItem.FieldName == "RoundsRolled" ||
                 e.DataFormItem.FieldName == "Submissions" ||
                 e.DataFormItem.FieldName == "Taps"))
            {
                textEditorItem.Keyboard = Keyboard.Numeric;
                textEditorItem.Background = Colors.DarkSlateGray;
            }
        }
    }
}
