using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSettingController : MonoBehaviour
{
    [Header("UI element")]
    public Toggle toggleFullScreen;
    public TMP_Dropdown resolutionDropdown;
    private WindowModeController windowModeController;

    public void ToggleFullScreen()
    {
        windowModeController.isFullScreen = !windowModeController.isFullScreen;
        if (windowModeController.isFullScreen)
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else
            Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void ChangeResolution()
    {
        windowModeController.resolutionIndex = Convert.ToInt32(resolutionDropdown.value);
        int index = windowModeController.resolutionIndex;
        Screen.SetResolution(windowModeController.resolutionOption[index, 0], windowModeController.resolutionOption[index, 1], windowModeController.isFullScreen);
    }

    private void OnEnable()
    {
        windowModeController = WindowModeController.Instance;
        toggleFullScreen.SetIsOnWithoutNotify(windowModeController.isFullScreen);
        resolutionDropdown.SetValueWithoutNotify(windowModeController.resolutionIndex);
    }
}
