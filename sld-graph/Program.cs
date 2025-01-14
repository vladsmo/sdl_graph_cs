using sdl_graph;
using EventHandler = sdl_graph.EventHandler;

EventHandler eventHandler = new EventHandler();
Window window = new Window();

Game game = new Game(eventHandler, window);
game.Run();