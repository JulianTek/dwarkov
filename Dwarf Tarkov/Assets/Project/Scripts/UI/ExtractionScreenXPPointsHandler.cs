using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EventSystem;
public class ExtractionScreenXPPointsHandler : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        EventChannels.UIEvents.OnSetXPPoints += SetXPText;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnSetXPPoints -= SetXPText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetXPText(int experiencePoints)
    {
        text.SetText($"Experience points earned: {experiencePoints}");
    }
}
