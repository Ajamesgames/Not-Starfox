using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawnManager : MonoBehaviour
{
    private float _xBoundary = 10f;
    private float _yBoundary = 6f;
    [SerializeField] private GameObject _warpRing;
    [SerializeField] private GameObject _arwingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            InstantiateWarpRing();
        }
    }

    private void InstantiateWarpRing()
    {
        float randomXPos = Random.Range(-_xBoundary, _xBoundary);
        float randomYPos = Random.Range(-_yBoundary, _yBoundary);
        Vector3 arwingPos = _arwingPlayer.transform.position;
        float zPosOffset = 50f; //may need dynamic variable here instead of value

        Instantiate(_warpRing, new Vector3(randomXPos, randomYPos,  arwingPos.z + zPosOffset), Quaternion.identity);
    }

    //need spawn routine
}
