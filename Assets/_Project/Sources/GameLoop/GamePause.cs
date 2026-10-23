using System.Collections;
using System.Collections.Generic;
using VContainer;

namespace Asteroids
{
    public class GamePause
    {
        private IEnumerable _pausables;

        [Inject]
        public void Construct(IEnumerable<IPausable> pausables)
        {
            _pausables = pausables;
        }


        public void SetPause(bool pause)
        {
            if (pause)
            {
                foreach (IPausable pausable in _pausables)
                {
                    pausable.Pause();
                }
            }
            else
            {
                foreach (IPausable pausable in _pausables)
                {
                    pausable.Unpause();
                }
            }
        }
    }
}