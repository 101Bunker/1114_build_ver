using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoboPatrol : MonoBehaviour
{
    public NavMeshAgent nav;
    public GameObject[] targets;
    private int point = 0;

    void Start()
    {
        next();
    }

    void Update()
    {
        if (!nav.pathPending && nav.remainingDistance < 2f) next();
    }

    void next()
    {
        if (targets.Length == 0) return;

        nav.destination = targets[point].gameObject.transform.position;
        point = (point + 1) % targets.Length;
    }
}
