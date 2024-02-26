using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cockPitCam;
    [SerializeField] private CinemachineVirtualCamera _thirdPersonCam;
    [SerializeField] private CinemachineBlendListCamera _cinematicCam;
    [SerializeField] private bool _noInputMadeForFiveSeconds = false;
    [SerializeField] private float _timeRemainingForCinematic;
    private Vector3 _lastMousePosition = Vector3.zero;

    private void Start()
    {
        _cockPitCam.Priority = 11;
        _timeRemainingForCinematic = 5f;
        
    }

    private void Update()
    {
        SwapCameras();
        CountDownCinematicTimer();
        EnableCinematicCamera();
    }
    private void LateUpdate()
    {
        _lastMousePosition = Input.mousePosition;
    }

    private void SwapCameras()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_cockPitCam.Priority >= 11)
            {
                _cockPitCam.Priority = 10;
                _thirdPersonCam.Priority = 11;
            }
            else if (_thirdPersonCam.Priority >= 11)
            {
                _thirdPersonCam.Priority = 10;
                _cockPitCam.Priority = 11;
            }
        }
    }

    private void EnableCinematicCamera()
    {
        if (_noInputMadeForFiveSeconds == true)
        {
            _cinematicCam.Priority = 12;
        }
        else
        {
            _cinematicCam.Priority = 10;
        }
    }

    private void CountDownCinematicTimer()
    {
        _timeRemainingForCinematic -= 1 * Time.deltaTime;
        if (Input.anyKey || Input.mousePosition != _lastMousePosition)
        {
            _timeRemainingForCinematic = 5f;
            _noInputMadeForFiveSeconds = false;
        }
        if (_timeRemainingForCinematic <= 0)
        {
            _noInputMadeForFiveSeconds = true;
        }
    }
    

}
