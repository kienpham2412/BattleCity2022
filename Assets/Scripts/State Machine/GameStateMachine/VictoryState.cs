using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VictoryState : GameState
{
    public override void Perform()
    {
        gameObject.SetActive(true);
        StartCoroutine(ReturnToTitle());
    }

    IEnumerator ReturnToTitle()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("TitleScreen");
    }
}
