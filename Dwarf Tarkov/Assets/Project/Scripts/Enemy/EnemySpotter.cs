using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpotter : MonoBehaviour
{
    private EnemyFieldOfView fov;
    private GameObject player;

    private void Start()
    {
        fov = GetComponent<EnemyFieldOfView>();
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        CheckIfPlayerSeen();
    }

    bool CheckIfPlayerSeen()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < fov.GetDistance()) {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.right, dirToPlayer) < fov.GetFOVAngle() / 2)
            {
                Debug.Log("seeing player");
            }
        }
        return true;
    }
}
