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
    private int currentlyLoadedTypeIndex;

    // Start is called before the first frame update
    void Start()
    {
        EventChannels.UIEvents.OnShowAmmoTypes += ShowHUD;
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes += CycleAmmoType;
        EventChannels.PlayerInputEvents.OnPlayerReload += HideHUD;
        EventChannels.UIEvents.OnShowNoAmmoTypes += ShowNoAmmoTypes;
        EventChannels.UIEvents.OnHideAmmoTypes += HideNoAmmoTypes;
        text.fontSize = 0.1f;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnShowAmmoTypes -= ShowHUD;
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes -= CycleAmmoType;
        EventChannels.PlayerInputEvents.OnPlayerReload -= HideHUD;
        EventChannels.UIEvents.OnShowNoAmmoTypes -= ShowNoAmmoTypes;
        EventChannels.UIEvents.OnHideAmmoTypes -= HideNoAmmoTypes;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CycleAmmoType()
    {
        if (gameObject.activeSelf)
        {
            if (currentlyLoadedTypeIndex == ammoSubtypes.Count - 1)
            {
                currentlyLoadedTypeIndex = 0;
            }
            else
            {
                currentlyLoadedTypeIndex++;
            }
            ResetText();
        }
    }

    private void ShowHUD()
    {
        text.SetText("");
        gameObject.SetActive(true);
        ammoSubtypes = EventChannels.ItemEvents.OnGetSubtypesInInventory();
        currentlyLoadedType = EventChannels.WeaponEvents.OnGetAmmoType?.Invoke();
        if (currentlyLoadedType != null)
        {
            for (int i = 0; i < ammoSubtypes.Count; i++)
            {
                AmmoSubtype subtype = ammoSubtypes[i];
                if (subtype == currentlyLoadedType)
                {
                    text.text += $"> {subtype.Name} <br>";
                    currentlyLoadedTypeIndex = i;
                }
                else
                {
                    text.text += $"{subtype.Name} <br>";
                }
            }
        }
        // if there isn't a loaded type yet
        else
        {
            text.text += $"> {ammoSubtypes[0].Name} <br>";
            for (int i = 1; i < ammoSubtypes.Count; i++)
            {
                AmmoSubtype subtype = ammoSubtypes[i];
                text.text += $"{subtype.Name} <br>";
            }
            currentlyLoadedTypeIndex = 0;
        }

    }

    private void HideHUD()
    {
        gameObject.SetActive(false);
        EventChannels.UIEvents.OnHideAmmoTypes?.Invoke();
    }

    private void ResetText()
    {
        text.text = "";
        for (int i = 0; i < ammoSubtypes.Count; i++)
        {
            if (i == currentlyLoadedTypeIndex)
                text.text += "> ";
            text.text += $"{ammoSubtypes[i].Name} <br>";
        }
    }

    private void ShowNoAmmoTypes()
    {
        text.SetText("No ammo types available!");
        gameObject.SetActive(true);
    }

    private void HideNoAmmoTypes()
    {
        gameObject.SetActive(false);
    }
}
