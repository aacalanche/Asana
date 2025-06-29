using Asana.Maui.ViewModels;

namespace Asana.Maui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        // Constructor for MainPage
        // Initializes the MainPage and sets the BindingContext to MainPageViewModel
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void AddNewClickedToDo(object sender, EventArgs e)
        {
            // Navigate to ToDoDetails page when the Add New button is clicked
            Shell.Current.GoToAsync("//ToDoDetails");
        }
        private void EditClickedToDo(object sender, EventArgs e)
        {
            // Navigate to ToDoDetails page with the selected ToDo ID when the Edit button is clicked
            // If no ToDo is selected, it will default to 0
            var selectedId = (BindingContext as MainPageViewModel)?.SelectedToDoId ?? 0;
            Shell.Current.GoToAsync($"//ToDoDetails?toDoId={selectedId}");
        }

        private void DeleteClickedToDo(object sender, EventArgs e)
        {
            // Call the DeleteToDo method from the MainPageViewModel when the Delete button is clicked
            (BindingContext as MainPageViewModel)?.DeleteToDo();
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as MainPageViewModel)?.RefreshPage();
        }

        private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
        {

        }
        // Inline button click handlers for editing and deleting ToDo items
        private void InLineEditClickedToDo(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Asana.Maui.ViewModels.ToDoDetailViewModel vm)
            {
                var id = vm.Model?.Id ?? 0;
                Shell.Current.GoToAsync($"//ToDoDetails?toDoId={id}");
            }
        }

        private void InLineDeleteClickedToDo(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.RefreshPage();
        }

        private void AddNewProjectClicked(object sender, EventArgs e)
        {
            // Navigate to ProjectDetailView when the Add New Project button is clicked
            Shell.Current.GoToAsync("//ProjectDetails");
        }

        private void EditClickedProject(object sender, EventArgs e)
        {
            // Navigate to ProjectDetailView with the selected Project ID when the Edit button is clicked
            var selectedId = (BindingContext as MainPageViewModel)?.SelectedProjectId ?? 0;
            Shell.Current.GoToAsync($"//ProjectDetails?projectId={selectedId}");
        }

        private void DeleteClickedProject(object sender, EventArgs e)
        {
            // Call the DeleteProject method from the MainPageViewModel when the Delete button is clicked
            (BindingContext as MainPageViewModel)?.DeleteProject();
        }

        private void InLineEditClickedProject(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Asana.Maui.ViewModels.ProjectDetailViewModel vm)
            {
                var id = vm.Model?.Id ?? 0;
                Shell.Current.GoToAsync($"//ProjectDetails?projectId={id}");
            }
        }

        private void InLineDeleteClickedProject(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.RefreshPage();
        }
    }

}
