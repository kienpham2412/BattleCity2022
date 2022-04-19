using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadyState : GameState
{
    [Header("UI elements")]
    public TMP_Text stageDisplay;
    private int stage = 1;

    public override void Perform()
    {
        AudioController.Instance.PlayMusic(MusicType.open);
        DisplayStage();
        StartCoroutine(DelayToNext());
    }

    public override void Next()
    {
        gameObject.SetActive(false);
        base.Next();
    }

    /// <summary>
    /// Display current stage on HUD
    /// </summary>
    private void DisplayStage()
    {
        stageDisplay.text = $"Stage {stage}";
        stage++;
    }

    /// <summary>
    /// Delay to the next state
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayToNext()
    {
        yield return new WaitForSeconds(2);
        Next();
    }
}
