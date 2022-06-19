using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourPlanner.ViewModels;
using TourPlanner.Library;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for EditTourView.xaml
    /// </summary>
    public partial class ImportTourView : Window
    {
        
        public ImportTourView()
        {
            InitializeComponent();
            this.DataContext = new ImportTourViewModel(this);
        }
    }
}
