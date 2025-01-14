using static SDL2.SDL;

namespace sdl_graph
{
    public class Game
    {
        private const int POINTS_COUNT = 9 * 9 * 9;

        private EventHandler _eventHandler;
        private Window _window;

        private Vector3 _cameraPosition = new() { X = 0, Y = 0, Z = -5 };
        private Vector3 _cubeRotation = new() { X = 0, Y = 0, Z = 0 };

        private Vector3[] _cubePoints = new Vector3[POINTS_COUNT];
        private Vector2[] _projectedPoints = new Vector2[POINTS_COUNT];

        private float _fovFactor = 640;

        public bool IsRunning { get; private set; }

        public Game(EventHandler eventHandler, Window window)
        {
            _eventHandler = eventHandler;
            _window = window;
        }

        public void Run()
        {
            IsRunning = _window.InitWindow();
            Start();

            while (IsRunning)
            {
                ProcessInput();
                Update();
                Render();
            }
        }

        private void Start()
        {
            _eventHandler.OnQuit += Quit;
            int currentPoint = 0;

            for (float x = -1; x < 1; x += 0.25f)
            {
                for (float y = -1; y < 1; y += 0.25f)
                {
                    for (float z = -1; z < 1; z += 0.25f)
                    {
                        Vector3 position = new Vector3(x, y, z);

                        _cubePoints[currentPoint++] = position;
                    }
                }
            }
        }

        private void Update()
        {
            _cubeRotation.X += 0.01;
            _cubeRotation.Y += 0.01;
            _cubeRotation.Z += 0.01;

            for (int i = 0; i < POINTS_COUNT; i++)
            {
                Vector3 point = _cubePoints[i];

                point = point.RotateX(_cubeRotation.X);
                point = point.RotateY(_cubeRotation.Y);
                point = point.RotateZ(_cubeRotation.Z);

                point.Z -= _cameraPosition.Z;

                Vector2 projectedPoint = Project(point);

                _projectedPoints[i] = projectedPoint;
            }
        }

        private void ProcessInput()
        {
            _eventHandler.HandleInput();
        }

        private void Render()
        {

            for (int i = 0; i < POINTS_COUNT; i++)
            {
                Vector2 projectedPoint = _projectedPoints[i];

                _window.DrawRect(
                    (int)projectedPoint.X + (_window.Width / 2),
                    (int)projectedPoint.Y + (_window.Height / 2),
                    4,
                    4,
                    0xFFFFFF00
                );
            }

            _window.RenderColorBuffer();
            _window.ClearColorBuffer(0xFF000000);

            SDL_RenderPresent(_window.SdlRender);
        }

        private void Quit()
        {
            _window.DestroyWindow();
            _eventHandler.OnQuit -= Quit;
        }

        private Vector2 Project(Vector3 point)
        {
            return new Vector2(
                _fovFactor * point.X / point.Z,
                _fovFactor * point.Y / point.Z);
        }
    }
}
