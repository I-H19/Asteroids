using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class SceneTickDriver : ITickable, IPausable, IFixedTickable, ILateTickable
    {
        private bool _isPaused;
        private IEnumerable<ISceneTickable> _sceneTickables;
        private IEnumerable<ISceneFixedTickable> _sceneFixedTickables;
        private IEnumerable<ISceneLateTickable> _sceneLateTickables;

        [Inject]
        public void Construct(
            IEnumerable<ISceneTickable> sceneTickables,
            IEnumerable<ISceneFixedTickable> sceneFixedTickables,
            IEnumerable<ISceneLateTickable> sceneLateTickables)
        {
            _sceneTickables = sceneTickables;
            _sceneFixedTickables = sceneFixedTickables;
            _sceneLateTickables = sceneLateTickables;
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

        public void LateTick()
        {
            if (_isPaused)
            {
                return;
            }

            foreach (ISceneLateTickable lateTickable in _sceneLateTickables)
            {
                lateTickable.LateTick();
            }
        }
    }
}
