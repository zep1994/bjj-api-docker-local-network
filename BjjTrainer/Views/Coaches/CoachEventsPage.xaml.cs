using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Services.Coaches;
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

    // Fix: Add missing OnEventSelected EventHandler
    private async void OnEventSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;

        var selectedEvent = (CoachEventDto)e.CurrentSelection.FirstOrDefault();
        if (selectedEvent != null)
        {
            await DisplayAlert("Event Details",
                $"Title: {selectedEvent.Title}\n" +
                $"Date: {selectedEvent.StartDate?.ToShortDateString()}\n" +
                $"Attendees: {selectedEvent.CheckIns.Count}\n" +
                $"Moves Practiced: {selectedEvent.Moves.Count}",
                "OK");
        }

        ((CollectionView)sender).SelectedItem = null;
    }
}