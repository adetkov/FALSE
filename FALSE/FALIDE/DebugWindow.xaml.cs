using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FALSE;

namespace FALIDE
{
    /// <summary>
    /// Interaction logic for ConsoleWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        private IEnumerable<Token> _tokens;
        private readonly VM _vm;

        public DebugWindow(IEnumerable<Token> tokens)
        {
            _tokens = tokens;
            InitializeComponent();

            _vm = new VM(console);
            new Thread(() => _vm.Run(new Context(_tokens))).Start();
        }
    }
}
