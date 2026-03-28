using System;
using _Project.Sources.Gameplay;
using _Project.Sources.UI;
using UnityEngine;
using VContainer;

namespace _Project.Sources.GameLoop
{
    public class GameFinisher : IDisposable
    {
        public event Action Finished;

        private GamePause _gamePause;
        private Player _player;

        [Inject]
        public void Construct(GamePause gamePause) => _gamePause = gamePause;

        public void Init(Player player)
        {
            player.Death += Finish;
            _player = player;
        }

        private void Finish()
        {
            Finished?.Invoke();
            _gamePause.SetPause(true);
        }

        public void Dispose() => _player.Death -= Finish;
    }
}