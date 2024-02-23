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
        [SerializeField] private Vector3 _stats;
        [SerializeField] private Sprite _icon;
        [SerializeField, TextArea] private string _description;

        public string Name => _name;
        public IngredientFamily Family => _ingredientFamily;
        public Vector3 Stats => _stats;
        public Sprite Icon => _icon;
        public string Description => _description;
    }
}
