using System.Collections.Generic;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace GraphicsLabor.Samples.SampleScripts.SOs
{
    [Manageable, Editable]
    public class AnotherEditableScriptableObject : ScriptableObject
    {
        public string test;
        public int test2 = 1;
        public List<string> testList;
        [ShowProperty] public int TestProp => test2;
    }
}
