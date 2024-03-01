using System.Collections;
using System.Collections.Generic;
using _project.Scripts;
using UnityEngine;

public class LastPhaseScript : MonoBehaviour
{

    [SerializeField] private CameraScript _camera;


    public void GoNextPhase()
    {
        _camera.NextPhase();
    }

}
