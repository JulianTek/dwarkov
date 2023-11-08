using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPlayerFinder : MonoBehaviour
{
    private GameObject player;
    private CinemachineVirtualCamera virtualCamera;
    private bool playerSpawned = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerSpawned)
        {
            player = GameObject.FindWithTag("Player");
        }
        else
        {
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;
        }
        Debug.Log(player);
    }
}
