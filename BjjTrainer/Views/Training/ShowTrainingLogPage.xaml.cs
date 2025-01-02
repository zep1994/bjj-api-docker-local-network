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
                // Directly await the async call to ensure UI updates correctly
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _viewModel.LoadLogDetailsAsync(_logId);
                });
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
        Console.WriteLine($"LogId received: {LogId}");

        if (LogId > 0)
        {
            Console.WriteLine("Loading data...");
            await _viewModel.LoadLogDetailsAsync(LogId);
            base.OnAppearing();
        }
        else
        {
            Console.WriteLine("Invalid LogId");
        }
    }


    private async void OnUpdateButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is ShowTrainingLogViewModel viewModel)
        {
            await Navigation.PushAsync(new UpdateTrainingLogPage(viewModel.Id));
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TrainingLogListPage");
    }
}
