using System.Windows;
using TourPlanner.Library;
using TourPlanner.Views;

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
        }

        private void TourList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_AddNewTour(object sender, RoutedEventArgs e)
        {
            mainView.Content = new AddTourView();

            //string TourName = AddTourView.GetTourName();
            //if (TourName != null)
            //{
            //    TourList.Items.Add(TourName);
            //}
            
        }
    }
}
