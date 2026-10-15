using System.Collections;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.GameLoop
{
    public class SceneTickDriver : ITickable, IPausable
    {
        private bool _isPaused;
        private IEnumerable _sceneTickables;

        [Inject]
        public void Construct(IEnumerable<ISceneTickable> sceneTickables) 
            =>  _sceneTickables  = sceneTickables;
        

        public void Tick()
        {
            if (!_isPaused)
                foreach (ISceneTickable sceneTickable in _sceneTickables) sceneTickable.Tick();
        }
        
        public void Pause() => _isPaused = true;
        public void Unpause() => _isPaused = false;
    }
}
