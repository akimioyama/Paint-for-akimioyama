using System;
using System.Collections.Generic;
using System.IO;
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
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using Path = System.IO.Path;
using System.Windows.Ink;

namespace Paint_for_akimioyama
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        //bool mb_save = false; // переменна для првоерки сохранения файла 
        string path = @"";
        Point staart, eend;
        Color color;
        bool fl;
        
    
        


        public MainWindow()
        {
            InitializeComponent();

            InkCan.DefaultDrawingAttributes.Color = Colors.Black;
        }

        //Создать, открыть, созранить как...............
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog(); // диологове окно
            myDialog.Filter = "Файлы bpm (*.bpm)|*.bpm|Файлы jpeg (*.jpeg)|*.jpeg|Все файлы (*.*)|*.*"; // фильтр 

        }

        private void Save_As_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "savedimage"; // Имя файла
            dlg.DefaultExt = ".jpg"; // Расшереение
            dlg.Filter = "Image (.jpg)|*.jpg"; // Фильтр

            
            Nullable<bool> result = dlg.ShowDialog();

            
            if (result == true)
            {
                
                string filename = dlg.FileName;

                int margin = (int)InkCan.Margin.Left;
                int width = (int)InkCan.ActualWidth - margin;
                int height = (int)InkCan.ActualHeight - margin;
                
                RenderTargetBitmap rtb =
                new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);

                rtb.Render(InkCan);

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(fs);
                }
                //mb_save = true;
                path = filename;
            }
        }

        //Вид...........................................
        private void qqq(object sender, RoutedEventArgs e)
        {
            if (ToolBarTray_First.Visibility == Visibility.Visible)
            {
                if (ToolBarTray_Secend.Visibility == Visibility.Visible)
                {
                    ToolBarTray_First.Visibility = Visibility.Hidden;
                    qqqq.IsChecked = false;
                    Thickness posithion = new Thickness(-55, 2, 2, 2);
                    InkCan.Margin = posithion;
                }
                else
                {
                    ToolBarTray_First.Visibility = Visibility.Hidden;
                    qqqq.IsChecked = false;
                    Thickness posithion = new Thickness(-55, 2, 2, -55);
                    InkCan.Margin = posithion;
                }
            }
            else
            {
                if (ToolBarTray_Secend.Visibility == Visibility.Visible)
                {
                    ToolBarTray_First.Visibility = Visibility.Visible;
                    qqqq.IsChecked = true;
                    Thickness posithion = new Thickness(2, 2, 2, 2);
                    InkCan.Margin = posithion;
                }
                else
                {
                    ToolBarTray_First.Visibility = Visibility.Visible;
                    qqqq.IsChecked = true;
                    Thickness posithion = new Thickness(2, 2, 2, -55);
                    InkCan.Margin = posithion;
                }
            }
        }
        private void zzz(object sender, RoutedEventArgs e)
        {
            if (ToolBarTray_Secend.Visibility == Visibility.Visible)
            {
                if (ToolBarTray_First.Visibility == Visibility.Visible)
                {
                    ToolBarTray_Secend.Visibility = Visibility.Hidden;
                    zzzz.IsChecked = false;
                    Thickness posithion = new Thickness(2, 2, 2, -55);
                    Thickness posithion2 = new Thickness(0, 0, 0, -55);
                    ToolBarTray_First.Margin = posithion2;
                    InkCan.Margin = posithion;
                }
                else
                {
                    ToolBarTray_Secend.Visibility = Visibility.Hidden;
                    zzzz.IsChecked = false;
                    Thickness posithion = new Thickness(-55, 2, 2, -55);
                    InkCan.Margin = posithion;
                }
            }
            else
            {
                if (ToolBarTray_First.Visibility == Visibility.Visible)
                {
                    ToolBarTray_Secend.Visibility = Visibility.Visible;
                    zzzz.IsChecked = true;
                    Thickness posithion = new Thickness(2, 2, 2, 2);
                    InkCan.Margin = posithion;
                }
                else
                {
                    ToolBarTray_Secend.Visibility = Visibility.Visible;
                    zzzz.IsChecked = true;
                    Thickness posithion = new Thickness(-55, 2, 2, 2);
                    InkCan.Margin = posithion;
                }
            }
        }
        
        //Рисовать кистью...............................
        private void Click_Kist(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
            InkCan.EditingMode = InkCanvasEditingMode.Ink;
            ToolBar_Kisti.Visibility = Visibility.Visible;
            blackColor.IsChecked = true;
            Kist2.IsChecked = true;

        }
        private void Click_Kist1(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.Height = 2;
            InkCan.DefaultDrawingAttributes.Width = 2;
        }
        private void Click_Kist2(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.Height = 5;
            InkCan.DefaultDrawingAttributes.Width = 5;
        }
        private void Click_Kist3(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.Height = 10;
            InkCan.DefaultDrawingAttributes.Width = 10;
        }
        private void Click_Kist4(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.Height = 20;
            InkCan.DefaultDrawingAttributes.Width = 20;
        }
        private void Click_Kist5(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.Height = 50;
            InkCan.DefaultDrawingAttributes.Width = 50;
        }

        //Выделение
        private void Click_SomeSelect(object sender, RoutedEventArgs e)
        {
            InkCan.EditingMode = InkCanvasEditingMode.Select;
            ToolBar_Kisti.Visibility = Visibility.Hidden;
        }
        //Ластик
        private void Click_Lastik(object sender, RoutedEventArgs e)
        {
            InkCan.EditingMode = InkCanvasEditingMode.EraseByPoint; //EraseByStroke
            ToolBar_Kisti.Visibility = Visibility.Visible;
            
        }
        private void Click_kararnah(object sender, RoutedEventArgs e)
        {
            ToolBar_Kisti.Visibility = Visibility.Hidden;
            
        }
        private void Click_Line(object sender, RoutedEventArgs e)
        {
           
        }

        //Цвета
        private void Click_Color(object sender, RoutedEventArgs e)
        {
            string str = (string)((RadioButton)e.OriginalSource).Name;

            switch (str) 
            {
                case "grayColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Gray;
                    color = Colors.Gray;
                    break;
                case "blackColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Black;
                    color = Colors.Black;
                    break;
                case "blueColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Blue;
                    color = Colors.Blue;
                    break;
                case "greenColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Green;
                    color = Colors.Green;
                    break;
                case "purpleColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Purple;
                    color = Colors.Purple;
                    break;
                case "redColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Red;
                    color = Colors.Red;
                    break;
                case "whiteColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.White;
                    color = Colors.White;
                    break;
                case "yellowColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Yellow;
                    color = Colors.Yellow;
                    break;
            }
        }
        
        
        private void inkCanvas_MouseDown(Object sender, MouseEventArgs e)
        {
            if (Elips.IsChecked == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    staart = e.GetPosition(InkCan);
                    fl = true;
                }
            }
        }     
        private void inkCanvas_MouseMove(Object sender, MouseEventArgs e)
        {
            tb.Content = (e.GetPosition(null).X - 57) + ";" + (e.GetPosition(null).Y - 20);
        }
        private void inkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Click_Elips(object sender, RoutedEventArgs e)
        {
            InkCan.EditingMode = InkCanvasEditingMode.Ink;
        }

        //Очистить
        private void Clear(object sender, RoutedEventArgs e)
        {
            InkCan.Strokes.Clear();
        }

        private void Click_Atributs(object sender, RoutedEventArgs e)
        {
            


        }

        private void Click_Regtangl(object sender, RoutedEventArgs e)
        {

        }

        //..............................................
    }
}
