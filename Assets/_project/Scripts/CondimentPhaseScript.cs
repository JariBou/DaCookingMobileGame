using UnityEngine;

namespace _project.Scripts
{
    public class CondimentPhaseScript : MonoBehaviour
    {
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private GameObject _monsterInfoDisplay;
        [SerializeField] private DraggedMealScript _draggedMeal;

        public CookingManager Manager => _cookingManager;


        public void GoNextPhase()
        {
            _draggedMeal.ResetPosition();
            _draggedMeal.Activate();
            _cookingManager.Camera.NextPhase();
            _monsterInfoDisplay.SetActive(false);
        }
    }
}
