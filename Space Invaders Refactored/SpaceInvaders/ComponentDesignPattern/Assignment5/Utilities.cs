using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment5
{
    public static class Utilities
    {
        public static Vector2 ToCoordinate(this LocationPresets pPreset) => LocationCoordinates[(int) pPreset];
        public static Vector2 GetCoordinates(LocationPresets pPreset) => LocationCoordinates[(int)pPreset];


        public static Vector2[] LocationCoordinates = new Vector2[8]
        {
            new Vector2(0, 0),
            new Vector2(0.5f, 0),
            new Vector2(1.0f, 0),
            new Vector2(1.0f, 0.5f),
            new Vector2(1.0f, 1.0f),
            new Vector2(0.5f, 1.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(0.0f, 0.5f),
        };
    }
}