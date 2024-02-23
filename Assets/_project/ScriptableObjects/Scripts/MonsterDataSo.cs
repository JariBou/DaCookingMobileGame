using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace _project.ScriptableObjects.Scripts
{
    [Manageable, Editable]
    public class MonsterDataSo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
        
        [SerializeField, TabProperty("Stats Config")] private Vector3 _statsMin;
        [SerializeField, TabProperty("Stats Config")] private Vector3 _statsMax;
        [SerializeField, TabProperty("Stats Config")] private MonsterBundlesSo _bundles;

        [SerializeField, TabProperty("Sprites")] private Sprite _icon;
        [SerializeField, TabProperty("Sprites")] private Sprite _sleepingSprite;
        [SerializeField, TabProperty("Sprites")] private Sprite _normalSprite;
        [SerializeField, TabProperty("Sprites")] private Sprite _angrySprite;
        
        // TODO: hum.... this probably shouldn't stay here but whatever we'll see later
        [ShowProperty] public Vector3 CurrentStats { get; set; }

        public string Name => _name;
        public string Description => _description;
        
        public Vector3 StatsMin => _statsMin;
        public Vector3 StatsMax => _statsMax;
        public MonsterBundlesSo Bundles => _bundles;
        
        public Sprite Icon => _icon;
        public Sprite SleepingSprite => _sleepingSprite;
        public Sprite NormalSprite => _normalSprite;
        public Sprite AngrySprite => _angrySprite;
    }
}