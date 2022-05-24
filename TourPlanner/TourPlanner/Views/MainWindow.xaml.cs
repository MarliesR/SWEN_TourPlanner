using System.Windows;
using TourPlanner.Library;
using TourPlanner.Views;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void TourList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            mainView.Content = new EditTourView();
            // tour name und id müssen irgendwie verlinkt sein
        }

        private void Button_Click_AddNewTour(object sender, RoutedEventArgs e)
        {
            //mainView.Content = new AddTourView();
            mainView.NavigationService.Navigate(new AddTourView());

            //string TourName = AddTourView.GetTourName();
            //if (TourName != null)
            //{
            //    TourList.Items.Add(TourName);
            //}

        }
    }
}
