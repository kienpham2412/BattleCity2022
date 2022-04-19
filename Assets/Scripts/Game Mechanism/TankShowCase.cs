using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShowCase : MonoBehaviour, ISubscriber
{
    public static TankShowCase Instance;
    private GameObject[] tankIcons;
    private int currentTankIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void Handle(Message message)
    {
        ResetEnemyShowCase();
    }

    /// <summary>
    /// Fully reset all the tanks icon on the HUD
    /// </summary>
    private void ResetEnemyShowCase()
    {
        currentTankIndex = 0;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void Destroy()
    {
        transform.GetChild(currentTankIndex).gameObject.SetActive(false);
        currentTankIndex++;
    }

    private void OnEnable()
    {
        MessageManager.Instance.AddSubscriber(MessageType.OnGameRestart, this);
    }
}
