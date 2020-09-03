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

namespace SchanzenBerechner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnBerechnenClickTab1(object sender, RoutedEventArgs e)
        {
            double winkel = double.Parse(AbsprungWinkelTextBox_Tab1.Text);
            double geschwindigkeit = double.Parse(AbsprungGeschwindigkeitTextBox_Tab1.Text)/3.6;

            SprungHöheText_Tab1.Content = Berechne.Höhe(v0: geschwindigkeit, alpha: winkel);
            SprungEntfernungText_Tab1.Content = Berechne.Weite(v0: geschwindigkeit, alpha: winkel);
        }

        private void OnBerechneClickTab2(object sender, RoutedEventArgs e)
        {
            double höhe = double.Parse(SprungHöheTextBox_Tab2.Text);
            double geschwindigkeit = double.Parse(AbsprungGeschwindigkeitTextBox_Tab2.Text)/3.6;

            AbsprungWinkelText_Tab2.Content = Berechne.Winkel(v0: geschwindigkeit, ys: höhe);
        }
        private void OnBerechneClickTab3(object sender, RoutedEventArgs e)
        {

        }
    }
}
