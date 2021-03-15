using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 force;

    Vector3 oldPosition;


    void OnEnable()
    {
        rb.AddRelativeForce(force,ForceMode.Impulse);
        Destroy(gameObject,7);
    }

    private void Update(){
         RaycastHit hit;

            if(Physics.Linecast (transform.position,oldPosition, out hit) ) 
            {
               
            }

            oldPosition = transform.position;
    }

}
