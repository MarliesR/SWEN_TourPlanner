using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private RelayCommand showTourPageCommand1;
        public ICommand showTourPageCommand => showTourPageCommand1 ??= new RelayCommand(showTourPage);

        private void showTourPage(object commandParameter)
        {
            AddTourView view = new ();


        }        
        
        //https://github.com/BernLeWal/fhtw-bif4-layeredarchitecture-cs 
        //viewmodelbase ist vorgegeben
        //MainViewModel muss im View auch hinterlegt werden 
        //xmlns:viewmodels="clr-namespace:TourPlanner.ViewModels" d:DataContext="{d:DesignInstance Type = viewmodels:MainViewModel}"
    }
}
