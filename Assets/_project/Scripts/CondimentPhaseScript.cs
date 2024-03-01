using UnityEngine;

namespace _project.Scripts
{
    public class CondimentPhaseScript : MonoBehaviour
    {
        [SerializeField] private CookingManager _cookingManager;

        public CookingManager Manager => _cookingManager;


        public void GoNextPhase()
        {
            _cookingManager.Camera.NextPhase();
        }
    }
}
