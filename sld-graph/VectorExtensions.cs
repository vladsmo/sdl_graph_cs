namespace sdl_graph
{
    public static class VectorExtensions
    {
        public static Vector3 RotateX(this Vector3 origin, double angle)
        {
            return new Vector3(
              origin.X,
              origin.Y * Math.Cos(angle) - origin.Z * Math.Sin(angle),
              origin.Y * Math.Sin(angle) + origin.Z * Math.Cos(angle)
              );
        }
        public static Vector3 RotateY(this Vector3 origin, double angle)
        {
            return new Vector3(
              origin.X * Math.Cos(angle) - origin.Z * Math.Sin(angle),
              origin.Y,
              origin.X * Math.Sin(angle) + origin.Z * Math.Cos(angle)
              );
        }
        public static Vector3 RotateZ(this Vector3 origin, double angle)
        {
            return new Vector3(
              origin.X * Math.Cos(angle) - origin.Y * Math.Sin(angle),
              origin.X * Math.Sin(angle) + origin.Y * Math.Cos(angle),
              origin.Z
              );
        }
    }
}
