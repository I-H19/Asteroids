using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class SceneTickDriver : ITickable, IPausable, IFixedTickable
    {
        private bool _isPaused;
        private IEnumerable<ISceneTickable> _sceneTickables;
        private IEnumerable<ISceneFixedTickable> _sceneFixedTickables;

        [Inject]
        public void Construct(
            IEnumerable<ISceneTickable> sceneTickables,
            IEnumerable<ISceneFixedTickable> sceneFixedTickables)
        {
            _sceneTickables = sceneTickables;
            _sceneFixedTickables = sceneFixedTickables;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }


        public void Tick()
        {
            if (_isPaused)
            {
                return;
            }

            foreach (ISceneTickable sceneTickable in _sceneTickables)
            {
                sceneTickable.Tick();
            }
        }

        public void FixedTick()
        {
            if (_isPaused)
            {
                return;
            }

            foreach (ISceneFixedTickable fixedTickable in _sceneFixedTickables)
            {
                fixedTickable.FixedTick();
            }
        }
    }
}
