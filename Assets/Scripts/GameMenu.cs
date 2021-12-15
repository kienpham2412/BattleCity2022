using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    public List<GameObject> menus;
    public List<GameObject> firstInteracables;
    private int currentMenu = 0;

    /// <summary>
    /// Hide all menus
    /// </summary>
    public void HideAllMenus(){
        for(int i=0;i<menus.Count;i++){
            menus[i].SetActive(false);
        }
    }

    /// <summary>
    /// Hide a menu
    /// </summary>
    /// <param name="index">Index of the hided menu</param>
    public void HideMenu(int index){
        menus[index].SetActive(false);
    }

    /// <summary>
    /// Display a menu using index
    /// </summary>
    /// <param name="index">The index value of menu</param>
    public void ShowMenu(int index){
        menus[currentMenu].SetActive(false);
        menus[index].SetActive(true);
        currentMenu = index;

        // refresh the selected interactable gameobject
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstInteracables[currentMenu]);
    }
}
