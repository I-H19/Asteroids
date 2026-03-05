using VContainer;
using VContainer.Unity;

public class SceneTickDriver : ITickable
{
    private KeyboardMonitor _keyboardMonitor;
    private PlayerKeyboardController _keyboardController;
    private BulletSpawner _bulletSpawner;
    private Laser _laser;
    private EnemyDriver _enemyDriver;
    private EnemySpawner _enemySpawner;
    private Player _player;
    private bool _isPaused = false;

    [Inject]
    public void Construct(EnemyDriver enemyTickDriver, Player player, 
        Laser laser, BulletSpawner bulletSpawner,
        PlayerKeyboardController keyboardController, KeyboardMonitor keyboardMonitor, EnemySpawner enemySpawner)
    { 
        _enemySpawner = enemySpawner;
        _keyboardMonitor = keyboardMonitor;
        _keyboardController = keyboardController;
        _bulletSpawner = bulletSpawner;
        _laser = laser;
        _enemyDriver = enemyTickDriver;
        _player = player;
    }

    public void Tick()
    {
        if (!_isPaused)
        {
            _keyboardController.Tick();
            _keyboardMonitor.Tick();
            _bulletSpawner.Tick();
            _enemyDriver.Tick();
            _enemySpawner.Tick();
            _player.Tick();
            _laser.Tick();
        }
    }

    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
