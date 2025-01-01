using BjjTrainer.ViewModels.Events;
using Syncfusion.Maui.Scheduler;

namespace BjjTrainer.Views.Events
{
    public partial class CreateEventPage : ContentPage
    {
        private readonly CreateEventViewModel _viewModel;

        public CreateEventPage(DateTime start, DateTime end)
        {
            InitializeComponent();
            _viewModel = new CreateEventViewModel
            {
                StartDate = start,
                StartTime = start.TimeOfDay,
                EndDate = end,
                EndTime = end.TimeOfDay
            };

            BindingContext = _viewModel;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is CalendarViewModel vm)
            {
                await vm.LoadAppointments();
            }
        }

        private async void OnSaveEventClicked(object sender, EventArgs e)
        {
            var success = await _viewModel.SaveEventAsync();

            if (success)
            {
                await DisplayAlert("Success", "Event saved successfully.", "OK");

                if (Application.Current.MainPage.Navigation.NavigationStack
                    .FirstOrDefault(x => x is CalendarPage) is CalendarPage calendarPage)
                {
                    if (calendarPage.BindingContext is CalendarViewModel calendarViewModel)
                    {
                        await calendarViewModel.LoadAppointments();
                    }
                }

                await Navigation.PushAsync(new CalendarPage());
            }
            else
            {
                await DisplayAlert("Error", "Failed to save event.", "OK");
            }
        }


        private async void OnCancelEventClicked(object sender, EventArgs e)
        {
            // Navigate back without saving
            await Navigation.PushAsync(new CalendarPage());
        }
    }
}

