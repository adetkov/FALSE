using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using FALSE;

namespace FALIDE
{
    public class CodeEditor : RichTextBox
    {
        public IEnumerable<Token> Tokens { get; private set; }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            if (_internal) return;

            var text = new TextRange(Document.ContentStart, Document.ContentEnd).Text;
            Tokens = Lexer.Tokenize(text);//.Where(x =>x.Type != OpCode.Error);
            _internal = true;
            GetDocument(text, Tokens);
            _internal = false;
        }

        public Block GetDocument(string text, IEnumerable<Token> tokens)
        {
            var par = new Paragraph();
            var startIndex = 0;
            new TextRange(Document.ContentStart, Document.ContentEnd).ClearAllProperties();
            foreach (var token in tokens)
            {
                var cur = GoToText(Document.ContentStart, startIndex);

                var next = GoToText(Document.ContentStart, token.Position + 1);
                var textrange = new TextRange(cur, next);
                textrange.ApplyPropertyValue(TextElement.ForegroundProperty,
                                             new SolidColorBrush(GetTokenColor(token)));
                startIndex = token.Position + 1;
            }

            return par;
        }

        private TextPointer GoToText(TextPointer tp, int offset)
        {
            while (tp != null && tp.CompareTo(Document.ContentEnd) < 0 && offset > 0)
            {
                
                while (tp != null && tp.GetPointerContext(LogicalDirection.Forward) != TextPointerContext.Text)
                {
                    tp = tp.GetNextContextPosition(LogicalDirection.Forward);
                }

                if (tp == null) break;
                offset--;
                tp = tp.GetPositionAtOffset(1);
            }


            return tp ?? Document.ContentEnd;
        }

        private Color GetTokenColor(Token token)
        {
            switch (token.Type.GetTokenType())
            {
                case TokenType.Number:
                    return Colors.LightBlue;
                case TokenType.Comment:
                    return Colors.LightGray;
                case TokenType.Char:
                case TokenType.String:
                    return Color.FromRgb(255, 212, 0);
                case TokenType.LoadVariable:
                case TokenType.StoreVariable:
                    return Colors.Orange;
                case TokenType.StackOperators:
                    return Color.FromRgb(176, 192, 129);
                case TokenType.ControlFlow:
                    return Color.FromRgb(190, 129, 192);
                    case TokenType.Error:
                    return Colors.Red;
                default:
                    return Colors.White;
            }
        }

        private bool _internal = false;
    }
}
