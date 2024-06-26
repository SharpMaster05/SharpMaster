﻿using SharpMaster.ViewModels.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpMaster.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegionView.xaml
    /// </summary>
    public partial class RegionView : Page
    {
        public RegionView()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var data = DataContext as RegionViewModel;
            await data.InitializeAsync();
        }
    }
}
