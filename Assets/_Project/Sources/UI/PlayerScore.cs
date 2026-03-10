using System;

namespace _Project.Sources.UI
{
    public class PlayerScore
    {
        public int Score { get; private set; } = 0;
        public Action Changed;
        public void Increment()
        {
            Score++;
            Changed?.Invoke();
        }
        public void ResetScore()
        {
            Score = 0;
            Changed?.Invoke();
        }
    }
}
