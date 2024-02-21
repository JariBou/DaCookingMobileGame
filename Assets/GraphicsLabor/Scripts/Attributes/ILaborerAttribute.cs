using System;
using UnityEngine;

namespace GraphicsLabor.Scripts.Attributes
{
    /// <summary>
    /// Interface for all Laborer Attributes
    /// </summary>
    public interface ILaborerAttribute
    {
    }

    /// <summary>
    /// Abstract class regrouping all Attributes that use a Drawer
    /// </summary>
    public abstract class DrawerAttribute : PropertyAttribute, ILaborerAttribute
    {
    }

    /// <summary>
    /// Abstract class regrouping all Attributes aimed at ScriptableObjects
    /// </summary>
    public abstract class ScriptableObjectAttribute : Attribute, ILaborerAttribute
    {
    }

    /// <summary>
    /// Abstract class regrouping all Attributes that are used by the customInspector
    /// </summary>
    public abstract class InspectedAttribute : Attribute, ILaborerAttribute
    {
    }

    
    
}
