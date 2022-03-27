using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : Block
{
    [SerializeField] private GameObject shieldFX;
    public int playerHealth;
    private float immotalTime;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    private void SetImmmotal(bool isImmotal)
    {
        immotal = isImmotal;
        shieldFX.SetActive(isImmotal);
    }

    public void ResetHealth()
    {
        health = playerHealth;
    }

    IEnumerator ImmotalActive()
    {
        SetImmmotal(true);
        yield return new WaitForSeconds(immotalTime);
        SetImmmotal(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Helmet"))
        {
            immotalTime = 10f;
            StartCoroutine(ImmotalActive());
        }
    }

    private void OnEnable()
    {
        immotalTime = 5f;
        StartCoroutine(ImmotalActive());
    }
}
