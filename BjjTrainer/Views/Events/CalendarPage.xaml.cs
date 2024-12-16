using BjjTrainer.ViewModels.Events;

namespace BjjTrainer.Views.Events;

public partial class CalendarPage : ContentPage
{
    private readonly CalendarViewModel _viewModel;

    public CalendarPage()
    {
        InitializeComponent();
        _viewModel = new CalendarViewModel();
        BindingContext = _viewModel;

        // Initialize the date picker with the current date
        WeekDatePicker.Date = _viewModel.SelectedDate;
    }

    private void OnPreviousWeekClicked(object sender, EventArgs e)
    {
        _viewModel.NavigateToPreviousWeek();
    }

    private void OnNextWeekClicked(object sender, EventArgs e)
    {
        _viewModel.NavigateToNextWeek();
    }

    private async void OnEventClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int eventId)
        {
            Console.WriteLine($"Navigating to ShowEventPage with EventId: {eventId}");
            await Navigation.PushAsync(new ShowEventPage(eventId));
        }
        else
        {
            Console.WriteLine("Invalid Event ID");
        }
    }
}
