using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using TMPro;

public class WeaponHandler : MonoBehaviour
{
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

    private bool isPaused;
    // saves the mag count of the primary/secondary weapon when switching to the other
    private int cachedMagCount;

    private void Start()
    {
        isPaused = false;
        data = GetComponent<PlayerWeaponInventoryHandler>().GetPrimaryWeapon();
        EventChannels.PlayerInputEvents.OnPlayerShootStarted += StartShooting;
        EventChannels.PlayerInputEvents.OnPlayerShootFinished += StopShooting;
        EventChannels.PlayerInputEvents.OnPlayerReload += Reload;
        EventChannels.PlayerInputEvents.OnPlayerAim += Aim;
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes += ToggleAmmoTypes;
        EventChannels.ItemEvents.OnGetCurrentlyLoadedAmmo += GetCurrentlyLoadedAmmoType;
        EventChannels.ItemEvents.OnGetSubtypesInInventory += GetAmmoTypesInInventory;
        EventChannels.WeaponEvents.OnGetAmmoType += GetCurrentAmmoType;
        EventChannels.WeaponEvents.OnSwitchWeapon += SetWeaponData;

        EventChannels.PlayerInputEvents.OnPlayerPauses += PauseGame;
        EventChannels.GameplayEvents.OnPlayerResumesGame += ResumeGame;

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

        EventChannels.PlayerInputEvents.OnPlayerPauses -= PauseGame;
        EventChannels.GameplayEvents.OnPlayerResumesGame -= ResumeGame;
    }

    public WeaponData GetWeaponData()
    {
        return data;
    }

    // This handles switching between your primary and secondary weapon
    private void SetWeaponData(WeaponData weapon)
    {
        if (data != weapon)
        {
            // This switches currentMagCount and cachedMagCount
            (currentMagCount, cachedMagCount) = (cachedMagCount, currentMagCount);
            data = weapon;
            gunSprite.sprite = data.Sprite;
            maxMagCount = data.MagCapacity;
            SetIndexAndAmmoType();
        }
    }

    private void Aim(Vector2 aimVector)
    {
        pointer = aimVector;
    }

    private void Update()
    {
        magText.text = $"{currentMagCount}/{maxMagCount}";
        if (!isPaused)
        {
            if (isAiming)
            {
                // Translate mouse position to on screen point and turn weapon to there
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(pointer);
                mousePos.z = 0f;
                Vector3 direction = (mousePos - transform.position).normalized;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                // Flip gun sprite if aiming left of center
                if (mousePos.x < 0)
                {
                    gunSprite.flipY = true;
                }
                else
                {
                    gunSprite.flipY = false;
                }

            }

            // Weapon firing logic
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
                    // This prevents the click event from playing every frame
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
    }

    void Fire()
    {

        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= data.RateOfFire)
        {
            // Stops firing after one shot on semi automatic weapons
            if (!data.IsAutoFire)
            {
                isFiring = false;
            }

            // Spawns a bullet using object pooling
            if (bulletPrefab != null)
            {
                GameObject bullet = ObjectPoolHandler.SpawnObject(bulletPrefab, GetComponentInChildren<SpriteRenderer>().transform.position, transform.rotation);
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
            // Checks if ammo subtype looping through is in player inventory
            if (EventChannels.ItemEvents.OnCheckIfItemInInventory(ammoType))
            {
                ammoTypeLoaded = ammoType;
                ammoTypeIndex = data.AmmoSubtypes.IndexOf(ammoType);
                return;
            }
        }
        // Since it is pretty much impossible to create billions of ammo types, this serves as a good way to check in other methods
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
        // When the mouse button is released, this function is called. This therefore will make the click be played again
        // when the mouse is pressed next
        clickPlayed = false;
    }

    void Reload()
    {
        // Hides the sub ammo type UI
        currentlyTogglingAmmoTypes = false;
        // Gets all subtypes in inventory
        List<AmmoSubtype> subtypes = GetAmmoTypesInInventory();
        // If no ammo type is currently loaded, load the first subtype in the list
        if (ammoTypeLoaded == null)
            ammoTypeLoaded = subtypes[0];
        if (canReload)
        {
            // Prevent the player from triggering another reload, firing or aiming while the reload is going on
            canReload = false;
            canFire = false;
            isAiming = false;
            StartCoroutine(ReloadCoolDown());
            return;
        }
    }

    private IEnumerator ReloadCoolDown()
    {
        // This event handles sprite logic
        EventChannels.WeaponEvents.OnWeaponReload?.Invoke();
        Debug.Log("Reloading");
        // Gets ammo in inventory, if it's 0 then nothing will happen
        int ammoInInventory = GetComponentInParent<PlayerInventory>().GetAmountOfItem(ammoTypeLoaded);
        if (ammoInInventory != 0)
        {
            LaunchMag();
            // Reload sound events are split into 3, this is so I don't have to make sound events the exact same length as the weapon data's reload
            FMODUnity.RuntimeManager.PlayOneShot($"event:/PlayerEvents/WeaponEvents/Reload/{data.reloadEventName}_Start", transform.position);
            if (currentMagCount > 0)
            {
                yield return new WaitForSecondsRealtime(data.ReloadTime);
                FMODUnity.RuntimeManager.PlayOneShot($"event:/PlayerEvents/WeaponEvents/Reload/{data.reloadEventName}_Finish_Tact");
            }
            else
            {
                // Reload from empty takes longer
                yield return new WaitForSecondsRealtime(data.ReloadTime + 2f);
                FMODUnity.RuntimeManager.PlayOneShot($"event:/PlayerEvents/WeaponEvents/Reload/{data.reloadEventName}_Finish_Empty");
            }
            // If there is more or the same amount of ammo in the player's inventory as the max mag count no 'complex' calculation has to be done
            if (ammoInInventory >= maxMagCount)
            {
                EventChannels.ItemEvents.OnRemoveItemFromInventory(ammoTypeLoaded, maxMagCount - currentMagCount);
                currentMagCount = maxMagCount;
            }
            else if (ammoInInventory > 0)
            {
                // Otherwise, use up all the ammo
                currentMagCount = ammoInInventory;
                EventChannels.ItemEvents.OnRemoveItemFromInventory(ammoTypeLoaded, ammoInInventory);
            }
            // Make the player aim, be able to fire and trigger the reload mechanic again again
            isAiming = true;
            canFire = true;
            canReload = true;
            Debug.Log(GetCurrentlyLoadedAmmoType());
        }
        // Weapon sprite logic
        EventChannels.WeaponEvents.OnWeaponReloaded?.Invoke();
    }

    void LaunchMag()
    {
        GameObject mag = ObjectPoolHandler.SpawnObject(magazinePrefab, transform.position, transform.rotation);
        // Sets the sprite to the weapon data magazine
        mag.GetComponent<MagazineHandler>().SetData(data);
        mag.GetComponent<Rigidbody2D>().AddForce(transform.right * .2f, ForceMode2D.Impulse);
    }


    public void ToggleAmmoTypes()
    {
        canFire = false;
        // If toggling ammo types already, loop between all available ammo types
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
            // If not toggling ammo types already, open the UI for it
            if (GetAmmoTypesInInventory().Count != 0)
            {
                EventChannels.UIEvents.OnShowAmmoTypes?.Invoke();
                currentlyTogglingAmmoTypes = true;
            }
            else
            {
                EventChannels.UIEvents.OnShowNoAmmoTypes?.Invoke();
            }
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

    private void PauseGame()
    {
        isPaused = true;
    }

    private void ResumeGame()
    {
        isPaused = false; ;
    }
}