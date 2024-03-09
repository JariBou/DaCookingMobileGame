using System;
using _project.Scripts.Core;
using UnityEngine;

namespace _project.Scripts
{
    public class BossScript : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private Animator _bossAnimator;
        private static readonly int Phase = Animator.StringToHash("Phase");

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

        public void SetState(BossState state)
        {
            _bossAnimator.SetInteger(Phase, (int)state);
        }
        
    }
}
