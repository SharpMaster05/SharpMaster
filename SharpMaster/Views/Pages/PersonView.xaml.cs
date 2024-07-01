using SharpMaster.ViewModels.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpMaster.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PersonView.xaml
    /// </summary>
    public partial class PersonView : Page
    {
        public PersonView()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is  PersonViewModel viewModel) 
                await viewModel.InitializeAsync();
        }
    }
}
