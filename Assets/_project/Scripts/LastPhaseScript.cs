using UnityEngine;

namespace _project.Scripts
{
    public class LastPhaseScript : MonoBehaviour
    {

        [SerializeField] private CameraScript _camera;
        [SerializeField] private ReRoll _reRoll;

        public void GoNextPhase()
        {
            _reRoll.RedistributeCards();
            _camera.NextPhase();
        }

    }
}
