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
        Color color = Colors.Black;
        Brush brush = Brushes.Black;
        bool fl, yeep;

        enum ToolType { Line, Rect, Ellipse, RectSome }

        ToolType currentTool = ToolType.Line;
        Point startPoint = new Point();
        //Point currentPoint = new Point();
        Shape currentShape = null;
        Brush currentBrush = Brushes.Black;
        MouseButtonState previousMouseEvent = new MouseButtonState();
        int currentBrushThickness = 1;





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
            yeep = false;
            InkCan.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
            InkCan.EditingMode = InkCanvasEditingMode.Ink;
            ToolBar_Kisti.Visibility = Visibility.Visible;
            cbFillShape.Visibility = Visibility.Hidden;
            blackColor.IsChecked = true;
            Kist2.IsChecked = true;
            

        }
        private void Click_Kist1(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.Height = 2;
            InkCan.DefaultDrawingAttributes.Width = 2;
            currentBrushThickness = 2;
        }
        private void Click_Kist2(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.Height = 5;
            InkCan.DefaultDrawingAttributes.Width = 5;
            currentBrushThickness = 5;
        }
        private void Click_Kist3(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.Height = 10;
            InkCan.DefaultDrawingAttributes.Width = 10;
            currentBrushThickness = 10;
        }
        private void Click_Kist4(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.Height = 20;
            InkCan.DefaultDrawingAttributes.Width = 20;
            currentBrushThickness = 20;
        }
        private void Click_Kist5(object sender, RoutedEventArgs e)
        {
            InkCan.DefaultDrawingAttributes.Height = 50;
            InkCan.DefaultDrawingAttributes.Width = 50;
            currentBrushThickness = 50;
        }

        //Выделение
        private void Click_SomeSelect(object sender, RoutedEventArgs e)
        {
            yeep = false;
            InkCan.EditingMode = InkCanvasEditingMode.Select;
            ToolBar_Kisti.Visibility = Visibility.Hidden;
            cbFillShape.Visibility = Visibility.Hidden;
        }
        //Ластик
        private void Click_Lastik(object sender, RoutedEventArgs e)
        {
            yeep = false;
            InkCan.DefaultDrawingAttributes.Color = Colors.White;
            InkCan.EditingMode = InkCanvasEditingMode.Ink;
            //InkCan.EditingMode = InkCanvasEditingMode.EraseByPoint; //EraseByStroke
            ToolBar_Kisti.Visibility = Visibility.Visible;
            cbFillShape.Visibility = Visibility.Hidden;
            InkCan.DefaultDrawingAttributes.Color = color;

        }
        private void Click_kararnah(object sender, RoutedEventArgs e)
        {
            yeep = false;
            ToolBar_Kisti.Visibility = Visibility.Hidden;
            cbFillShape.Visibility = Visibility.Hidden;
        }


        //Цвета
        private void Color_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SolidColorBrush col = new SolidColorBrush(Color.FromArgb(cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B));
                brush = col;
            }
        }
        private void Click_Color(object sender, RoutedEventArgs e)
        {
            string str = (string)((RadioButton)e.OriginalSource).Name;

            switch (str) 
            {
                case "grayColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Gray;
                    color = Colors.Gray;
                    brush = Brushes.Gray;
                    break;
                case "blackColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Black;
                    color = Colors.Black;
                    brush = Brushes.Black;
                    break;
                case "blueColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Blue;
                    color = Colors.Blue;
                    brush = Brushes.Blue;
                    break;
                case "greenColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Green;
                    color = Colors.Green;
                    brush = Brushes.Green;
                    break;
                case "purpleColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Purple;
                    color = Colors.Purple;
                    brush = Brushes.Purple;
                    break;
                case "redColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Red;
                    color = Colors.Red;
                    brush = Brushes.Red;
                    break;
                case "whiteColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.White;
                    color = Colors.White;
                    brush = Brushes.White;
                    break;
                case "yellowColor":
                    InkCan.DefaultDrawingAttributes.Color = Colors.Yellow;
                    color = Colors.Yellow;
                    brush = Brushes.Yellow;
                    break;
            }
        }
        
        
        private void inkCanvas_MouseDown(Object sender, MouseEventArgs e)
        {
            if (yeep)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    currentShape = new Line();
                startPoint = e.GetPosition(InkCan);
            }
        }     
        private void inkCanvas_MouseMove(Object sender, MouseEventArgs e)
        {
            tb.Content = (e.GetPosition(null).X - 60) + ";" + (e.GetPosition(null).Y - 23);
            
            

            if (yeep)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    InkCan.Children.Remove(currentShape);

                    switch (currentTool)
                    {
                        case ToolType.Line:
                            drawLine(e);
                            break;
                        case ToolType.Rect:
                            drawRect(e);
                            break;
                        case ToolType.Ellipse:
                            drawEllipse(e);
                            break; 
                        case ToolType.RectSome:
                            drawRectSome(e);
                            break;
                    }
                }
                previousMouseEvent = e.LeftButton;
            }

        }
        private void inkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }


        //Очистить
        private void Clear(object sender, RoutedEventArgs e)
        {
            InkCan.Strokes.Clear();
        }

        private void Click_Atributs(object sender, RoutedEventArgs e)
        {
            


        }



        // Прямоугольник, линия, элипс
        private void Click_Regtangl(object sender, RoutedEventArgs e)
        {
            if (Regtangl.IsChecked == true)
            {
                currentTool = ToolType.Rect;
                yeep = true;
                InkCan.EditingMode = InkCanvasEditingMode.None;
                ToolBar_Kisti.Visibility = Visibility.Visible;
                cbFillShape.Visibility = Visibility.Visible;
            }
            else
            {
                yeep = false;
            }
        }
        private void Click_RegtanglSome(object sender, RoutedEventArgs e)
        {
            if (RectSome.IsChecked == true)
            {
                currentTool = ToolType.RectSome;
                yeep = true;
                InkCan.EditingMode = InkCanvasEditingMode.None;
                ToolBar_Kisti.Visibility = Visibility.Visible;
                cbFillShape.Visibility = Visibility.Visible;
            }
            else
            {
                yeep = false;
            }
        }
        private void Click_Line(object sender, RoutedEventArgs e)
        {
            if (Linerr.IsChecked == true)
            {
                currentTool = ToolType.Line;
                InkCan.EditingMode = InkCanvasEditingMode.None;
                ToolBar_Kisti.Visibility = Visibility.Visible;
                yeep = true;
                cbFillShape.Visibility = Visibility.Visible;
            }
            else
            {
                yeep = false;
            }
        }
        private void Click_Elips(object sender, RoutedEventArgs e)
        {
            if (Elips.IsChecked == true)
            {
                currentTool = ToolType.Ellipse;
                InkCan.EditingMode = InkCanvasEditingMode.None;
                ToolBar_Kisti.Visibility = Visibility.Visible;
                yeep = true;
                cbFillShape.Visibility = Visibility.Visible;
            }
            else
            {
                yeep = false;
            }
        }
        private void drawLine(MouseEventArgs e)
        {
            Line line = new Line();
            line.Stroke = brush;
            line.StrokeThickness = currentBrushThickness;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;
            line.X2 = e.GetPosition(InkCan).X;
            line.Y2 = e.GetPosition(InkCan).Y;

            InkCan.Children.Add(line);
            currentShape = line;
        }

        private void drawRect(MouseEventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect.Width = Math.Abs(startPoint.X - e.GetPosition(InkCan).X);
            rect.Height = Math.Abs(startPoint.Y - e.GetPosition(InkCan).Y);
            rect.Stroke = brush;
            rect.StrokeThickness = currentBrushThickness;

            if (cbFillShape.IsChecked == true)
            {
                rect.Fill = brush;                
            }
            if (startPoint.X - e.GetPosition(InkCan).X > 0)
            {
                InkCanvas.SetLeft(rect, startPoint.X - rect.Width);
            }
            else
            {
                InkCanvas.SetLeft(rect, startPoint.X);
            }
            if (startPoint.Y - e.GetPosition(InkCan).Y > 0)
            {
                InkCanvas.SetTop(rect, startPoint.Y - rect.Height);
            }
            else
            {
                InkCanvas.SetTop(rect, startPoint.Y);
            }
            InkCan.Children.Add(rect);
            currentShape = rect;
        }
        private void drawRectSome(MouseEventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect.Width = Math.Abs(startPoint.X - e.GetPosition(InkCan).X);
            rect.Height = Math.Abs(startPoint.Y - e.GetPosition(InkCan).Y);
            rect.Stroke = brush;
            rect.StrokeThickness = currentBrushThickness;
            rect.RadiusX = 10;
            rect.RadiusY = 10;

            if (cbFillShape.IsChecked == true)
            {
                rect.Fill = brush;
            }
            if (startPoint.X - e.GetPosition(InkCan).X > 0)
            {
                InkCanvas.SetLeft(rect, startPoint.X - rect.Width);
            }
            else
            {
                InkCanvas.SetLeft(rect, startPoint.X);
            }
            if (startPoint.Y - e.GetPosition(InkCan).Y > 0)
            {
                InkCanvas.SetTop(rect, startPoint.Y - rect.Height);
            }
            else
            {
                InkCanvas.SetTop(rect, startPoint.Y);
            }
            InkCan.Children.Add(rect);
            currentShape = rect;
        }
        private void drawEllipse(MouseEventArgs e)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = Math.Abs(startPoint.X - e.GetPosition(InkCan).X);
            ellipse.Height = Math.Abs(startPoint.Y - e.GetPosition(InkCan).Y);
            ellipse.Stroke = brush;
            ellipse.StrokeThickness = currentBrushThickness;

            if (cbFillShape.IsChecked == true)
            {
                ellipse.Fill = brush;
            }

            if (startPoint.X - e.GetPosition(InkCan).X > 0)
            {
                InkCanvas.SetLeft(ellipse, startPoint.X - ellipse.Width);
            }
            else
            {
                InkCanvas.SetLeft(ellipse, startPoint.X);
            }

            if (startPoint.Y - e.GetPosition(InkCan).Y > 0)
            {
                InkCanvas.SetTop(ellipse, startPoint.Y - ellipse.Height);
            }
            else
            {
                InkCanvas.SetTop(ellipse, startPoint.Y);
            }

            InkCan.Children.Add(ellipse);
            currentShape = ellipse;
        }
        //..............................................
    }
}
