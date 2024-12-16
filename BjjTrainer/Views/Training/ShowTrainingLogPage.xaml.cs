using BjjTrainer.ViewModels.TrainingLogs;

namespace BjjTrainer.Views.Training;

[QueryProperty(nameof(LogId), "logId")]
public partial class ShowTrainingLogPage : ContentPage
{
    private readonly ShowTrainingLogViewModel _viewModel;

    private int _logId;
    public int LogId
    {
        get => _logId;
        set
        {
            _logId = value;
            Console.WriteLine($"LogId set to: {_logId}"); // Debugging output

            if (_viewModel != null && _logId > 0)
            {
                Task.Run(async () => await _viewModel.LoadLogDetailsAsync(_logId));
            }
        }
    }

    public ShowTrainingLogPage()
    {
        InitializeComponent();
        _viewModel = new ShowTrainingLogViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Console.WriteLine($"LogId received: {LogId}"); // Debugging output

        if (LogId > 0)
        {
            Console.WriteLine("Calling LoadLogDetailsAsync..."); // Debugging output
            await _viewModel.LoadLogDetailsAsync(LogId);
        }
        else
        {
            Console.WriteLine("LogId is invalid or missing."); // Debugging output
        }
    }
    private async void OnUpdateButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is ShowTrainingLogViewModel viewModel)
        {
            await Navigation.PushAsync(new UpdateTrainingLogPage(LogId));
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
