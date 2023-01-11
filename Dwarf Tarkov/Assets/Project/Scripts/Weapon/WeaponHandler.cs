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
        currentMagCount = maxMagCount;
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
            Vector2 direction = (pointer - (Vector2)transform.position).normalized;
            transform.right = direction;

            Vector2 scale = transform.localScale;
            if (direction.x < 0)
            {
                scale.y = -1;
            }
            else if (direction.x > 0)
            {
                scale.y = 1;
            }
            transform.localScale = scale;
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
                bullet.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0f, 0f, spread) * transform.right * data.AmmoType.BulletSpeed, ForceMode2D.Impulse);
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
        int ammoInInventory = GetComponentInParent<PlayerInventory>().GetAmountOfItem(data.AmmoType);
        if (ammoInInventory >= maxMagCount)
        {
            LaunchMag();
            currentMagCount = maxMagCount;
        }
        else if (ammoInInventory > 0)
        {
            LaunchMag();
            currentMagCount = ammoInInventory;
        }
        else
        {
            yield break;
        }
        if (currentMagCount > 0)
        {
            yield return new WaitForSecondsRealtime(data.ReloadTime);
        }
        else
        {
            yield return new WaitForSecondsRealtime(data.ReloadTime + 2f);
        }
        isAiming = true;
        canFire = true;
        canReload = true;
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
