using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace AucdtestCheater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataTextBox.Text =@"
FILE: 01.Heatbeat - Game Over (Original Mix).flac
  Size: 53343947 Hash: A48B02EB87FA305D92F1BBD0519F3E46 Accuracy: -m0
  Conclusion: CDDA 100%
".Trim();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var text = this.DataTextBox.Text.Trim();
            text += "\r\n";

            byte[] prefix =
            {
                0x01, 0x72, 0x45, 0x5F, 0x25, 0x6C, 0x64,
                0x7E, 0x4D, 0x37, 0x37, 0x6E, 0x72, 0x1F
            };

            byte[] text_bytes = Encoding.Unicode.GetBytes(text);

            byte[] to_hash = Enumerable.Concat(prefix, text_bytes).ToArray();

            byte[] hash = SHA1.Create().ComputeHash(to_hash);

            this.ResultTextBox.Text =
                text + "  Signature: " + string.Join("", hash.Select(b => b.ToString("X2")))
                + Environment.NewLine;
        }
    }
}
