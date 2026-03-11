using UnityEngine;

namespace _Project.Sources.Settings
{
    public class SceneObjectHolder : MonoBehaviour
    {
        [field: SerializeField] public GameObject LaserVisual { get; private set; }
    }
}