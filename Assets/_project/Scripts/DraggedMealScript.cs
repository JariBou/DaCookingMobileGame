using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts
{
    public class DraggedMealScript : MonoBehaviour, IDraggable
    {
        [FormerlySerializedAs("_lastPhasePhaseScript")] [FormerlySerializedAs("_condimentPhaseScript")] [SerializeField] private LastPhaseScript _lastPhaseScript;
        [SerializeField] private Collider2D _collider2D;
        private Vector3 _initialPosition;
        private bool _usable;


        private void Awake()
        {
            _initialPosition = transform.position;
            DisableUse();
        }

        public void ResetPosition()
        {
            transform.position = _initialPosition;
        }

        public void EnableUse()
        {
            _collider2D.enabled = true;
            _usable = true;
        }

        public void DisableUse()
        {
            _collider2D.enabled = false;
            _usable = false;
        }

        public void EndPhase()
        {
            _lastPhaseScript.EndFeedingPhase();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            DisableUse();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public bool IsActive()
        {
            return _usable;
        }
    }
}
