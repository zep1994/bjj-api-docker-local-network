using Microsoft.Maui.Controls;

namespace BjjTrainer
{
    public partial class AppShell : Shell
    {
        private readonly string _url = "http://10.0.2.2:5057";

        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;

            // Register the route for LessonsPage
            Routing.RegisterRoute("LessonsPage", typeof(Views.Lessons.LessonsPage));
        }
    }
}
