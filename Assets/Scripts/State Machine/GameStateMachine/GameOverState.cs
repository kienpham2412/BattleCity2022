using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : GameState
{
    public override void Perform()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        AudioController.Instance.PlayMusic(MusicType.lose);
        StartCoroutine(ReturnToTitle());
    }

    /// <summary>
    /// Return to title screen after seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator ReturnToTitle()
    {
        yield return new WaitForSecondsRealtime(AudioController.Instance.GetMusicLength(MusicType.lose) + 1);
        SceneManager.LoadScene("TitleScreen");
        Time.timeScale = 1;
    }
}
