using System.Collections.Generic;
using FALSE;

namespace FALSECompiler
{
    public partial class Translator
    {
        private static IEnumerable<ILCode> ILIf(Token token)
        {
            var label = new ILLabel();

            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.JumpIfFalse, label);

            foreach (var c in TranslateToken(token))
                yield return c;

            foreach (var c in ILCallFunction())
                yield return c;

            yield return new ILCode(ILCode.ILType.Label, label);
        }

        private static IEnumerable<ILCode> ILWhile(Token[] tokens)
        {
            var condition = tokens[0];
            var body = tokens[1];
            
            var condLabel = new ILLabel();
            var endBodyLabel = new ILLabel();

            yield return new ILCode(ILCode.ILType.Label, condLabel);

            foreach (var c in TranslateToken(condition))
                yield return c;

            foreach (var c in ILCallFunction())
                yield return c;

            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.JumpIfFalse, endBodyLabel);

            foreach (var c in TranslateToken(body))
                yield return c;

            foreach (var c in ILCallFunction())
                yield return c;
            
            yield return new ILCode(ILCode.ILType.Jump, condLabel);
            yield return new ILCode(ILCode.ILType.Label, endBodyLabel);
        }
    }
}
