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

namespace Lines98
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button last=null;
        public MainWindow()
        {
            InitializeComponent();
            //for (int i = 0; i <= 9; i++)
            //{
            //    Button button = new Button
            //    {
            //        Margin = new Thickness(1, 1, 1, 1),
            //        Height = (this.Height - 100) / 9,
            //        Width = (this.Height - 100) / 9,

            //    };
            //    Field.Children.Add(button);
            //}
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            if (last != null)
            {
                last.Background = new SolidColorBrush(Colors.Gray);
            }
            Button btn = sender as Button;
            last = btn;
            btn.Background = new SolidColorBrush(Colors.Black);
        }

        private void btn00_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
