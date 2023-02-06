using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadOutpostScene()
    {
        LoadScene(2);
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
        EventChannels.OnLoadScene?.Invoke(index);
    }
} 
