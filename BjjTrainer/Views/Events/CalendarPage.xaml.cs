using BjjTrainer.ViewModels.Events;

namespace BjjTrainer.Views.Events
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel(); // Set ViewModel here
        }
    }
}