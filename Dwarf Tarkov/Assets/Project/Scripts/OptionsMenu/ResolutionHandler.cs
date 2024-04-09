using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionHandler : MonoBehaviour
{
    private int currentResolutionIndex;
    private List<Resolution> resolutions;
    [Header("Resolution")]
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    private bool isFullScreen;

    [SerializeField]
    private Toggle fullscreenToggle;


    // Start is called before the first frame update
    void Start()
    {
        isFullScreen = Screen.fullScreen;
        fullscreenToggle.isOn = isFullScreen;
        List<string> options = new List<string>();
        resolutions = Screen.resolutions.ToList();

        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = $"{resolutions[i].width}x{resolutions[i].height}";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
    }

    public void SetResolution()
    {
        Resolution newRes = resolutions[resolutionDropdown.value];
        Screen.SetResolution(newRes.width, newRes.height, isFullScreen);
    }

    public void SetFullScreen()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
}
