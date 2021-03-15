using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bot1 : MonoBehaviour
{
    private NavMeshAgent agent;

    Transform cavalo;

    [HideInInspector]
    public bool seePlayer;

    [HideInInspector]
    public bool dentro;

    public Transform muzzle;
    public GameObject vendo;
    public float campoDeVisao;
    public float raio;

    //public GameObject explosao;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    public void ChasePlayer()
    {
        agent.SetDestination(Controller.current.transform.position);
        vendo.SetActive(true);
    }

    public void ChaseCavalo()
    {
        if (cavalo != null)
            agent.SetDestination(cavalo.position);
        vendo.SetActive(true);
    }

    public bool SeePlayer()
    {
        RaycastHit hit;

        if (Physics.SphereCast(muzzle.position, raio, muzzle.forward, out hit, campoDeVisao)
        && hit.collider.CompareTag("Player")
        )
        {
            seePlayer = true;
        }
        else
        {
            if (!dentro)
                seePlayer = false;

            vendo.SetActive(false);
        }
        return seePlayer;

    }

    public bool SeeHorse()
    {
        RaycastHit hit;

        if (Physics.SphereCast(muzzle.position, raio, muzzle.forward, out hit, campoDeVisao)
        && hit.collider.CompareTag("Cavalo")
        )
        {
            cavalo = hit.transform;
            return true;
        }
        else
        {
            return false;
        }

    }

    public void teste()
    {
        agent.isStopped = true;
    }

    public void Fight(){
        //Instantiate(explosao,transform.position,Quaternion.identity);
        HandlePlayerMorte.current.Morte();
        //StartCoroutine(tempoMorte());
    }

    // private void OnDrawGizmos()
    // {
    //     RaycastHit hit;

    //     bool isHit = Physics.SphereCast(muzzle.position, raio, muzzle.forward, out hit, campoDeVisao);
    //     if (isHit)
    //     {
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawRay(muzzle.position, muzzle.forward * hit.distance);
    //         Gizmos.DrawWireSphere(muzzle.position + muzzle.forward * hit.distance, raio);
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.green;
    //         Gizmos.DrawRay(muzzle.position, muzzle.forward * campoDeVisao);
    //     }

    // }
    IEnumerator tempoMorte(){
        yield return new WaitForSeconds(2);
      
    }
}
