using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
public class WeaponBenchToggler : MonoBehaviour
{
    private bool playerIsInTrigger;
    // Start is called before the first frame update
    void Start()
    {
        playerIsInTrigger = false;
        EventChannels.PlayerInputEvents.OnInteract += OpenWeaponBench;
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnInteract -= OpenWeaponBench;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInputHandler>())
            playerIsInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInputHandler>() && playerIsInTrigger)
            playerIsInTrigger= false;
    }
    
    private void OpenWeaponBench()
    {
        if (playerIsInTrigger)
            EventChannels.OutpostEvents.OnShowWeaponBench?.Invoke();
    }
}
