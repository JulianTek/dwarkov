using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAimManager : MonoBehaviour
{
    private bool isPaused;
    private bool isAiming = true;
    private Vector2 pointer;
    private WeaponData data;

    [SerializeField]
    private SpriteRenderer gunSprite;
    [SerializeField]
    private SpriteRenderer leftHand;
    [SerializeField]
    private SpriteRenderer rightHand;

    private Vector2 leftHandFlipPos;
    private Vector2 rightHandFlipPos;

    private void Awake()
    {
        EventChannels.PlayerInputEvents.OnPlayerAim += Aim;
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerAim -= Aim;
    }

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<PlayerWeaponInventoryHandler>().GetPrimaryWeapon();
        if (data == null)
        {
            data = EventChannels.DatabaseEvents.OnGetWeaponData?.Invoke("MKM-14");
        }
        leftHandFlipPos = data.LeftHandPosition + new Vector2(0, -0.1f);
        Debug.Log(leftHandFlipPos);
        rightHandFlipPos = data.RightHandPosition + new Vector2(0, -0.1f);
        Debug.Log(rightHandFlipPos);
    }

    private void Aim(Vector2 aimVector)
    {
        pointer = aimVector;
    }

    private void FixedUpdate()
    {
        if (isAiming)
        {
            // Translate mouse position to on-screen point and turn weapon to there
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(pointer);
            mousePos.z = 0f;
            Vector3 direction = (mousePos - transform.position).normalized;

            // Calculate the angle between player's forward vector and the aiming direction
            float angle = Vector3.Angle(Vector3.right, direction);

            // Check if the mouse is above or below the player
            if (direction.y < 0)
            {
                angle = 360f - angle; // Adjust the angle for the correct rotation
            }

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Flip gun sprite if aiming left of center
            gunSprite.flipY = (mousePos.x < transform.position.x);

            leftHand.flipY = (mousePos.x < transform.position.x);
            // Sets position depending on if the sprite is flipped
            leftHand.transform.localPosition = leftHand.flipY ? data.LeftHandPosition : leftHandFlipPos;

            rightHand.flipY = (mousePos.x < transform.position.x);
            // Sets position depending on if the sprite is flipped
            rightHand.transform.localPosition = rightHand.flipY ? data.RightHandPosition : rightHandFlipPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAiming(bool aiming)
    {
        isAiming = aiming;
    }
}
