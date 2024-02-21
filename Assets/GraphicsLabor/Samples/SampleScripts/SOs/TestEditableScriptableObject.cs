using System;
using System.Collections.Generic;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace GraphicsLabor.Samples.SampleScripts.SOs
{
    [Manageable, Editable]
    public class TestEditableScriptableObject : ScriptableObject
    {
        public string testString = "test";
        public int testInt = 20;
        public SerializableTestClass SerializableTestClass;
        [TabProperty("First Tab", "Second Tab")]public int testInt2 = 25;
        [TabProperty("First Tab")]public float testFloat = 56.2f;
        [ShowProperty] public float testProp2 => testFloat;
        public List<string> testList;
        [Expandable, TabProperty("Second Tab")] public AnotherEditableScriptableObject TestSO;
        [TabProperty("Third Tab", "First Tab"), ShowProperty] public float testProp => testFloat;
        [TabProperty("Second Tab")]public GameObject testGameObject;
        [TabProperty("Second Tab")]public SerializableTestClass SerializableTestClass2;
    }

    [Serializable]
    public class SerializableTestClass
    {
        public int testInt;
        public SerializableTestClassButDifferent SerializableTestClassButDifferent;
    }
    
    [Serializable]
    public class SerializableTestClassButDifferent
    {
        public int testInt;
        public string testString;
        public float testFloat;
        public int testInt2;
        public List<int> testListInt = new();
        [Expandable]public AnotherEditableScriptableObject testezrr;

    }
}
