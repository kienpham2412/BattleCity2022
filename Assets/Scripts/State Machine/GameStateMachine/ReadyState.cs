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

    private void DisplayStage()
    {
        stageDisplay.text = $"Stage {stage}";
        stage++;
    }

    IEnumerator DelayToNext()
    {
        yield return new WaitForSeconds(2);
        Next();
    }
}
