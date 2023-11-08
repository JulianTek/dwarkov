using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EventSystem;
public class CreditsTextHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI creditsText;
    [SerializeField]
    private int cooldownInSeconds;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.BarteringEvents.OnPlayerHasInsufficientCredits += ShowText;
    }

    void ShowText()
    {
        creditsText.gameObject.SetActive(true);
        StartCoroutine(HideText());
    }

    private IEnumerator HideText() 
    {
        yield return new WaitForSecondsRealtime(cooldownInSeconds);
        creditsText.gameObject.SetActive(false);
    }
}
