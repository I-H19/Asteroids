using UnityEngine;

namespace Asteroids.Settings
{
    public class SceneObjectHolder : MonoBehaviour
    {
        [field: SerializeField] public GameObject Player { get; private set; }
        [field: SerializeField] public GameObject LaserVisual { get; private set; }
    }
}