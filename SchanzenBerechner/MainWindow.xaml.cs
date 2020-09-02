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

        private void OnBerechnenClick(object sender, RoutedEventArgs e)
        {
            double geschwindigkeit = double.Parse(AbsprungGeschwindigkeitTextBox.Text)/3.6;
            //double winkel = double.Parse(AbsprungWinkelTextBox.Text);
            double höhe = double.Parse(SprungHöheTextBox.Text);
            //double entfernugn = double.Parse(SprungEntfernungTextBox.Text);

            //SprungHöheTextBox.Text = Berechne.Höhe(v0: geschwindigkeit, alpha: winkel).ToString();
            //SprungEntfernungTextBox.Text = Berechne.Weite(v0: geschwindigkeit, alpha: winkel).ToString();
            AbsprungWinkelTextBox.Text = Berechne.Winkel(geschwindigkeit, höhe).ToString();
        }
    }
}
