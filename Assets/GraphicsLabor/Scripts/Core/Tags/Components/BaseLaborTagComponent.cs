using GraphicsLabor.Scripts.Core.Utility;
using UnityEngine;

namespace GraphicsLabor.Scripts.Core.Tags.Components
{
    public abstract class BaseLaborTagComponent : MonoBehaviour, ITagHolder
    {
        [SerializeField] private LaborTags _tags;

        public LaborTags GetLaborTags()
        {
            return _tags;
        }

        /// <summary>
        /// Override this method if you want to Use the OnValidate Method
        /// </summary>
        protected virtual void OnSelfValidate(){}

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (GetComponents<ITagHolder>().Length > 1)
            {
                GLogger.LogWarning($"GameObject {nameof(gameObject)} has more than 1 ITagHolder Component, this may cause unwanted behaviour");
            }
            OnSelfValidate();
        }
        #endif
    }
}