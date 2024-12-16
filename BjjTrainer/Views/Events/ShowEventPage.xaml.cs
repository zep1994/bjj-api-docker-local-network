using BjjTrainer.ViewModels.Events;

namespace BjjTrainer.Views.Events;

public partial class ShowEventPage : ContentPage
{
    private readonly ShowEventViewModel _viewModel;

    public ShowEventPage(int eventId)
    {
        InitializeComponent();
        Console.WriteLine($"Navigating to ShowEventPage with EventId: {eventId}");
        _viewModel = new ShowEventViewModel(eventId);
        BindingContext = _viewModel;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnUpdateButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UpdateEventPage(_viewModel.EventId));
    }
}
