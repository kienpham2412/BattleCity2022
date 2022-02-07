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
    private TMP_Dropdown dropdown;
    private TMP_InputField input;
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
        LoadFileName();
    }

    /// <summary>
    /// Load saved filenames
    /// </summary>
    public void LoadFileName()
    {
        List<string> filenames = new List<string>();

        string[] files = Directory.GetFiles(Application.dataPath + "/Maps", "*.map", SearchOption.AllDirectories);
        foreach (string aFile in files)
        {
            filenames.Add(Path.GetFileNameWithoutExtension(aFile));
        }

        dropdown = nameDropdown.GetComponent<TMP_Dropdown>();
        dropdown.AddOptions(filenames);
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
    /// Display a question to make sure player wants to exit
    /// </summary>
    public override void ShowExitQuestion()
    {
        base.ShowExitQuestion();
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
        TMP_InputField input = nameInput.gameObject.GetComponent<TMP_InputField>();

        if (input.text == "") return;
        GameManager.singleton.SaveMap(input.text);
        Debug.Log(input.text);
    }

    /// <summary>
    /// Load a saved map
    /// </summary>
    public void LoadMap()
    {
        string name = dropdown.options[dropdown.value].text;
        GameManager.singleton.LoadMap(name);
    }

    public void GenerateMap(){
        GameManager.singleton.mapBuilder.GenerateRandomMap();
    }

    public void GenerateBaseMap(){
        GameManager.singleton.mapBuilder.GenerateBaseMap();
    }

    public void CreateBlankMap(){
        GameManager.singleton.mapBuilder.CreateBlankMap();
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
