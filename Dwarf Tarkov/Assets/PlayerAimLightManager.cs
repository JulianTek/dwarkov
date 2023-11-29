using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunkyCode;
using EventSystem;

public class PlayerAimLightManager : MonoBehaviour
{
    [SerializeField]
    private Light2D playerLight;
    [SerializeField]
    private float additionalRotation = -90f;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.PlayerInputEvents.OnPlayerAim += Aim;
    }

    private void OnDisable()
    {
        EventChannels.PlayerInputEvents.OnPlayerAim -= Aim;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Aim(Vector2 aimVector) {

        Vector3 targetPoint = Camera.main.ScreenToWorldPoint(new Vector3(aimVector.x, aimVector.y));
        Vector3 direction = targetPoint - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + additionalRotation;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }
}
