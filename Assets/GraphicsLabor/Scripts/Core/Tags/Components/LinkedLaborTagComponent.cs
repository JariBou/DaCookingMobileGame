using UnityEngine;

namespace GraphicsLabor.Scripts.Core.Tags.Components
{
    /// <summary>
    /// A Component giving access to LaborTags and allowing for a direct access to a linkedGameObject and/or a linkedMonoBehaviour
    /// </summary>
    [AddComponentMenu("GraphicsLabor/Tags/Linked Labor Tags")]
    public class LinkedLaborTagComponent : BaseLaborTagComponent
    {
        [SerializeField] private GameObject _linkedGameObject;
        [SerializeField] private MonoBehaviour _linkedMonoBehaviour;

        [SerializeField] private bool _linkGameObject;
        [SerializeField] private bool _linkMonoBehaviour;

        /// <summary>
        /// Returns the linked GameObject. If _linkGameObject is set to false returns null
        /// </summary>
        /// <returns></returns>
        public GameObject GetLinkedGameObject() => _linkGameObject ? _linkedGameObject : null;

        /// <summary>
        /// Searches the linked GameObject for a component of type T. If _linkGameObject is set to false returns null
        /// </summary>
        /// <typeparam name="T">The type of the searched component</typeparam>
        /// <returns></returns>
        public T GetGameObjectComponent<T>() where T : Component
        {
            if (!_linkGameObject || _linkedGameObject == null) return null;
            return _linkedGameObject.GetComponent<T>();
        }
        
        /// <summary>
        /// Returns the linked MonoBehaviour. If _linkMonoBehaviour is set to false returns null
        /// </summary>
        /// <returns></returns>
        public MonoBehaviour GetLinkedMonoBehaviour() => _linkMonoBehaviour ? _linkedMonoBehaviour : null;

        /// <summary>
        /// Returns the linked MonoBehaviour cast as T. If _linkMonoBehaviour is set to false returns null
        /// </summary>
        /// <typeparam name="T">The type of the MonoBehaviour to cast to</typeparam>
        /// <returns></returns>
        public T GetLinkedMonoBehaviour<T>() where T : MonoBehaviour
        {
            return _linkMonoBehaviour ? (T) _linkedMonoBehaviour : null;
        }
    }
}