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
    private bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.PlayerInputEvents.OnPlayerMove += Move;
        EventChannels.PlayerInputEvents.OnPlayerSprint += SetIsSprinting;
        EventChannels.PlayerInputEvents.OnSetMovement += SetMovement;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerMove -= Move;
        EventChannels.PlayerInputEvents.OnPlayerSprint -= SetIsSprinting;
        EventChannels.PlayerInputEvents.OnSetMovement -= SetMovement;
    }

    void SetIsSprinting(bool sprinting)
    {
        isSprinting = sprinting;
    }

    void SetMovement(bool canMove)
    {
        this.canMove = canMove;
    }

    void Move(Vector2 movementVector)
    {
        if (!canMove)
            return;
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
