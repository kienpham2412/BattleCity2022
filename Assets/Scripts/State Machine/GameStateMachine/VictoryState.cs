using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VictoryState : GameState
{
    public override void Perform()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        StartCoroutine(ReturnToTitle());
    }

    IEnumerator ReturnToTitle()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("TitleScreen");
        Time.timeScale = 1;
    }
}
