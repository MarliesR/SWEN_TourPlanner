﻿using System;
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

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for AddTourView.xaml
    /// </summary>
    public partial class AddTourView : Window
    {
        public AddTourView()
        {
            InitializeComponent();
            this.DataContext = new AddTourViewModel(this);
        }
    }
}
