using System.Reflection.Emit;

namespace FALSECompiler
{
    internal class ILLabel
    {
        private Label? _label;

        public Label GetLabel(ILGenerator g)
        {
            _label = _label ?? g.DefineLabel();
            return _label.Value;
        }
    }
}
