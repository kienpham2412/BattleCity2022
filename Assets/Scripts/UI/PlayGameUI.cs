using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameUI : UIManager
{
    protected PlayerControl myControl;

    public override void Awake()
    {
        this.myControl = new PlayerControl();
        this.myControl.UI.Cancel.performed += context => ShowExitMenu();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public void ShowExitMenu()
    {
        if (!menu.GetMenu(1).activeSelf)
        {
            menu.ShowMenu(1);
            Player.Instance.ActiveInput(false);
        }
        else
        {
            menu.HideMenu(1);
            Player.Instance.ActiveInput(true);
        }
    }

    public override void HideMenu(int index)
    {
        Player.Instance.ActiveInput(true);
        base.HideMenu(index);
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
