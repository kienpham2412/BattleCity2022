using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnim : AnimController
{
    private IEnumerator countDown;
    private const float LENGTH = 20f;
    private const float FADE_MOMENT = 5f;
    private float currentTime;
    
    protected override IEnumerator Deactive()
    {
        while (currentTime > 0)
        {
            if (currentTime == FADE_MOMENT)
            {
                animator.SetBool("isFading", true);
            }

            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        currentTime = LENGTH;
        animator.SetBool("isFading", false);

        if (countDown != null)
        {
            StopCoroutine(countDown);
        }
        countDown = Deactive();
        StartCoroutine(countDown);
    }

    protected void OnDisable()
    {
        StopCoroutine(countDown);
    }
}
