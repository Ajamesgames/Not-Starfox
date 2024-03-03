using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpRings : MonoBehaviour
{
    //increases players forward speed
    //destroy object
    private ShipControls _shipControlsScript;
    private RingSpawnManager _ringSpawnManagerScript;
    private AudioSource _audioSource;
    //[SerializeField] private AudioClip _audioClip;
    [SerializeField] private MeshRenderer[] _meshRenderers;

    private void Start()
    {
        _shipControlsScript = GameObject.FindWithTag("Player").GetComponent<ShipControls>();
        if (_shipControlsScript == null)
        {
            Debug.Log("ship controls script component not found in warp rings script");
        }
        _ringSpawnManagerScript = GameObject.FindWithTag("Ring Spawner").GetComponent<RingSpawnManager>();
        if (_ringSpawnManagerScript == null)
        {
            Debug.Log("ring manager script component not found in warp rings script");
        }
        _audioSource = this.gameObject.GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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

    //TODO: Add destroy boundary, garbage collection
    //TODO: increase noise on camera with each collection
    //TODO: make boundary for when arriving at phase 2
    //TODO: barrel roll animation when going through ring

}
