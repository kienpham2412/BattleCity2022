using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedUI : MonoBehaviour, ISelectHandler
{
    /// <summary>
    /// Selected event of the UI
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSelect(BaseEventData eventData)
    {
        AudioController.Instance.PlaySelectEventSFX();
    }
}
