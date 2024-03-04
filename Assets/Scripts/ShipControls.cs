using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _speedModifier;
    private float _vertical;
    private float _horizontal;

    [SerializeField] private float _returnRotSpeed;
    private Quaternion _originalRotation;
    [SerializeField] private CinemachineVirtualCamera _thirdPersonCam;
    private float _xBoundary = 10f;
    private float _yBoundary = 6f;

    [SerializeField] private ParticleSystem _spaceParticles;

    // Start is called before the first frame update
    void Start()
    {
        _forwardSpeed = 10f;
        _speedModifier = 15f;
        _rotSpeed = 15f;
        _returnRotSpeed = 5f;
        _originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < 1800)
        {
            ShipMovement();
            MoveCamera();
        }
        else
        {
            _spaceParticles.gameObject.SetActive(false);
        }
    }

    private void ShipMovement()
    {
        //movement
        _vertical = Input.GetAxis("Vertical") * _speedModifier;
        _horizontal = Input.GetAxis("Horizontal") * _speedModifier;

        transform.Translate(new Vector3(_horizontal, _vertical, _forwardSpeed) * Time.deltaTime, Space.World);

       //rotation
       transform.Rotate(new Vector3(-_vertical, 0, -_horizontal) * _rotSpeed * Time.deltaTime, Space.World);
       if (transform.rotation != _originalRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _originalRotation, _returnRotSpeed * Time.deltaTime);
        }

       //boundaries
       if (transform.position.x >= _xBoundary)
        {
            transform.position = new Vector3(_xBoundary, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -_xBoundary)
        {
            transform.position = new Vector3(-_xBoundary, transform.position.y, transform.position.z);
        }
        if (transform.position.y >= _yBoundary)
        {
            transform.position = new Vector3(transform.position.x, _yBoundary, transform.position.z);
        }
        if (transform.position.y <= -_yBoundary)
        {
            transform.position = new Vector3(transform.position.x, -_yBoundary, transform.position.z);
        }


    }

    private void MoveCamera()
    {
        _thirdPersonCam.transform.Translate(Vector3.forward * _forwardSpeed * Time.deltaTime);
    }

    public void IncreaseForwardSpeed(float speed)
    {
        _forwardSpeed += speed;
    }

    public void IncreaseParticleSpeed()
    {
        //var sizeMultiplier = _forwardSpeed * 0.1f;
        //main.startSize = sizeMultiplier;
        var main = _spaceParticles.main;
        main.startSpeed = _forwardSpeed;
        var trails = _spaceParticles.trails;
        trails.ratio += 0.1f;
            
    }

    // private void ShipMovement()
    // {
    //     _vertical = Input.GetAxis("Vertical");
    //     _horizontal = Input.GetAxis("Horizontal");
    //
    //     if (Input.GetKeyDown(KeyCode.T))
    //     {
    //         _currentSpeed++;
    //         if (_currentSpeed > 4)
    //         {
    //             _currentSpeed = 4;
    //         }
    //     }//increase speed
    //
    //     if (Input.GetKeyDown(KeyCode.G))
    //     {
    //         _currentSpeed--;
    //         if (_currentSpeed < 1)
    //         {
    //             _currentSpeed = 1;
    //         }
    //     }//decrease speed
    //
    //     Vector3 rotateH = new Vector3(0, _horizontal, 0);
    //     transform.Rotate(rotateH * _rotSpeed * Time.deltaTime);
    //
    //     Vector3 rotateV = new Vector3(_vertical, 0, 0);
    //     transform.Rotate(rotateV * _rotSpeed * Time.deltaTime);
    //
    //     transform.Rotate(new Vector3(_vertical/2, 0, -_horizontal * 0.2f), Space.Self);
    //
    //     transform.position += transform.forward * _currentSpeed * Time.deltaTime;
    // }
}
