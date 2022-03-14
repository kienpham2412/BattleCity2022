using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator tankAnimator;
    private Marker marker;
    private Rigidbody2D enemyRB;
    private State currentState;
    private bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        currentState = new Idle(gameObject, marker, enemyRB, tankAnimator);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        currentState = currentState.Process();
    }

    protected virtual void OnDisable()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }
        Referee.singleton.SpawnDestroyExplosion(gameObject.transform.position);
        Debug.Log("enemy destroyed");
    }
}
