using System;
using System.Windows;
using MangoAppsLoginApi.ViewModel;

namespace MangoAppsLoginApi
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly UserViewModel userViewModel;

        public LoginView()
        {
            InitializeComponent();
            userViewModel = new UserViewModel();
            this.DataContext = userViewModel;        
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtLogin.Focus();
        }

        private void BtnMinimizeScreen_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnCloseScreen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }  
    }
}
