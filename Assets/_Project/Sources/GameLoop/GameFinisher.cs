using System;
using _Project.Sources.Gameplay;
using _Project.Sources.UI;
using UnityEngine;
using VContainer;

namespace _Project.Sources.GameLoop
{
    public class GameFinisher : IDisposable
    {
        private UIElementsHolder _uiElementsHolder;
        private GamePause _gamePause;
        private Player _player;

        [Inject]
        public void Construct(GamePause gamePause, UIElementsHolder uIElementsHolder)
        {
            _gamePause = gamePause;
            _uiElementsHolder = uIElementsHolder;
        }


        public void Init(Player player)
        {
            player.Death += Finish;
            _player = player;
        }

        private void Finish()
        {
            _uiElementsHolder.RestartGamePanel.SetActive(true);
            _gamePause.SetPause(true);
        }

        public void Dispose() => _player.Death -= Finish;
    }
}