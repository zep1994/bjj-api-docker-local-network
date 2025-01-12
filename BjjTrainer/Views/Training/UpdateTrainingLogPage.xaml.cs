using BjjTrainer.Messages;
using BjjTrainer.ViewModels.TrainingLogs;
using BjjTrainer.Views.Components;
using CommunityToolkit.Mvvm.Messaging;
using Syncfusion.Maui.Core.Carousel;
using Syncfusion.Maui.DataForm;
using System.Collections.ObjectModel;

namespace BjjTrainer.Views.Training
{
    [QueryProperty(nameof(LogId), "logId")]
    public partial class UpdateTrainingLogPage : ContentPage
    {
        private UpdateTrainingLogViewModel _viewModel;
        private int _logId;

        public int LogId
        {
            get => _logId;
            set
            {
                _logId = value;
                Console.WriteLine($"LogId set to: {_logId}");

                // Initialize ViewModel only after LogId is assigned
                if (_logId > 0)
                {
                    _viewModel = new UpdateTrainingLogViewModel(_logId);
                    BindingContext = _viewModel;
                    _viewModel.LoadLogDetails();
                }
            }
        }

        public UpdateTrainingLogPage(int logId)
        {
            InitializeComponent();
            LogId = logId;
            // Delay ViewModel Initialization
            dataForm.GenerateDataFormItem += OnGenerateDataFormItem;

            WeakReferenceMessenger.Default.Register<SelectedMovesUpdatedMessage>(this, OnSelectedMovesUpdated);
        }

        private void OnSelectedMovesUpdated(object recipient, SelectedMovesUpdatedMessage message)
        {
            _viewModel.UpdateSelectedMoves(new ObservableCollection<int>(message.SelectedMoveIds));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            WeakReferenceMessenger.Default.Unregister<SelectedMovesUpdatedMessage>(this);
        }

        // Opens the Move Selection Modal
        private async void OnEditMovesClicked(object sender, EventArgs e)
        {
            if (_viewModel.Moves == null || !_viewModel.Moves.Any())
            {
                await DisplayAlert("Notice", "No moves available. Add new moves from the selection modal.", "OK");
            }

            var modal = new MoveSelectionModal(new ObservableCollection<int>(_viewModel.SelectedMoveIds), LogId);
            await Navigation.PushModalAsync(modal);
        }

        // Handles updating the training log
        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            dataForm.Commit();

            if (dataForm.Validate())
            {
                bool success = await _viewModel.UpdateLogAsync();

                if (success)
                {
                    await DisplayAlert("Success", "Training log updated successfully.", "OK");
//                    await Navigation.PushAsync(new TrainingLogListPage());
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
            await Shell.Current.GoToAsync("//TrainingLogListPage");
        }

        // Customize numeric inputs for specific fields
        private void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
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
