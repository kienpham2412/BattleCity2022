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
    /// Get a menu by index
    /// </summary>
    /// <param name="index">The index of menu</param>
    /// <returns></returns>
    public GameObject GetMenu(int index)
    {
        return menus[index];
    }

    /// <summary>
    /// Hide all menus
    /// </summary>
    public void HideAllMenus()
    {
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].SetActive(false);
        }
    }

    /// <summary>
    /// Hide a menu
    /// </summary>
    /// <param name="index">Index of the hided menu</param>
    public void HideMenu(int index)
    {
        menus[index].SetActive(false);
    }

    /// <summary>
    /// Display a menu using index
    /// </summary>
    /// <param name="index">The index value of menu</param>
    public void ShowMenu(int index)
    {
        menus[currentMenu].GetComponent<Animator>().Play("CloseMenu");

        StartCoroutine(SetInteractable(index));
    }

    private IEnumerator SetInteractable(int index)
    {
        // refresh the selected interactable gameobject
        yield return new WaitForSecondsRealtime(0.1f);
        menus[currentMenu].SetActive(false);
        menus[index].SetActive(true);
        currentMenu = index;

        yield return new WaitForSecondsRealtime(0.1f);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstInteracables[currentMenu]);
    }
}
