namespace sdl_graph
{
    public class BackendException : Exception
    {
        public BackendException(string message) : base($"An error occured the SDL2 backend: {message}")
        {
        }
    }

    public static class BackendExceptions
    {
        public static void ThrowOnError(this int code)
        {
            if (code >= 0)
            {
                return;
            }
            throw new BackendException(code.ToString());
        }
    }
}
