using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private WeaponData data;
    private int currentMagCount;
    private Vector2 pointer;

    private bool isAiming = true;
    private bool canFire = true;
    private bool canReload = true;

    private void Start()
    {
        EventChannels.PlayerInputEvents.OnPlayerShoot += Fire;
        EventChannels.PlayerInputEvents.OnPlayerReload += Reload;
        EventChannels.PlayerInputEvents.OnPlayerAim += Aim;
    }

    void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerShoot -= Fire;
        EventChannels.PlayerInputEvents.OnPlayerReload -= Reload;
        EventChannels.PlayerInputEvents.OnPlayerAim -= Aim;
    }

    private void Aim(Vector2 aimVector)
    {
        pointer = aimVector;
    }

    private void Update()
    {
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
    }
    void Fire()
    {
        if (canFire)
        {
            Debug.Log("bang bang");
        }
    }

    void Reload()
    {
        if (canReload)
        {
            canReload = false;
            canFire = false;
            isAiming = false;
            EventChannels.WeaponEvents.OnWeaponReload?.Invoke();
            Debug.Log("event hit");
            StartCoroutine(ReloadCoolDown());
        }
    }

    private IEnumerator ReloadCoolDown()
    {
        Debug.Log("Reloading");
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
        EventChannels.WeaponEvents.OnWeaponReloaded?.Invoke();
    }

    public WeaponData GetWeaponData()
    {
        return data;
    }
}
