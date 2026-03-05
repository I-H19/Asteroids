using UnityEngine;
using VContainer;

public class GameRestarter : MonoBehaviour
{
    private GamePause _gamePause;
    private EnemyRegistry _enemiesRegistry;
    private PlayerScore _playerScore;
    private Player _player;

    [Inject]
    public void Construct(Player player, PlayerScore playerScore, EnemyRegistry enemiesRegistry, GamePause gamePause)
    {
        _gamePause = gamePause;
        _enemiesRegistry = enemiesRegistry;
        _playerScore = playerScore;
        _player = player;
    }

    public void RestartGame()
    {
        _playerScore.ResetScore();
        _enemiesRegistry.KillAll();
        _player.ResetPlayer();
        _gamePause.SetPause(false);
    }
}
