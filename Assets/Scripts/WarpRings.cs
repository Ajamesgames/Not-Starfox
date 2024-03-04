using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WarpRings : MonoBehaviour
{
    private GameObject _playerObject;
    private ShipControls _shipControlsScript;
    private RingSpawnManager _ringSpawnManagerScript;
    private AudioSource _audioSource;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    private CinemachineVirtualCamera _thirdPersonCam;
    private CinemachineBasicMultiChannelPerlin _thirdPersonCamNoise;


    private void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
        if (_playerObject == null)
        {
            Debug.Log("player object not found in warp rings script");
        }
        _shipControlsScript = _playerObject.GetComponent<ShipControls>();
        if(_shipControlsScript == null)
        {
            Debug.Log("cant find player control script");
        }
        _ringSpawnManagerScript = GameObject.FindWithTag("Ring Spawner").GetComponent<RingSpawnManager>();
        if (_ringSpawnManagerScript == null)
        {
            Debug.Log("ring manager script component not found in warp rings script");
        }
        _audioSource = this.gameObject.GetComponent<AudioSource>();
        _thirdPersonCam = GameObject.Find("ThirdPerson_VCam").GetComponent<CinemachineVirtualCamera>();
        if (_thirdPersonCam == null)
        {
            Debug.Log("Cant find third person cam in WarpRings script");
        }
        else
        {
            _thirdPersonCamNoise = _thirdPersonCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }


        Destroy(this.gameObject, 10f);
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(BarrelRoll(0.5f)); 
            _thirdPersonCamNoise.m_AmplitudeGain += 0.1f;
            _shipControlsScript.IncreaseForwardSpeed(5f);
            _ringSpawnManagerScript.ZPosIncrease(5f);
            _audioSource.Play();
            foreach (var m in _meshRenderers)
            {
                m.gameObject.SetActive(false);
            }
            _shipControlsScript.IncreaseParticleSpeed();
            Destroy(this.gameObject, 2f);
        }
    }

    private IEnumerator BarrelRoll(float duration)
    {
        float elapsedTime = 0f;
        float startRotation = _playerObject.transform.eulerAngles.z;
        float endRotation = startRotation + 360f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / duration) % 360f;

            // Apply the rotation to the object
            _playerObject.transform.eulerAngles = new Vector3(_playerObject.transform.eulerAngles.x, _playerObject.transform.eulerAngles.y, currentRotation);

            yield return null;
        }
    }

}
