using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    // private Transform position; 
    [SerializeField]
    private float waitDuration, speed;
    [SerializeField]
    private Transform[] targetDestination;
    [SerializeField]
    private bool useWait = false;
    private bool isArrive = false;
    private int i;

    void Start()
    {
        transform.position = targetDestination[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        isArrive = Vector2.Distance(transform.position, targetDestination[i].position) < 0.02f;
        if (isArrive)
        {
            i++;
            if (i == targetDestination.Length)
            {
                i = 0;
            }
        }
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetDestination[i].position, speed * Time.deltaTime);
        yield return new WaitUntil(() => isArrive);
        yield return new WaitForSeconds(waitDuration);
    }

}
