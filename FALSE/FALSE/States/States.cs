namespace FALSE
{
    public static class States
    {
        private static IState _charState;

        private static IState _commonState;

        private static IState _stringState;

        public static IState Char
        {
            get
            {
                _charState = _charState ?? new CharState();
                return _charState;
            }
        }

        public static IState Common
        {
            get
            {
                _commonState = _commonState ?? new CommonState();
                return _commonState;
            }
        }

        public static IState String
        {
            get
            {
                _stringState = _stringState ?? new StringState();
                return _stringState;
            }
        }
    }
}