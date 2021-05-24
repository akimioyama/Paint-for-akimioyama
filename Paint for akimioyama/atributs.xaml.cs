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
using System.Windows.Shapes;

namespace Paint_for_akimioyama
{
    /// <summary>
    /// Логика взаимодействия для atributs.xaml
    /// </summary>
    public partial class atributs : Window
    {
        public atributs()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            if (TextBox1.Text != "" || TextBox2.Text != "")
            {
                Class1.h = Convert.ToInt32(TextBox1.Text);
                Class1.w = Convert.ToInt32(TextBox2.Text);
                atributs1.Close();
            }
            else
            {
                Class1.h = 100;
                Class1.w = 100;
                atributs1.Close();
            }
        }
    }
}
