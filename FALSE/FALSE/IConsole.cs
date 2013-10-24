namespace FALSE
{
    public interface IConsole
    {
        void Write<T>(T obj);

        int Read();
    }
}
