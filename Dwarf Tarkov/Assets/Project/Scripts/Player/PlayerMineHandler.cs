using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EventSystem;

public class PlayerMineHandler : MonoBehaviour
{
    private Camera main;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;

        EventChannels.PlayerInputEvents.OnPlayerMine += Mine;
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerMine -= Mine;
    }

    void Mine()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            Debug.Log($"Raycast hit {hitObject.name}");
        }
    }
}
