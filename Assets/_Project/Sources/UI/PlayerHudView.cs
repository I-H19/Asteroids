using System.Text;
using TMPro;
using UnityEngine;

namespace Asteroids
{
    public class PlayerHudView : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI PlayerStats { get; private set; }

        private readonly StringBuilder _hudBuilder = new();

        public void ChangeHUDStats(PlayerStats playerStats)
        {
            _hudBuilder.Clear();

            _hudBuilder.AppendLine($"Coords: {playerStats.PlayerPosition}");
            _hudBuilder.AppendLine($"Angle: {playerStats.Angle}");
            _hudBuilder.AppendLine($"Speed: {playerStats.Speed}");
            _hudBuilder.AppendLine($"LaserAmmo: {playerStats.TotalAmmo}/{playerStats.MaxChargesNumber}");
            _hudBuilder.AppendLine($"LaserReloadTime: {playerStats.ReloadingTime}");
            _hudBuilder.AppendLine($"LaserCooldown: {playerStats.ShootingCooldown}");

            PlayerStats.SetText(_hudBuilder.ToString());
        }
    }
}