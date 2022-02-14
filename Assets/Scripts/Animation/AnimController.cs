using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public Animator animator;
    protected float length;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        length = animator.GetCurrentAnimatorStateInfo(0).length;
    }

    IEnumerator Deactive()
    {
        yield return new WaitForSeconds(length);
        gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(Deactive());
    }
}
