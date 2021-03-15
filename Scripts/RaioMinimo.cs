using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioMinimo : MonoBehaviour
{
    public Bot1 enemi;
    // Start is called 
    void OnTriggerStay(Collider other){
      
          if(other.CompareTag("Player")){
              enemi.dentro = true;
              enemi.seePlayer = true;
          }
             
    }
    void OnTriggerExit(Collider other){
      
          if(other.CompareTag("Player")){
              enemi.dentro = false;
          }
             
    }
}
