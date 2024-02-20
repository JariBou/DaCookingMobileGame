using System;
using System.Collections.Generic;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class TabPropertyAttribute : DrawerAttribute
    {
        // TODO: adapt to regularInspector maybe?
        public readonly string[] TabNames;
        
        /// <summary>
        /// Shows a field or property on a tab on a ScriptableObject EditorWindow or CreatorWindow
        /// </summary>
        /// <param name="tabName">Name of the tab</param>
        /// <param name="tabNames">Other tabs to also display the field or property in</param>
        public TabPropertyAttribute(string tabName, params string[] tabNames)
        {
            List<string> tempList = new List<string> { tabName };
            tempList.AddRange(tabNames);
            TabNames = tempList.ToArray();
        }
    }
    
}