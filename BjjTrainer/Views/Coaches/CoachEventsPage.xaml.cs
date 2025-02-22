using BjjTrainer.Models.DTO.Coaches;
using BjjTrainer.ViewModels.Coaches;

namespace BjjTrainer.Views.Coaches;

public partial class CoachEventsPage : ContentPage
{
    private readonly CoachViewModel _viewModel;

    public CoachEventsPage(CoachViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Retrieve SchoolId from preferences
        int schoolId = Preferences.Get("SchoolId", 0);
        if (schoolId > 0)
        {
            await _viewModel.LoadPastEvents(schoolId);
        }
        else
        {
            Console.WriteLine("Error: SchoolId not found.");
        }
    }

    private async void OnEventClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is CoachEventDto selectedEvent)
        {
            var parameters = new Dictionary<string, object>
        {
            { "eventId", selectedEvent.Id }
        };
            await Shell.Current.GoToAsync($"///{nameof(CoachEventDetailPage)}", parameters);
        }
    }
}