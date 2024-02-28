﻿using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace _project.ScriptableObjects.Scripts
{
    [Manageable, Editable]
    public class CondimentSo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Vector3Int _value;
        [SerializeField] private Sprite _icon;
        [SerializeField, TextArea] private string _description;

        public string Name => _name;
        public Vector3Int Value => _value;
        public Sprite Icon => _icon;
        public string Description => _description;
    }
}