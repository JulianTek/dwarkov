using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using EventSystem;

public class WeaponBenchUIHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private GameObject WeaponBenchUI;

    private bool isPrimary;

    // Start is called before the first frame update
    void Start()
    {
        HideWeaponBench();

        EventChannels.OutpostEvents.OnShowWeaponBench += ShowWeaponBench;
        EventChannels.OutpostEvents.OnHideWeaponBench += HideWeaponBench;

        isPrimary = true;
    }


    private void OnDestroy()
    {
        EventChannels.OutpostEvents.OnShowWeaponBench -= ShowWeaponBench;
        EventChannels.OutpostEvents.OnHideWeaponBench -= HideWeaponBench;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowWeaponBench()
    {
        WeaponBenchUI.SetActive(true);
        GetComponentInChildren<WeaponBenchSlotListHandler>().UpdateList(true);
        EventChannels.PlayerInputEvents.OnEnableHUDControls?.Invoke();
    }

    void HideWeaponBench()
    {
        WeaponBenchUI.SetActive(false);
    }

    public void SwitchSides()
    {
        isPrimary = !isPrimary;
        if (isPrimary)
        {
            buttonText.SetText("Secondary weapons");
        }
        else
        {
            buttonText.SetText("Primary weapons");
        }
        GetComponentInChildren<WeaponBenchSlotListHandler>().UpdateList(isPrimary);
        EventChannels.UIEvents.OnSwitchWeaponSlotSide?.Invoke(isPrimary);
    }
}
