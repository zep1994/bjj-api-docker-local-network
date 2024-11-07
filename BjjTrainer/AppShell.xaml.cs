namespace BjjTrainer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;

            // Register the route for LessonsPage
            Routing.RegisterRoute("LessonsPage", typeof(Views.Lessons.LessonsPage));
        }
    }
}
