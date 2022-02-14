using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : Block
{
    [SerializeField]
    private GameObject shieldFX;
    private float immotalTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }

    private void SetImmmotal(bool isImmotal)
    {
        immotal = isImmotal;
        shieldFX.SetActive(isImmotal);
    }

    IEnumerator ImmotalActive()
    {
        SetImmmotal(true);
        yield return new WaitForSeconds(immotalTime);
        SetImmmotal(false);
    }

    private void OnEnable()
    {
        StartCoroutine(ImmotalActive());
    }
}
