namespace FALSE
{
    public class Stack
    {
        private readonly Register[] _stack;

        private int _ptr = 0;

        public Stack(int capacity = 1024)
        {
            Capacity = capacity;
            _stack = new Register[capacity];
        }

        public enum Type : byte
        {
            Integer = 0,
            Char = 1,
            Lambda = 2
        }

        public int Capacity { get; private set; }

        public void Dup(int N = 1)
        {
            _stack[_ptr++] = _stack[_ptr - N];
        }

        public int Pop()
        {
            return _stack[--_ptr].Value;
        }

        public Register PopRegister()
        {
            return _stack[--_ptr];
        }

        public void Push(int value, Type type = Type.Integer)
        {
            _stack[_ptr++] = new Register {Value = value, Type = type};
        }

        public void PushRegister(Register item)
        {
            _stack[_ptr++] = item;
        }

        public void Rot()
        {
            var t = _stack[_ptr - 3];
            _stack[_ptr - 3] = _stack[_ptr - 2];
            _stack[_ptr - 2] = _stack[_ptr - 1];
            _stack[_ptr - 1] = t;
        }

        public void Swap()
        {
            var t = _stack[_ptr - 2];
            _stack[_ptr - 2] = _stack[_ptr - 1];
            _stack[_ptr - 1] = t;
        }
    }
}
