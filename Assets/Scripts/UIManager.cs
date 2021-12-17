using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public enum SceneName { TitleScreen = 0, Construct = 1, PlayGame = 2 };
    private GameMenu menu;
    public SceneName thisScene;
    private PlayerControl myControl;

    private void Awake()
    {
        myControl = new PlayerControl();
        myControl.UI.Cancel.performed += context => ShowBuildMenu();
    }

    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<GameMenu>();
        menu.HideAllMenus();
        if (thisScene == SceneName.TitleScreen)
            menu.ShowMenu(0);
    }

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
    public void AskExitBuilding()
    {
        if (thisScene != SceneName.TitleScreen)
        {
            menu.ShowMenu(1);
        }
    }

    /// <summary>
    /// Hide a menu
    /// </summary>
    /// <param name="index">Index value of the menu</param>
    public void HideMenu(int index)
    {
        menu.HideMenu(index);
    }

    /// <summary>
    /// Show title menu
    /// </summary>
    public void ReturnToTitle()
    {
        menu.ShowMenu(0);
    }

    /// <summary>
    /// Emit exit event
    /// </summary>
    public void ShowExitQuestion()
    {
        menu.ShowMenu(1);
    }

    /// <summary>
    /// Show setting menu
    /// </summary>
    public void ShowSettingMenu()
    {
        menu.ShowMenu(2);
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
    public void LoadScene(int index)
    {
        switch ((SceneName)index)
        {
            case SceneName.TitleScreen:
                SceneManager.LoadScene("TitleScreen");
                break;
            case SceneName.Construct:
                SceneManager.LoadScene("Construct");
                break;
            case SceneName.PlayGame:
                SceneManager.LoadScene("PlayGame");
                break;
        }
    }

    /// <summary>
    /// Exit Game
    /// </summary>
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private void OnEnable()
    {
        myControl.UI.Enable();
    }

    private void OnDisable()
    {
        myControl.UI.Disable();
    }
}
