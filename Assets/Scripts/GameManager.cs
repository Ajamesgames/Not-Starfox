using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource _barrelRollSource;

    private void Update()
    {
        QuitGameButton();
        DoABarrelRoll();
    }

    private void QuitGameButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void DoABarrelRoll()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            _barrelRollSource.Play();
        }
    }
}
