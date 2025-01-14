using static SDL2.SDL;

namespace sdl_graph;

public unsafe class Window
{
    private struct ColorBuffer
    {
        public fixed uint Buffer[800 * 600];
    }

    private IntPtr _window;
    private IntPtr _renderer;

    private ColorBuffer _colorBuffer;
    private IntPtr _colorBufferTexture;

    public IntPtr SdlWindow => _window;
    public IntPtr SdlRender => _renderer;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public bool InitWindow()
    {
        SDL_Init(SDL_INIT_EVERYTHING).ThrowOnError();
        /*if (SDL_Init(SDL_INIT_EVERYTHING) != 0)
        {
            Console.WriteLine($"Failed to init SDL: {SDL_GetError()}");
            return false;
        }*/

        Width = 800;
        Height = 600;

        _window = SDL_CreateWindow(
          null,
          SDL_WINDOWPOS_CENTERED,
          SDL_WINDOWPOS_CENTERED,
          Width,
          Height,
          0);

        SDL_DisplayMode displayMode;
        SDL_GetCurrentDisplayMode(0, out displayMode);
        displayMode.w = 800;
        displayMode.h = 600;

        if (_window == IntPtr.Zero)
        {
            Console.WriteLine($"Failed to create window: {SDL_GetError()}");
            return false;
        }

        _renderer = SDL_CreateRenderer(_window, -1, 0);

        if (_renderer == IntPtr.Zero)
        {
            Console.WriteLine($"Failed to create renderer: {SDL_GetError()}");
        }

        _colorBuffer = new ColorBuffer();

        _colorBufferTexture = SDL_CreateTexture(
          _renderer,
          SDL_PIXELFORMAT_ABGR8888,
          2,
          Width,
          Height);

        //SDL_SetWindowFullscreen(_window, (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);

        return true;
    }

    public void DestroyWindow()
    {
        SDL_DestroyRenderer(_renderer);
        SDL_DestroyWindow(_window);
        SDL_Quit();
    }

    public void DrawPixel(int x, int y, uint color)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            _colorBuffer.Buffer[Width * y + x] = color;
        }
    }

    public void DrawRect(int x, int y, int width, int height, uint color)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int currentX = x + i;
                int currentY = y + j;
                DrawPixel(currentX, currentY, color);
            }
        }
    }

    public void RenderColorBuffer()
    {
        fixed (uint* buffer = _colorBuffer.Buffer)
        {
            SDL_UpdateTexture(
              _colorBufferTexture,
              IntPtr.Zero,
              (IntPtr)buffer,
              Width * 4);
        }

        SDL_RenderCopy(
          _renderer,
          _colorBufferTexture,
          IntPtr.Zero,
          IntPtr.Zero);
    }

    public void ClearColorBuffer(uint color)
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                _colorBuffer.Buffer[Width * i + j] = color;
            }
        }
    }
}