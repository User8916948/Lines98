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
     class CellLogics
    {
        public bool Used=false;
        public int val = 150;
       public int pos = 0;
    }
    public partial class MainWindow : Window
    {
        Random random = new Random();
        Cell last=null;
        Cell lastball = null;
        List<int> freeCells = new List<int>();
        int[,] Cells = new int[9, 9];
        List<CellLogics> LogicCells = new List<CellLogics>();
        List<Cell> path = new List<Cell>();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        CellLogics choosen = null;
        int color = 0;
       

        public void SpawnBalls()
        {
            for(int i = 0; i <3; i++)
            {
                int choose = random.Next(0, freeCells.Count-1);
                int ball=freeCells[choose];
                freeCells.RemoveAt(choose);
                int color = random.Next(2, 9);
                Cells[ball/10, ball % 10]=color;
                Cell cell =this.FindName($"cell{ball}") as Cell;
                cell.DrawBall(color);
                
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (choosen.val == 1)
            {
                timer.Stop();
                choosen = CellNeighbours(choosen).FirstOrDefault(x => x.val ==
                               CellNeighbours(choosen).Min(x => x.val));
                (this.FindName($"cell{choosen.pos}") as Cell).DrawBall(color);
                lastball.RemovePath();
                lastball.ContainsBall = false;
                lastball = null;
                last.Background = new SolidColorBrush(Color.FromRgb(181, 181, 181));
                last = null;

                foreach (CellLogics cell in LogicCells)
                {
                    cell.val = 150;
                }
                foreach(Cell cell in path)
                {
                    cell.RemovePath();
                }
                path = new List<Cell>();

                return;
            }
            choosen = CellNeighbours(choosen).FirstOrDefault(x => x.val ==
                                CellNeighbours(choosen).Min(x => x.val));
            (this.FindName($"cell{choosen.pos}") as Cell).DrawPath();
            path.Add(this.FindName($"cell{choosen.pos}") as Cell);
        }
        public MainWindow()
        {
            InitializeComponent();
            foreach(Control c in Field.Children)
            {
                freeCells.Add(Int32.Parse(c.Tag.ToString()));
                LogicCells.Add(new CellLogics { pos = Int32.Parse(c.Tag.ToString()) });

            }
            SpawnBalls();
            SpawnBalls();
            SpawnBalls();
            SpawnBalls();
            //SpawnBalls();
            //SpawnBalls();
            //SpawnBalls();
            //SpawnBalls();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0,0, 0, 0,25);
        }
        private void BtnClick(object sender, RoutedEventArgs e)
        {  
        }
        private  void FillValues2(CellLogics cell, int value = 0)
        {
            Queue<CellLogics> q=new Queue<CellLogics>();
            q.Enqueue(cell);
            LogicCells.FirstOrDefault(x => x.pos == cell.pos).Used = true;
            while(q.Count > 0)
            {
                value++;
                CellLogics curr = q.Dequeue();
                foreach (CellLogics c in CellNeighbours(curr))
                {
                    if (c.Used == true)
                    {
                        continue;
                        
                    }
                    else
                    {
                        c.Used = true;
                        c.val = value;
                        q.Enqueue(c);
                    }
                    

                }
            }
            





        }
        private  List<CellLogics> FillValues(List<CellLogics> cells,int value=0,int priority=0)
        {
            List<CellLogics> q=new List<CellLogics>();
            foreach (CellLogics cell in cells)
            {
                cell.val = value;
                cell.Used = true;
                List<CellLogics> neigbours = CellNeighbours(cell);
                foreach (CellLogics c in neigbours)
                {
                    //if (a.Used == false)
                    //{
                    //    FillValues(a, value + 1);
                    //}
                    q.Add(c);

                }
            }
            return q;
        }
        private List<CellLogics> CellNeighbours(CellLogics cell)
        {
            List<CellLogics> neighbours = new List<CellLogics>();
            int val =cell.pos;
            if (LogicCells.FirstOrDefault(x => x.pos == val - 10) != null)
            {
                if ((this.FindName($"cell{val - 10}") as Cell).ContainsBall == false & LogicCells.FirstOrDefault(x => x.pos == val - 10).Used == false)
                {
                    neighbours.Add(LogicCells.FirstOrDefault(x => x.pos == val - 10));
                }
                
            }
            if (LogicCells.FirstOrDefault(x => x.pos == val + 10) != null)
            {
                if ((this.FindName($"cell{val + 10}") as Cell).ContainsBall == false & LogicCells.FirstOrDefault(x => x.pos == val + 10).Used == false) 
                {
                    neighbours.Add(LogicCells.FirstOrDefault(x => x.pos == val + 10));
                }
                    
            }
            if (LogicCells.FirstOrDefault(x => x.pos == val - 1) != null)
            {
                if ((this.FindName($"cell{val - 1}") as Cell).ContainsBall == false & LogicCells.FirstOrDefault(x => x.pos == val - 1).Used == false)
                {
                    neighbours.Add(LogicCells.FirstOrDefault(x => x.pos == val - 1));
                }
                    
            }
            if (LogicCells.FirstOrDefault(x => x.pos == val +1) != null)
            {
                if ((this.FindName($"cell{val + 1}") as Cell).ContainsBall == false & LogicCells.FirstOrDefault(x => x.pos == val + 1).Used == false)
                {
                    neighbours.Add(LogicCells.FirstOrDefault(x => x.pos == val + 1));
                }
                    
            }
            return neighbours;
        }
        private void Foo()
        {
            int i = 0;
        }
        private async void left_click(object sender, MouseButtonEventArgs e)
        {
            Cell c = sender as Cell;
            c.Background = new SolidColorBrush(Color.FromRgb(38, 42, 49));//#262A31
            List<Cell> dd = new List<Cell>();
            if (c.ContainsBall==true)
            {
                color = c.color;
                
            }
            System.Diagnostics.Debug.WriteLine(color);
            //foreach (AA b in CellNeighbours(aas.FirstOrDefault(x => x.pos == Int32.Parse(c.Tag.ToString()))){
            //    //System.Diagnostics.Debug.WriteLine()
            //}
            if (last != null)
            {
                last.Background=new SolidColorBrush(Color.FromRgb(181,181,181));//#b5b5b5



                if (last.ContainsBall == true & c.ContainsBall == false)
                {
                    lastball= last;
                    List<CellLogics> lst = new List<CellLogics>();
                    lst.Add(LogicCells.FirstOrDefault(x => x.pos == Int32.Parse(c.Tag.ToString())));
                    int value = 1;
                    lst=FillValues(lst);
                    while(lst.Count > 0)
                    {
                        lst = FillValues(lst,value);
                        value++;
                    }
                    //int i = 0;
                    //foreach (AA aa in aas)
                    //{
                    //    System.Diagnostics.Debug.Write($"{aa.val} ");
                    //    i++;
                    //    if (i == 9)
                    //    {
                    //        System.Diagnostics.Debug.WriteLine("");
                    //        i = 0;
                    //    }


                    //    }
                        foreach (CellLogics bb in LogicCells)
                    {
                        bb.Used = false;
                    }
                     choosen = CellNeighbours(LogicCells.FirstOrDefault(x => x.pos == Int32.Parse(last.Tag.ToString()))).FirstOrDefault(x => x.val ==
                      CellNeighbours(LogicCells.FirstOrDefault(x => x.pos == Int32.Parse(last.Tag.ToString()))).Min(x => x.val));
                    
                    
                    
                    if (choosen != null)
                    {

                        if(choosen.val != 150)
                        {
                            (this.FindName($"cell{choosen.pos}") as Cell).DrawPath();
                            path.Add(this.FindName($"cell{choosen.pos}") as Cell);
                            //dd.Add((this.FindName($"cell{choosen.pos}") as Cell));
                            // while (choosen.val != 1)
                            //{
                            //choosen = CellNeighbours(choosen).FirstOrDefault(x => x.val ==
                            //CellNeighbours(choosen).Min(x => x.val));
                            //(this.FindName($"cell{choosen.pos}") as Cell).DrawPath();
                            //}
                            if (choosen.val != 0)
                            {
                                timer.Start();
                            }
                            


                        }
                        
                       
                    }
                    //choosen = null;
                    //SpawnBalls();
                }


            }


            last = c;
            //foreach(AA a in CellNeighbours(aas.First(x => x.pos == Int32.Parse(c.Tag.ToString()))))
            //{
            //    System.Diagnostics.Debug.WriteLine(a.pos);
            //}
            //System.Diagnostics.Debug.WriteLine("");

           // Foo();
            //foreach (string s in CellNeighbours(c.Tag.ToString()))
            //{
            //    System.Diagnostics.Debug.WriteLine($"{s}");
            //}

            //foreach(string a in freeCells)
            //{
            //    System.Diagnostics.Debug.WriteLine($"{a}");
            //}
        }

        private void ssss(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
