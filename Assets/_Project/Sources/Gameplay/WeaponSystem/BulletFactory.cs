using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class BulletFactory
    {
        private float _bulletDamage;
        private Bullet _bulletPrefab;
        private DirectionalMoverSettings _bulletMoverSettings;
        private IObjectResolver _resolver;

        [Inject]
        public void Construct(
            IObjectResolver resolver,
            PrefabHolder prefabHolder,
            PlayerCombatSettings playerCombatSettings)
        {
            _resolver = resolver;
            _bulletPrefab = prefabHolder.BulletPrefab;
            _bulletMoverSettings = playerCombatSettings.BulletMoverSettings;
            _bulletDamage = playerCombatSettings.BulletDamage;
        }

        public Bullet Create(Vector3 position, Quaternion rotation)
        {
            Bullet bullet = _resolver.Instantiate(_bulletPrefab, position, rotation);
            bullet.Init(_bulletMoverSettings, _bulletDamage);
            return bullet;
        }
    }
}
