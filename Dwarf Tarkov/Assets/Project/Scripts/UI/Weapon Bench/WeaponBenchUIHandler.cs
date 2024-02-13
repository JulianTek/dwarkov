using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EventSystem;

public class WeaponBenchUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject WeaponBenchUI;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        HideWeaponBench();

        EventChannels.OutpostEvents.OnShowWeaponBench += ShowWeaponBench;
        EventChannels.OutpostEvents.OnHideWeaponBench += HideWeaponBench;
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
        GetComponentInChildren<WeaponBenchSlotListHandler>().UpdateList();
        EventChannels.PlayerInputEvents.OnEnableHUDControls?.Invoke();
    }

    void HideWeaponBench()
    {
        WeaponBenchUI.SetActive(false);
    }
}
