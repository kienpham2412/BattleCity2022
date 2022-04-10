using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowModeController : MonoBehaviour
{
    public static WindowModeController Instance;

    private string fullScreenKey = "FullScreen";
    private string resolutionKey = "ResolutionKey";
    public int[,] resolutionOption = new int[3, 2] { { 720, 480 }, { 1280, 720 }, { 1920, 1080 } };
    public bool isFullScreen
    {
        set
        {
            PlayerPrefs.SetInt(fullScreenKey, Convert.ToInt32(value));
        }
        get
        {
            if (!PlayerPrefs.HasKey(fullScreenKey))
                PlayerPrefs.SetInt(fullScreenKey, 1);

            return PlayerPrefs.GetInt(fullScreenKey) == 1;
        }
    }

    public int resolutionIndex
    {
        set
        {
            PlayerPrefs.SetInt(resolutionKey, value);
        }
        get
        {
            if (!PlayerPrefs.HasKey(resolutionKey))
                PlayerPrefs.SetInt(resolutionKey, 2);

            return PlayerPrefs.GetInt(resolutionKey);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        Screen.SetResolution(resolutionOption[resolutionIndex, 0], resolutionOption[resolutionIndex, 1], isFullScreen);
    }
}
