using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawnManager : MonoBehaviour
{
    private float _xBoundary = 10f;
    private float _yBoundary = 6f;
    [SerializeField] private GameObject _warpRing;
    [SerializeField] private GameObject _arwingPlayer;
    private float _zPosIncrease = 50f;

    void Start()
    {
        StartCoroutine(SpawnRoutineRings());
    }

    private void InstantiateWarpRing()
    {
        float randomXPos = Random.Range(-_xBoundary, _xBoundary);
        float randomYPos = Random.Range(-_yBoundary, _yBoundary);
        Vector3 arwingPos = _arwingPlayer.transform.position;
        float zPosOffset = _zPosIncrease; 

        Instantiate(_warpRing, new Vector3(randomXPos, randomYPos,  arwingPos.z + zPosOffset), Quaternion.identity);
    }

    private IEnumerator SpawnRoutineRings()
    {
        while (_arwingPlayer.transform.position.z < 1800)
        {
            float randomTime = Random.Range(2, 4);
            yield return new WaitForSeconds(randomTime);
            InstantiateWarpRing();
        }
    }
 
    public void ZPosIncrease(float increase)
    {
        _zPosIncrease += increase;
    }
}
