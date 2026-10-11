using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandHandler : MonoBehaviour
{
    [SerializeField]
    private Sprite handSprite;
    [SerializeField]
    private SpriteRenderer leftHandRenderer;
    [SerializeField] 
    private SpriteRenderer rightHandRenderer;

    private WeaponData weaponData;
    // Start is called before the first frame update
    void Start()
    {
        Transform myParent = transform.parent;
        weaponData = myParent.GetComponentInParent<PlayerWeaponInventoryHandler>().GetPrimaryWeapon();
        rightHandRenderer.sprite = handSprite;

        SetHandPositions(weaponData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetHandPositions(WeaponData weaponData)
    {
        rightHandRenderer.gameObject.transform.localPosition = weaponData.RightHandPosition;
        if (!weaponData.IsOneHanded)
        {
            leftHandRenderer.gameObject.transform.localPosition = weaponData.LeftHandPosition;
            leftHandRenderer.sprite = handSprite;
        }
        else
        {
            leftHandRenderer.sprite = null;
        }

    }
}
