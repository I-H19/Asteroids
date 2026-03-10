using UnityEngine;

namespace _Project.Sources.Settings
{
    public class PrefabHolder : MonoBehaviour
    {
        [field: SerializeField] public GameObject Bullet { get; private set; }
        [field: SerializeField] public GameObject Asteroid { get; private set; }
        [field: SerializeField] public GameObject AsteroidFragment { get; private set; }
        [field: SerializeField] public GameObject UFO { get; private set; }
    }
}