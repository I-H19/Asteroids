namespace Asteroids
{
    public class BulletSystem : ISceneFixedTickable, IPausable
    {
        private readonly BulletRegistry _bulletRegistry;

        public BulletSystem(BulletRegistry bulletRegistry)
        {
            _bulletRegistry = bulletRegistry;
        }

        public void FixedTick()
        {
            _bulletRegistry.ForEach(bullet => bullet.FixedTick());
        }

        public void Pause()
        {
            _bulletRegistry.ForEach(bullet => bullet.Pause());
        }

        public void Unpause()
        {
            _bulletRegistry.ForEach(bullet => bullet.Unpause());
        }
    }
}