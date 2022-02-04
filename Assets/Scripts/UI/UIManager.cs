using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public abstract class UIManager : MonoBehaviour
{
    [SerializeField]
    private enum SceneName { TitleScreen = 0, Construct = 1, PlayGame = 2 };
    protected GameMenu menu;
    private SceneName thisScene;

    public virtual void Awake() {
        // myControl = new PlayerControl();
    }

    // // Start is called before the first frame update
    public virtual void Start() {
        menu = GetComponent<GameMenu>();
        menu.HideAllMenus();
    }

    /// <summary>
    /// Hide a menu
    /// </summary>
    /// <param name="index">Index value of the menu</param>
    public virtual void HideMenu(int index)
    {
        menu.HideMenu(index);
    }

    /// <summary>
    /// Load scene by name
    /// </summary>
    /// <param name="scene">Index value of the scene</param>
    public virtual void LoadScene(int index)
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
    /// Display a question to make sure player wants to exit
    /// </summary>
    public virtual void ShowExitQuestion()
    {
        menu.ShowMenu(1);
    }

}
