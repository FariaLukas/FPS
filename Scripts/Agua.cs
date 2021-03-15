using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Agua : MonoBehaviour
{
    public bool congelado;
    public Material gelo;
    public Material agua;
    float tempo;
    public float cooldown;
    public GameObject canvas;
    public Image temporizador;
    void Start()
    {

    }

    // Update is called once per frame
    void Update(){
       
        if(tempo > 0){
            tempo -= Time.deltaTime;
        }else{
           TrocaMaterial();
           congelado=false;
            gameObject.tag = "Agua";
            canvas.SetActive(false);
        }
       temporizador.fillAmount = tempo/cooldown;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Gelo")){
            if(!congelado){
                congelado=true;
                TrocaMaterial();
                canvas.SetActive(true);
                gameObject.tag = "Congelado";
                tempo = cooldown;
                
            }
        }
    }
    
   void OnTriggerStay(Collider other)
   {
       if(other.CompareTag("Gelo")){
           if(!congelado){
                congelado=true;
                TrocaMaterial();
                canvas.SetActive(true);
                gameObject.tag = "Congelado";
                tempo = cooldown;
               
           }
           
        }
   }

   void TrocaMaterial(){
       if(congelado){
            GetComponent<MeshRenderer>().material = gelo; 
       }   
        else{
            GetComponent<MeshRenderer>().material = agua;
        }
            
   }
}
