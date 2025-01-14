namespace sdl_graph;

public struct Vector2(double x, double y)
{
    public double X = x;
    public double Y = y;

    public override string ToString()
    {
        return $"X: {X} | Y: {Y}";
    }
}