using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    public float gizmoSize = 1;
    int currentWaypoint= -1;
    AudioSource source;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
    }

    public void ChangeWaypoint(){
         currentWaypoint++;

            if(currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
    }

    public void MoveToWaypoint(){
        agent.SetDestination(waypoints[currentWaypoint].position);
    }

    public float Remaing(){
        return agent.remainingDistance;
    }
     public void SomBoss(AudioClip clip){
        source.PlayOneShot(clip);
    }

    //     private void OnDrawGizmos()
    // {
    //     for( int i = 0; i < waypoints.Length; i++)
    //     {
    //         if(i+1 < waypoints.Length)
    //             Gizmos.DrawLine(waypoints[i].position, waypoints[i+1].position);
    //         else
    //             Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);

    //         Gizmos.DrawSphere(waypoints[i].position, gizmoSize);

    //     }

    // }
}
