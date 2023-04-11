using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using TMPro;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private WeaponData data;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject magazinePrefab;
    [SerializeField]
    private TextMeshProUGUI magText;
    private SpriteRenderer gunSprite;

    private AmmoSubtype ammoTypeLoaded;

    private int maxMagCount;
    private int currentMagCount;

    private Vector2 pointer;
    private float timeSinceLastShot = 0f;

    private bool isAiming = true;
    private bool isFiring = false;
    private bool canFire = true;
    private bool canReload = true;

    private void Start()
    {
        EventChannels.PlayerInputEvents.OnPlayerShootStarted += StartShooting;
        EventChannels.PlayerInputEvents.OnPlayerShootFinished += StopShooting;
        EventChannels.PlayerInputEvents.OnPlayerReload += Reload;
        EventChannels.PlayerInputEvents.OnPlayerAim += Aim;

        maxMagCount = data.MagCapacity;
        currentMagCount = 0;
        gunSprite = GetComponentInChildren<SpriteRenderer>(); 
    }

    void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerShootStarted -= StartShooting;
        EventChannels.PlayerInputEvents.OnPlayerShootFinished -= StopShooting;
        EventChannels.PlayerInputEvents.OnPlayerReload -= Reload;
        EventChannels.PlayerInputEvents.OnPlayerAim -= Aim;
    }

    private void Aim(Vector2 aimVector)
    {
        pointer = aimVector;
    }

    private void Update()
    {
        magText.text = $"{currentMagCount}/{maxMagCount}";
        if (isAiming)
        {
            // Get the position of the mouse cursor in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(pointer);
            mousePos.z = 0f;

            // Calculate the direction the gun should face
            Vector3 direction = (mousePos - transform.position).normalized;

            // Calculate the angle the gun should rotate to face the mouse cursor
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the gun to face the mouse cursor
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Debug.Log(mousePos.x);
            if (mousePos.x < 0)
            {
                Debug.Log("Mouse on left half of screen");
                gunSprite.flipY = true;
            }
            else
            {
                Debug.Log("Mouse on right half of screen");
                gunSprite.flipY = false;
            }

        }

        if (isFiring)
        {
            if (currentMagCount > 0)
            {
                Fire();
            }
            else
            {
                // play sound
            }
        }
        else
        {
            timeSinceLastShot = data.RateOfFire;
        }
    }

    void Fire()
    {

        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= data.RateOfFire)
        {
            if (!data.IsAutoFire)
            {
                isFiring = false;
            }

            if (bulletPrefab != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, GetComponentInChildren<SpriteRenderer>().transform.position, transform.rotation);
                float spread = Random.Range(data.BaseSpreadAngle * -1, data.BaseSpreadAngle);
                bullet.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0f, 0f, spread) * transform.right * ammoTypeLoaded.BulletSpeed, ForceMode2D.Impulse);
                bullet.GetComponent<BulletHandler>().ammoType = ammoTypeLoaded;
            }
            timeSinceLastShot = 0f;
            currentMagCount--;
            Debug.DrawRay(transform.position, transform.right * 50f, Color.red, 0.1f);
        }
    }

    void StartShooting()
    {
        if (canFire)
        {
            isFiring = true;
        }
    }

    void StopShooting()
    {
        isFiring = false;
    }

    void Reload()
    {
        if (canReload)
        {
            canReload = false;
            canFire = false;
            isAiming = false;
            Debug.Log("event hit");
            StartCoroutine(ReloadCoolDown());
        }
    }

    private IEnumerator ReloadCoolDown()
    {
        Debug.Log("Reloading");
        int ammoInInventory = GetComponentInParent<PlayerInventory>().GetAmountOfItem(ammoTypeLoaded);
        if (ammoInInventory != 0)
        {
            LaunchMag();
            if (currentMagCount > 0)
            {
                yield return new WaitForSecondsRealtime(data.ReloadTime);
            }
            else
            {
                yield return new WaitForSecondsRealtime(data.ReloadTime + 2f);
            }
            if (ammoInInventory >= maxMagCount)
            {
                currentMagCount = maxMagCount;
                EventChannels.ItemEvents.OnRemoveItemFromInventory(ammoTypeLoaded, maxMagCount);
            }
            else if (ammoInInventory > 0)
            {
                currentMagCount = ammoInInventory;
                EventChannels.ItemEvents.OnRemoveItemFromInventory(ammoTypeLoaded, ammoInInventory);
            }
            isAiming = true;
            canFire = true;
            canReload = true;
        }

        EventChannels.WeaponEvents.OnWeaponReloaded?.Invoke();
    }

    void LaunchMag()
    {
        GameObject mag = Instantiate(magazinePrefab, transform.position, transform.rotation);
        mag.GetComponent<MagazineHandler>().SetData(data);
        mag.GetComponent<Rigidbody2D>().AddForce(transform.right * .2f, ForceMode2D.Impulse);
    }

    public WeaponData GetWeaponData()
    {
        return data;
    }
}
