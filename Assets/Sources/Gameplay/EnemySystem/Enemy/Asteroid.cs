using System;
using UnityEngine;

[RequireComponent(typeof(DirectionalMover))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyLife))]
[RequireComponent(typeof(EnemyDamageSource))]
public class Asteroid : MonoBehaviour, IEnemy, ISceneTickable
{
    public Action<IEnemy> Killed { get; set; }
    public GameObject EnemyGameObject { get; private set; }
    public DirectionalMoverSettings MoverSettings { get; private set; }

    public IMover Mover { get; private set; }

    private Rigidbody2D _rigidbody2D;
    private EnemyLife _enemyLife;
    private ScreenTeleporter _screenTeleporter;

    public void Init(GameObject gameObject, DirectionalMoverSettings moverSettings, float damageCount)
    {
        EnemyGameObject = gameObject;
        MoverSettings = moverSettings;

        float randomAngleDegrees = UnityEngine.Random.Range(0f, 360f);
        EnemyGameObject.transform.rotation = Quaternion.Euler(0f, 0f, randomAngleDegrees);

        _enemyLife = GetComponent<EnemyLife>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Mover = GetComponent<DirectionalMover>();
        Mover.Init(MoverSettings, _rigidbody2D);
        Mover.SetMoving(true);

        EnemyDamageSource damageSource = GetComponent<EnemyDamageSource>();
        damageSource.Init(damageCount);

        _enemyLife.OnDeath += OnDeath;

        _screenTeleporter = GetComponent<ScreenTeleporter>();
        _screenTeleporter.Init();
    }


    private void OnDeath()
    {
        Killed?.Invoke(this);
    }

    public void Tick()
    {
        Mover.Move();
        _screenTeleporter.Tick();
    }

    public void Kill()
    {
        Destroy(EnemyGameObject);
    }
}
