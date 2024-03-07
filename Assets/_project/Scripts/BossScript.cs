using System;
using UnityEngine;

namespace _project.Scripts
{
    public class BossScript : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;

        private void Start()
        {
            DeactivateFeeding();
        }

        public void ActivateFeeding()
        {
            _collider2D.enabled = true;
        }
    
        public void DeactivateFeeding()
        {
            _collider2D.enabled = false;
        }
    }
}
