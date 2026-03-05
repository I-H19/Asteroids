using UnityEngine;
using VContainer;

public class GameFinisher : MonoBehaviour
{
    private UIElementsHolder _uiElementsHolder;
    private GamePause _gamePause;

    [Inject]
    public void Construct(UIElementsHolder uIElementsHolder, GamePause gamePause, Player player)
    {
        _uiElementsHolder = uIElementsHolder;
        _gamePause = gamePause;

        player.Death += Finish;
    }

    public void Finish()
    {
        _uiElementsHolder.RestartGamePanel.SetActive(true);
        _gamePause.SetPause(true);
    }
}
