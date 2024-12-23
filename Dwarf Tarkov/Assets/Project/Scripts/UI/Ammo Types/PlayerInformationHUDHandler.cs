using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class PlayerInformationHUDHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.UIEvents.OnShowAmmoTypes += HideHUD;
        EventChannels.UIEvents.OnHideAmmoTypes += ShowHUD;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnShowAmmoTypes -= HideHUD;
        EventChannels.UIEvents.OnHideAmmoTypes -= ShowHUD;
    }

    void ShowHUD()
    {
        gameObject.SetActive(true);
    }

    void HideHUD()
    {
        gameObject.SetActive(false);
    }
}
