using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;
using FALSE;

namespace FALIDE
{
    public class ConsoleControl : TextBox, IConsole
    {
        public int ConsoleCaret { get; protected set; }

        private int LastChar { get; set; }

        private readonly ManualResetEvent _waitKeyDown = new ManualResetEvent(false);

        private bool _waitKey = false;

        private bool _internal = false;

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!IsChagesAllowed() && !_internal)
            {
                _internal = true;
                e.Handled = true;
                SystemSounds.Beep.Play();
                CaretIndex = ConsoleCaret;
                Text = (string)e.OriginalSource;
                _internal = false;
            }

            base.OnTextChanged(e);
        }

        protected bool IsChagesAllowed()
        {
            return CaretIndex >= ConsoleCaret;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            LastChar = (int)e.Key;
            if ((e.Key == Key.Delete && !IsChagesAllowed()) || (e.Key == Key.Back && CaretIndex <= ConsoleCaret))
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
                CaretIndex = ConsoleCaret;
            }

            base.OnPreviewKeyDown(e);

            if (_waitKey)
            {
                _waitKeyDown.Set();
            }
            else if (e.Key == Key.Return && IsChagesAllowed())
            {
                CaretIndex = ConsoleCaret = Text.Length + 2;
            }
        }

        public ConsoleControl()
        {
            AcceptsReturn = true;
            FontFamily = new FontFamily("Consolas");
            FontSize = 16;
            Foreground = new SolidColorBrush(Colors.LightBlue);
            Background = new SolidColorBrush(Colors.Black);
        }

        public void Write<T>(T obj)
        {
            Dispatcher.BeginInvoke(
                new Action(() =>
                    {
                        _internal = true;
                        AppendText(obj.ToString());
                        CaretIndex = ConsoleCaret = Text.Length + 2;
                        _internal = false;
                    }));

        }

        public int Read()
        {
            _waitKey = true;
            _waitKeyDown.WaitOne();
            //Dispatcher.BeginInvoke(new Action(() => CaretIndex = ConsoleCaret = Text.Length + 2));
            _waitKey = false;

            return LastChar;
        }
    }
}
