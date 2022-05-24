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
            this.DataContext = new AddTourViewModel();
        }

        
    }
}
