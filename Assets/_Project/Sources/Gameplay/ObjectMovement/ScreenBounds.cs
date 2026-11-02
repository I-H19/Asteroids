using UnityEngine;

namespace Asteroids
{
    public class ScreenBounds
    {
        private readonly float _bottom;
        private readonly float _left;
        private readonly float _right;
        private readonly float _top;

        public ScreenBounds(float left, float right, float bottom, float top)
        {
            _left = left;
            _right = right;
            _bottom = bottom;
            _top = top;
        }

        public bool IsOutOfBounds(Vector2 position)
        {
            return !Contains(position);
        }

        public Vector2 Wrap(Vector2 position)
        {
            float wrappedX = position.x;
            if (wrappedX > _right)
            {
                wrappedX = _left;
            }
            else if (wrappedX < _left)
            {
                wrappedX = _right;
            }

            float wrappedY = position.y;
            if (wrappedY > _top)
            {
                wrappedY = _bottom;
            }
            else if (wrappedY < _bottom)
            {
                wrappedY = _top;
            }

            return new(wrappedX, wrappedY);
        }

        public Vector2 GetRandomPointOnPerimeter()
        {
            float width = _right - _left;
            float height = _top - _bottom;

            float perimeter = 2f * (width + height);
            float perimeterDistance = Random.value * perimeter;

            if (perimeterDistance < width)
            {
                return new(_left + perimeterDistance, _top);
            }

            perimeterDistance -= width;

            if (perimeterDistance < height)
            {
                return new(_right, _top - perimeterDistance);
            }

            perimeterDistance -= height;

            if (perimeterDistance < width)
            {
                return new(_right - perimeterDistance, _bottom);
            }

            perimeterDistance -= width;

            return new(_left, _bottom + perimeterDistance);
        }

        private bool Contains(Vector2 position)
        {
            bool isInsideX = position.x >= _left && position.x <= _right;
            bool isInsideY = position.y >= _bottom && position.y <= _top;

            return isInsideX && isInsideY;
        }
    }
}