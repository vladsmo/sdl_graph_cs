using static SDL2.SDL;

namespace sdl_graph
{
    public class EventHandler
    {
        public event Action OnQuit;

        public void HandleInput()
        {
            if (SDL_PollEvent(out SDL_Event @event) != 0)
            {
                switch (@event.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        OnQuit?.Invoke();
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        OnQuit?.Invoke();
                        break;
                }
            }
        }
    }
}
