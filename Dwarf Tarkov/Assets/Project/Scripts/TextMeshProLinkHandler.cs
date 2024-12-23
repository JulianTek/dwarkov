using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextMeshProLinkHandler : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text m_Text;
    private Canvas canvas;
    [SerializeField]
    private Camera cameraToUse;


    private void Awake()
    {
        m_Text = GetComponent<TMP_Text>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            cameraToUse = null;
        else
            cameraToUse = canvas.worldCamera;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 inputPosition = new Vector3(eventData.position.x, eventData.position.y, 0f);

        var linkTag = TMP_TextUtilities.FindIntersectingLink(m_Text, inputPosition, cameraToUse);

        if (linkTag != -1)
        {
            TMP_LinkInfo info = m_Text.textInfo.linkInfo[linkTag];

            string linkID = info.GetLinkID();

            if (linkID.Contains("www") || linkID.Contains("https://"))
            {
                Application.OpenURL(linkID);
                return;
            }
        }
    }
}
