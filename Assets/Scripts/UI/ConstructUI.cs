using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ConstructUI : UIManager
{
    public GameObject nameDropdown;
    public GameObject nameInput;
    protected PlayerControl myControl;

    public override void Awake()
    {
        // base.Awake();
        myControl = new PlayerControl();
        myControl.UI.Cancel.performed += context => ShowBuildMenu();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Show the construction menu
    /// </summary>
    public void ShowBuildMenu()
    {
        if (!menu.GetMenu(0).activeSelf)
        {
            menu.ShowMenu(0);
            Selector.singleton.ActiveInput(false);
        }
        else
        {
            menu.HideMenu(0);
            Selector.singleton.ActiveInput(true);
        }
    }

    /// <summary>
    /// Hide a menu
    /// </summary>
    /// <param name="index">Index value of the menu</param>
    public override void HideMenu(int index)
    {
        base.HideMenu(index);
    }

    /// <summary>
    /// Load scene by name
    /// </summary>
    /// <param name="scene">Index value of the scene</param>
    public override void LoadScene(int index)
    {
        base.LoadScene(index);
    }

    /// <summary>
    /// Show the save map menu
    /// </summary>
    public void ShowSaveMapMenu()
    {
        menu.ShowMenu(2);
    }

    /// <summary>
    /// Save the current map
    /// </summary>
    public void SaveMap()
    {
        PlayerPrefs.SetInt("CustomMap", 0);
        MapSaverController.Instance.SaveMap("base");
        LoadScene(0);
    }

    public void ExitWithoutSave(){
        PlayerPrefs.SetInt("CustomMap", 1);
        LoadScene(0);
    }

    /// <summary>
    /// Generate map event
    /// </summary>
    public void GenerateMap()
    {
        MapSaverController.Instance.mapBuilder.GenerateRandomMap();
    }

    /// <summary>
    /// Generate base map event
    /// </summary>
    public void GenerateBaseMap()
    {
        MapSaverController.Instance.mapBuilder.GenerateBaseMap();
    }

    /// <summary>
    /// Generate black map event
    /// </summary>
    public void CreateBlankMap()
    {
        MapSaverController.Instance.mapBuilder.CreateBlankMap();
    }

    public void OnEnable()
    {
        myControl.UI.Enable();
    }

    public void OnDisable()
    {
        myControl.UI.Disable();
    }
}
