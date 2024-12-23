using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class PlayerMovementHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float sprintSpeed;

    private bool isSprinting;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.PlayerInputEvents.OnPlayerMove += Move;
        EventChannels.PlayerInputEvents.OnPlayerSprint += SetIsSprinting;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerMove -= Move;
    }

    void SetIsSprinting(bool sprinting)
    {
        isSprinting = sprinting;
    }

    void Move(Vector2 movementVector)
    {
        if (isSprinting)
        {
            rb.MovePosition(rb.position + movementVector * sprintSpeed * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + movementVector * movementSpeed * Time.deltaTime);
        }
    }
}
