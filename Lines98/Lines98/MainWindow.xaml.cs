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
        Random random = new Random();

        //int[] colors = {1,2,3,4,5,6,7 };
        Control last=null;
        List<string> freeCells = new List<string>();
        List<List<int>> Cells = new List<List<int>>();

        public void SpawnBalls()
        {
            for(int i = 0; i <3; i++)
            {
                int choose = random.Next(0, freeCells.Count-1);
                string ball=freeCells[choose];
                freeCells.RemoveAt(choose);
                int color = random.Next(0, 6);
                char j = ball[0];
                char k = ball[1];
                Cells[Convert.ToInt32(j)][Convert.ToInt32(k)]=color;
                
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            foreach(Control c in Field.Children)
            {
                //int cell = Int32.Parse(c.Tag.ToString());
                freeCells.Add(c.Tag.ToString());
            }
            
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {  
        }
        private void left_click(object sender, MouseButtonEventArgs e)
        {
            SpawnBalls();
            if(last != null)
            {
                last.Background=new SolidColorBrush(Color.FromRgb(181,181,181));//#b5b5b5
            }
            Control c = sender as Control; 
            last = c;
            c.Background = new SolidColorBrush(Color.FromRgb(38,42,49));//#262A31
            foreach(string a in freeCells)
            {
                System.Diagnostics.Debug.WriteLine($"{a}");
            }
            int i = 0;
            int j = 0;
            foreach (List<int> a in Cells)
            {
                foreach (int b in a)
                {
                    System.Diagnostics.Debug.WriteLine($"{b} {i}{j}");
                    j++;
                }
                i++;
                j = 0;
            }
        }
    }
}
