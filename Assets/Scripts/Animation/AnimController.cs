using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;
    protected float length;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        length = animator.GetCurrentAnimatorStateInfo(0).length;
    }

    /// <summary>
    /// Countdown to disable the attached gameobject
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Deactive()
    {
        yield return new WaitForSeconds(length);
        gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(Deactive());
    }
}
