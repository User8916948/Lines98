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

namespace Lines98
{
    /// <summary>
    /// Interaction logic for Cell.xaml
    /// </summary>
    public partial class Cell : UserControl
    {
        public bool ContainsBall=false;
        public int color = 0;
        public Cell()
        {
            InitializeComponent();
        }

        private void UserControl_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //this.Background=new SolidColorBrush(Colors.White);
        }
        public void DrawBall(int color)
        {
            this.color = color;
            DrawingGroup imageDrawings = new DrawingGroup();

            ImageDrawing Ball = new ImageDrawing();
            Ball.Rect = new Rect(0, 0, 50, 50);
            Ball.ImageSource = new BitmapImage(
                new Uri($@"images\ball{color}.png", UriKind.Relative));
            imageDrawings.Children.Add(Ball);
            DrawingImage drawingImageSource = new DrawingImage(imageDrawings);
            Image imageControl = new Image();
            imageControl.Stretch = Stretch.None;
            imageControl.Source = drawingImageSource;
            aa.Children.Add(imageControl);
            ContainsBall = true;
        }
        public void DrawPath()
        {
                DrawingGroup imageDrawings = new DrawingGroup();

                ImageDrawing Ball = new ImageDrawing();
                Ball.Rect = new Rect(0, 0, 15, 15);
                Ball.ImageSource = new BitmapImage(
                    new Uri($@"images\ballpath.png", UriKind.Relative));
                imageDrawings.Children.Add(Ball);
                DrawingImage drawingImageSource = new DrawingImage(imageDrawings);
                Image imageControl = new Image();
                imageControl.Stretch = Stretch.None;
                imageControl.Source = drawingImageSource;
                aa.Children.Add(imageControl);
                this.UpdateLayout();
                UpdateLayout();
                InvalidateVisual();
                aa.UpdateLayout();
                aa.InvalidateVisual();
                imageControl.UpdateLayout();
                imageControl.InvalidateVisual();
        }
        public void RemovePath()
        {
            if (aa.Children.Count > 0)
            {


                aa.Children.RemoveAt(aa.Children.Count - 1);
            }
        }
    }
}
