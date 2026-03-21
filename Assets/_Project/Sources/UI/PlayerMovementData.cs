using System;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using UnityEngine;
using VContainer;

namespace _Project.Sources.UI
{
    public class PlayerStats
    {
        public Vector2 PlayerPosition { get; private set; }
        public float Angle { get; private set; }
        public float Speed { get; private set; }
        public int TotalAmmo { get; private set; }
        public int MaxChargesNumber { get; private set; }
        public float ReloadingTime { get; private set; }
        public float ShootingCooldown { get; private set; }

        
        public void ChangePosition(Vector2 position) => PlayerPosition = position;
        public void ChangeAngle(float angle) => Angle = angle;
        public void ChangeSpeed(float speed) => Speed = speed;
        public void ChangeTotalAmmo(int totalAmmo) => TotalAmmo = totalAmmo;
        public void ChangeMaxCharges(int maxCharges) => MaxChargesNumber = maxCharges;
        public void ChangeReloadingTime(float reloadingTime) => ReloadingTime = reloadingTime;
        public void ChangeShootingCooldown(float cooldown) => ShootingCooldown =  cooldown;
    }
}