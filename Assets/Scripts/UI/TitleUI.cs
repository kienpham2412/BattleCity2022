using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleUI : UIManager
{
    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<GameMenu>();
        menu.HideAllMenus();
        menu.ShowMenu(0);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    /// <summary>
    /// Show setting menu
    /// </summary>
    public void ShowSettingMenu()
    {
        this.menu.ShowMenu(2);
    }

    /// <summary>
    /// Show control setting menu
    /// </summary>
    public void ShowControlSetting()
    {
        menu.ShowMenu(3);
    }

    /// <summary>
    /// Show video setting menu
    /// </summary>
    public void ShowVideoSetting()
    {
        menu.ShowMenu(4);
    }

    /// <summary>
    /// Show music setting menu
    /// </summary>
    public void ShowMusicSetting()
    {
        menu.ShowMenu(5);
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
    /// Display a question to make sure player wants to exit
    /// </summary>
    public override void ShowExitQuestion()
    {
        base.ShowExitQuestion();
    }

    /// <summary>
    /// Exit Game
    /// </summary>
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    /// <summary>
    /// Show title menu
    /// </summary>
    public void ReturnToTitle()
    {
        menu.ShowMenu(0);
    }
}
