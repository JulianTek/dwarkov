using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using TMPro;

public class AmmoTypeUIHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private List<AmmoSubtype> ammoSubtypes = new List<AmmoSubtype>();
    private AmmoSubtype currentlyLoadedType;

    // Start is called before the first frame update
    void Start()
    {
        EventChannels.UIEvents.OnShowAmmoTypes += ShowHUD;
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes += CycleAmmoType;
        EventChannels.PlayerInputEvents.OnPlayerReload += HideHUD;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnShowAmmoTypes -= ShowHUD;
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes -= CycleAmmoType;
        EventChannels.PlayerInputEvents.OnPlayerReload -= HideHUD;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CycleAmmoType()
    {
        if (gameObject.activeSelf)
        {

        }
    }

    private void ShowHUD()
    {
        gameObject.SetActive(true);
        ammoSubtypes = EventChannels.ItemEvents.OnGetSubtypesInInventory();
        currentlyLoadedType = EventChannels.WeaponEvents.OnGetAmmoType?.Invoke();
        foreach (AmmoSubtype subtype in ammoSubtypes)
        {
            if (subtype == currentlyLoadedType)
            {
                text.color = Color.yellow;
                text.text += $">{subtype.Name} <br>";
            }
            else
            {
                text.text += $"{subtype.Name} <br>";
            }
        }
    }

    private void HideHUD()
    {
        gameObject.SetActive(false);
        EventChannels.UIEvents.OnHideAmmoTypes?.Invoke();
    } 
}
