using System.Windows;
using System.Windows.Controls;
using TourPlanner.ViewModels;
using TourPlanner.Library;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaktionslogik für AddTourView.xaml
    /// </summary>
    public partial class AddTourView : Page
    {
        public AddTourView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //get data from textbox --> put in object (have to create object with tour data prop)
            this.DataContext = new AddTourViewModel(this);

            //Tour TourData = new Tour()
            //{
            //    Name = TourName.Text,
            //    Start = TourOrigin.Text,
            //    Destination = TourDestination.Text,
            //    Description = TourDescription.Text,
            //    TransportType = TourTransportType.Text
            //};

            // add tourname to tourlist
            // TourList.Items.Add(TourName.Text);
        }

        public string GetTourName()
        {
            return TourName.Text;
        }
    }
}
