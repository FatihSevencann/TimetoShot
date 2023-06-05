using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class HostageEscapeNavmesh : Instancable<HostageEscapeNavmesh>
{
    public NavMeshAgent agent;
    public Animator _hostageAnimator;
    
    public void Run()
    {
        if (_hostageAnimator != null)
        {
            _hostageAnimator.SetBool("IsRun",true);
            SetTargetPosition();
            StartCoroutine(WalkRandom());
        }
        
    }

    void SetTargetPosition()
    {
        
            Vector3 randomDirection = Random.insideUnitSphere * 10;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 10, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;
                agent.SetDestination(finalPosition);
                print("Set destination: " + finalPosition);
            }
    }

    private void Update()
    {
        if (!LevelManager.IsGameRunning)
            Destroy(gameObject);
    }

    IEnumerator WalkRandom()
    {
        while (LevelManager.IsGameRunning)
        {
            yield return new WaitForFixedUpdate();

            if (LevelManager.IsGameRunning && !agent.pathPending && agent.remainingDistance < 3f)
            {
                SetTargetPosition();
            }
        }
    }
}
