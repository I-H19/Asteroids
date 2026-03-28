using UnityEngine;

namespace _Project.Sources.Gameplay.WeaponSystem
{
    public class LaserVisual : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
