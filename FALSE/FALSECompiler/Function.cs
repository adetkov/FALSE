using System.Linq;

namespace FALSECompiler
{
    public class Function
    {
        public uint Variables { get; private set; }

        public ILCode[] Codes { get; private set; }

        public Function(ILCode[] codes)
        {
            Codes = codes;
            Variables = codes
                .Where(c => c.Type == ILCode.ILType.StoreVariable || c.Type == ILCode.ILType.LoadVariable)
                .Aggregate(0U, (a, c) => a | (1U << (int)c.Tag));
        }
    }
}
