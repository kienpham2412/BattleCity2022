using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameUI : UIManager
{
    protected PlayerControl myControl;

    public override void Awake()
    {
        // base.Awake();
        this.myControl = new PlayerControl();
        this.myControl.UI.Cancel.performed += context => ShowExitMenu();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Display a question to make sure player wants to exit
    /// </summary>
    public void ShowExitMenu()
    {
        if (!menu.GetMenu(1).activeSelf)
        {
            menu.ShowMenu(1);
            Player.singleton.ActiveInput(false);
        }
        else
        {
            menu.HideMenu(1);
            Player.singleton.ActiveInput(true);
        }
    }

    /// <summary>
    /// Hide a menu
    /// </summary>
    /// <param name="index">Index value of the menu</param>
    public override void HideMenu(int index)
    {
        Player.singleton.ActiveInput(true);
        base.HideMenu(index);
    }

    /// <summary>
    /// This function is called when the game object is active on the scene
    /// </summary>
    public void OnEnable()
    {
        myControl.UI.Enable();
    }

    /// <summary>
    /// This function is called when the game object is inactive on the scene
    /// </summary>
    public void OnDisable()
    {
        myControl.UI.Disable();
    }
}
