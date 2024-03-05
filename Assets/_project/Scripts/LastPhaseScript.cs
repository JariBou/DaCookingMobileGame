using UnityEngine;

namespace _project.Scripts
{
    public class LastPhaseScript : MonoBehaviour
    {

        [SerializeField] private CameraScript _camera;


        public void GoNextPhase()
        {
            _camera.NextPhase();
        }

    }
}
