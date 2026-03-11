using System;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.WeaponSystem;
using TMPro;
using VContainer;

namespace _Project.Sources.UI
{
    public class UIElementsUpdater : IDisposable
    {
        private PlayerMovementData _movementData;
        private PlayerScore _playerScore;
        private Laser _laser;
        private Player _player;
        private TextMeshProUGUI _textScore;
        private TextMeshProUGUI _textScoreEnd;
        private TextMeshProUGUI _textMovementData;

        [Inject]
        public void Construct(UIElementsHolder elementsHolder, PlayerMovementData movementData, PlayerScore playerScore,
            Player player, Laser laser)
        {
            _laser = laser;
            _player = player;

            _textScore = elementsHolder.PlayerScoreGameText;
            _textScoreEnd = elementsHolder.PlayerScoreResultText;
            _textMovementData = elementsHolder.PlayerMovementDataText;

            _movementData = movementData;
            _playerScore = playerScore;
        }

        public void Subscribe()
        {
            _movementData.Changed += OnMovementDataChanged;
            _playerScore.Changed += OnScoreChanged;
        }

        public void Unsubscribe()
        {
            _movementData.Changed -= OnMovementDataChanged;
            _playerScore.Changed -= OnScoreChanged;
        }

        private void OnScoreChanged()
        {
            _textScore.text = $"GameScore: {_playerScore.Score}";
            _textScoreEnd.text = $"GameScore: {_playerScore.Score}";
        }

        private void OnMovementDataChanged()
        {
            _textMovementData.text =
                $"Coords: {_player.RigidBodyTemplate.position}\n" +
                $"Angle: {_movementData.CurrentAngle}\n" +
                $"Speed: {_movementData.CurrentSpeed}\n" +
                $"LaserAmmo: {_laser.TotalAmmo}/{_laser.MaxChargesNumber}\n" +
                $"LaserReloadTime: {_laser.ReloadingTime}\n" +
                $"LaserCooldown: {_laser.ShootingCooldown}";
        }

        public void Dispose() => Unsubscribe();
    }
}