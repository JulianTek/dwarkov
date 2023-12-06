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
    private int ammoTypeIndex;

    private int maxMagCount;
    private int currentMagCount;

    private Vector2 pointer;
    private float timeSinceLastShot = 0f;

    private bool isAiming = true;
    private bool isFiring = false;
    private bool canFire = true;
    private bool canReload = true;
    private bool currentlyTogglingAmmoTypes;
    private bool clickPlayed;

    private void Start()
    {
        EventChannels.PlayerInputEvents.OnPlayerShootStarted += StartShooting;
        EventChannels.PlayerInputEvents.OnPlayerShootFinished += StopShooting;
        EventChannels.PlayerInputEvents.OnPlayerReload += Reload;
        EventChannels.PlayerInputEvents.OnPlayerAim += Aim;
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes += ToggleAmmoTypes;
        EventChannels.ItemEvents.OnGetCurrentlyLoadedAmmo += GetCurrentlyLoadedAmmoType;
        EventChannels.ItemEvents.OnGetSubtypesInInventory += GetAmmoTypesInInventory;
        EventChannels.WeaponEvents.OnGetAmmoType += GetCurrentAmmoType;
        EventChannels.WeaponEvents.OnSwitchWeapon += SetWeaponData;

        maxMagCount = data.MagCapacity;
        currentMagCount = 0;
        gunSprite = GetComponentInChildren<SpriteRenderer>();
        SetIndexAndAmmoType();
    }

    void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerShootStarted -= StartShooting;
        EventChannels.PlayerInputEvents.OnPlayerShootFinished -= StopShooting;
        EventChannels.PlayerInputEvents.OnPlayerReload -= Reload;
        EventChannels.PlayerInputEvents.OnPlayerAim -= Aim;
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes -= ToggleAmmoTypes;
        EventChannels.ItemEvents.OnGetCurrentlyLoadedAmmo -= GetCurrentlyLoadedAmmoType;
        EventChannels.ItemEvents.OnGetSubtypesInInventory -= GetAmmoTypesInInventory;
        EventChannels.WeaponEvents.OnGetAmmoType -= GetCurrentAmmoType;
        EventChannels.WeaponEvents.OnSwitchWeapon -= SetWeaponData;
    }

    public WeaponData GetWeaponData()
    {
        return data;
    }

    private void SetWeaponData(WeaponData weapon)
    {
        data = weapon;
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
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(pointer);
            mousePos.z = 0f;
            Vector3 direction = (mousePos - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (mousePos.x < 0)
            {
                gunSprite.flipY = true;
            }
            else
            {
                gunSprite.flipY = false;
            }

        }

        if (isFiring)
        {
            if (currentMagCount > 0)
            {
                Fire();
                FMODUnity.RuntimeManager.PlayOneShot($"event:/PlayerEvents/WeaponEvents/Firing/{data.firingEventName}", transform.position);
                Physics2D.CircleCast(transform.position, data.firingEventRadius, Vector2.zero, 0f);
            }
            else
            {
                if (!clickPlayed)
                {
                    FMODUnity.RuntimeManager.PlayOneShot($"event:/PlayerEvents/WeaponEvents/Empty/{data.ammoEmptyFiringEventName}", transform.position);
                    clickPlayed = true;
                }
                
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

    public void SetIndexAndAmmoType()
    {
        foreach (AmmoSubtype ammoType in data.AmmoSubtypes)
        {
            if (EventChannels.ItemEvents.OnCheckIfItemInInventory(ammoType))
            {
                ammoTypeLoaded = ammoType;
                ammoTypeIndex = data.AmmoSubtypes.IndexOf(ammoType);
                return;
            }
        }
        if (ammoTypeLoaded == null)
        {
            ammoTypeIndex = int.MaxValue;
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
        clickPlayed = false;
    }

    void Reload()
    {
        currentlyTogglingAmmoTypes = false;
        List<AmmoSubtype> subtypes = GetAmmoTypesInInventory();
        if (ammoTypeLoaded == null)
            ammoTypeLoaded = subtypes[0];
        if (canReload)
        {
            canReload = false;
            canFire = false;
            isAiming = false;
            StartCoroutine(ReloadCoolDown());
            return;
        }
    }

    private IEnumerator ReloadCoolDown()
    {
        Debug.Log("Reloading");
        int ammoInInventory = GetComponentInParent<PlayerInventory>().GetAmountOfItem(ammoTypeLoaded);
        if (ammoInInventory != 0)
        {
            LaunchMag();
            FMODUnity.RuntimeManager.PlayOneShot($"event:/PlayerEvents/WeaponEvents/Reload/{data.reloadEventName}_Start", transform.position);
            if (currentMagCount > 0)
            {
                yield return new WaitForSecondsRealtime(data.ReloadTime);
                FMODUnity.RuntimeManager.PlayOneShot($"event:/PlayerEvents/WeaponEvents/Reload/{data.reloadEventName}_Finish_Tact");
            }
            else
            {
                yield return new WaitForSecondsRealtime(data.ReloadTime + 2f);
                FMODUnity.RuntimeManager.PlayOneShot($"event:/PlayerEvents/WeaponEvents/Reload/{data.reloadEventName}_Finish_Empty");
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
            Debug.Log(GetCurrentlyLoadedAmmoType());
        }

        EventChannels.WeaponEvents.OnWeaponReloaded?.Invoke();
    }

    void LaunchMag()
    {
        GameObject mag = Instantiate(magazinePrefab, transform.position, transform.rotation);
        mag.GetComponent<MagazineHandler>().SetData(data);
        mag.GetComponent<Rigidbody2D>().AddForce(transform.right * .2f, ForceMode2D.Impulse);
    }


    public void ToggleAmmoTypes()
    {
        canFire = false;
        if (currentlyTogglingAmmoTypes)
        {
            if (ammoTypeIndex == data.AmmoSubtypes.Count - 1)
                ammoTypeIndex = 0;
            else if (ammoTypeIndex > data.AmmoSubtypes.Count)
                ammoTypeIndex = 1;
            else
                ammoTypeIndex++;
            ammoTypeLoaded = data.AmmoSubtypes[ammoTypeIndex];
        }
        else
        {
            EventChannels.UIEvents.OnShowAmmoTypes?.Invoke();
            currentlyTogglingAmmoTypes = true;
        }
    }

    public AmmoSubtype GetCurrentlyLoadedAmmoType()
    {
        return ammoTypeLoaded;
    }

    public List<AmmoSubtype> GetAmmoTypesInInventory()
    {
        List<AmmoSubtype> typesInInventory = new List<AmmoSubtype>();
        foreach (AmmoSubtype ammoSubtype in data.AmmoSubtypes)
        {
            if (EventChannels.ItemEvents.OnCheckIfItemInInventory(ammoSubtype))
            {
                typesInInventory.Add(ammoSubtype);
            }
        }
        return typesInInventory;
    }

    private AmmoSubtype GetCurrentAmmoType()
    {
        return ammoTypeLoaded;
    }


}