using UnityEngine;

namespace _project.Scripts
{
    public class CondimentPhaseScript : MonoBehaviour
    {
        [SerializeField] private CookingManager _cookingManager;

        public CookingManager Manager => _cookingManager;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void GoNextPhase()
        {
            _cookingManager.Camera.NextPhase();
        }
    }
}
