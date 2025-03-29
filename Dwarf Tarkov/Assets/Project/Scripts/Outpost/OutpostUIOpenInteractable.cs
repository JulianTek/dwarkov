using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostUIOpenInteractable : OutpostInteractable
{
    [SerializeField]
    private GameObject uiToOpen;
    // Start is called before the first frame update
    void Start()
    {
        uiToOpen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void RunInteraction()
    {
        uiToOpen?.SetActive(true);
    }
}
