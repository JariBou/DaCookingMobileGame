using _project.Scripts.Core;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace _project.ScriptableObjects.Scripts
{
    [Manageable, Editable]
    public class IngredientSo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private IngredientFamily _ingredientFamily;
        [SerializeField] private Vector3Int _stats;
        [SerializeField] private Vector3Int _randomAddedstats = Vector3Int.zero;
        [SerializeField] private Sprite _icon;
        [SerializeField, TextArea] private string _description;

        public string Name => _name;
        public IngredientFamily Family => _ingredientFamily;
        public Vector3Int Stats => _stats;
        public Vector3Int RandomAddedstats => _randomAddedstats;
        public Sprite Icon => _icon;
        public string Description => _description;
    }
}
