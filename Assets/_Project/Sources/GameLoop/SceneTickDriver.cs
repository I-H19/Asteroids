using System.Collections;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class SceneTickDriver : ITickable, IPausable
    {
        private bool _isPaused;
        private IEnumerable _sceneTickables;

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

        [Inject]
        public void Construct(IEnumerable<ISceneTickable> sceneTickables)
        {
            _sceneTickables = sceneTickables;
        }
    }
}