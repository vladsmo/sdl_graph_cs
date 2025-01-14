namespace sdl_graph;

public struct Vector3(double x, double y, double z)
{
    public double X = x;
    public double Y = y;
    public double Z = z;

    public double Magnitude =>
      Math.Sqrt(X * X + Y * Y + Z * Z);

    public Vector3 Normalize()
    {
        X /= Magnitude;
        Y /= Magnitude;
        Z /= Magnitude;
        return new Vector3(X, Y, Z);
    }

    public override string ToString()
    {
        return $"X: {X} | Y: {Y} | Z: {Z}";
    }
}