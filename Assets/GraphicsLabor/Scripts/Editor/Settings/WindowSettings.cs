using System;
using System.Collections.Generic;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Editor.Windows;
using UnityEngine;

namespace GraphicsLabor.Scripts.Editor.Settings
{
    public sealed class WindowSettings : ScriptableObject
    {
        [SerializeField, ReadOnly] public List<WindowBase> _openedCustomWindows = new();
        public List<WindowBase> OpenedCustomWindows => _openedCustomWindows;


        /// <summary>
        /// Finds the first opened window that matches the predicate
        /// </summary>
        /// <param name="predicate">A Function that acts as a filter</param>
        /// <returns>The WindowBase found</returns>
        public WindowBase FindWindowWhere(Func<WindowBase, bool> predicate)
        {
            WindowBase window = _openedCustomWindows.Find(predicate.Invoke);
            return window != default ? window : null;
        }
        
        /// <summary>
        /// Finds the first opened window that matches the predicate and casts it as T
        /// </summary>
        /// <param name="predicate">A Function that acts as a filter</param>
        /// <typeparam name="T">The type to cast the window as</typeparam>
        /// <returns>The WindowBase found cast as T</returns>
        public T FindWindowWhere<T>(Func<WindowBase, bool> predicate) where T : WindowBase
        {
            WindowBase window = _openedCustomWindows.Find(predicate.Invoke);
            return window != default ? (T)window : null;
        }
    }
}