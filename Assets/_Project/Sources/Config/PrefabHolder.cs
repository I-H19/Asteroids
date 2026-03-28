using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.Enemy;
using _Project.Sources.Gameplay.WeaponSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Sources.Config
{
    [CreateAssetMenu(fileName = "PrefabHolder", menuName = "Settings/PrefabHolder")]
    public class PrefabHolder : ScriptableObject
    {
        [Header("Gameplay")]
        
        [field: SerializeField] public Bullet BulletTemplate { get; private set; }
        [field: SerializeField] public Asteroid AsteroidTemplate { get; private set; }
        [field: SerializeField] public AsteroidFragment AsteroidFragmentTemplate { get; private set; }
        [field: SerializeField] public UFO UfoTemplate { get; private set; }
        
        [field: Space]
        
        [Header("UI")]
        [field: SerializeField] public Canvas RootCanvas { get; private set; }
        [field: SerializeField] public TextMeshProUGUI PlayerRuntimeScore { get; private set; }
        [field: SerializeField] public TextMeshProUGUI PlayerResultScore { get; private set; }
        [field: SerializeField] public TextMeshProUGUI PlayerStats { get; private set; }
        [field: SerializeField] public Image RestartPanel { get; private set; }
        [field: SerializeField] public Button RestartGameButton { get; private set; }
        [field: SerializeField] public EventSystem EventSystemTemplate { get; private set; }
        
        
        [field: Space]
        
        [Header("Player")]
        [field: SerializeField] public Player PlayerRoot { get; private set; }
        [field: SerializeField] public SpriteRenderer PlayerVisual { get; private set; }
        [field: SerializeField] public Laser LaserTemplate { get; private set; }
        [field: SerializeField] public LaserVisual LaserVisualTemplate { get; private set; }
    }
}
