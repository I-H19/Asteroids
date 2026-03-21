using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Sources.Config
{
    [CreateAssetMenu(fileName = "PrefabHolder", menuName = "Settings/PrefabHolder")]
    public class PrefabHolder : ScriptableObject
    {
        [Header("Gameplay")]
        
        [field: SerializeField] public GameObject Bullet { get; private set; }
        [field: SerializeField] public GameObject Asteroid { get; private set; }
        [field: SerializeField] public GameObject AsteroidFragment { get; private set; }
        [field: SerializeField] public GameObject UFO { get; private set; }
        
        [field: Space]
        
        [Header("UI")]
        [field: SerializeField] public GameObject RootCanvas { get; private set; }
        [field: SerializeField] public GameObject PlayerRuntimeScore { get; private set; }
        [field: SerializeField] public GameObject PlayerResultScore { get; private set; }
        [field: SerializeField] public GameObject PlayerStats { get; private set; }
        [field: SerializeField] public GameObject RestartPanel { get; private set; }
        [field: SerializeField] public GameObject RestartGameButton { get; private set; }
        [field: SerializeField] public GameObject EventSystem { get; private set; }
        
        
        [field: Space]
        
        [Header("Player")]
        [field: SerializeField] public GameObject PlayerRoot { get; private set; }
        [field: SerializeField] public GameObject PlayerVisual { get; private set; }
        [field: SerializeField] public GameObject Laser { get; private set; }
        [field: SerializeField] public GameObject LaserVisual { get; private set; }
    }
}
