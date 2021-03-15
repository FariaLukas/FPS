using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleChao : MonoBehaviour
{
  public GameObject agua;
  public Chao ground;


  void Start(){
      ground.desativar += TrocaVisibilidade;
  }

  void TrocaVisibilidade(){
      agua.SetActive(!ground.gameObject.activeSelf);
      StartCoroutine(tempoExplode());
  }


 IEnumerator tempoExplode(){
     yield return new WaitForSeconds(0.5f);
    
 }
}
