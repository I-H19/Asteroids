using UnityEngine;

public class ScreenBounds
{
    public readonly float Left;
    public readonly float Right;
    public readonly float Bottom;
    public readonly float Top;

    public float Width => Right - Left;
    public float Height => Top - Bottom;

    public ScreenBounds(float left, float right, float bottom, float top)
    {
        Left = left;
        Right = right;
        Bottom = bottom;
        Top = top;
    }

    public bool Contains(Vector2 position)
    {
        bool isInsideX = position.x >= Left && position.x <= Right;
        bool isInsideY = position.y >= Bottom && position.y <= Top;

        return isInsideX && isInsideY;
    }

    public bool IsOutOfBounds(Vector2 position)
    {
        return !Contains(position);
    }

    public Vector2 Wrap(Vector2 position)
    {
        float wrappedX = position.x;
        if (wrappedX > Right) wrappedX = Left;
        else if (wrappedX < Left) wrappedX = Right;

        float wrappedY = position.y;
        if (wrappedY > Top) wrappedY = Bottom;
        else if (wrappedY < Bottom) wrappedY = Top;

        return new Vector2(wrappedX, wrappedY);
    }

    public Vector2 GetRandomPointOnPerimeter()
    {
        float width = Width;
        float height = Height;

        float perimeter = 2f * (width + height);
        float perimeterDistance = Random.value * perimeter;

        if (perimeterDistance < width) return new Vector2(Left + perimeterDistance, Top);          
        perimeterDistance -= width;

        if (perimeterDistance < height) return new Vector2(Right, Top - perimeterDistance);         
        perimeterDistance -= height;

        if (perimeterDistance < width) return new Vector2(Right - perimeterDistance, Bottom);      
        perimeterDistance -= width;

        return new Vector2(Left, Bottom + perimeterDistance);                  
    }
}