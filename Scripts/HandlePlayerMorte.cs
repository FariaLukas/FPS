using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerMorte : MonoBehaviour
{
    public static HandlePlayerMorte current;
    
    public bool morreu;

    void Awake(){
        current = this;
    }
        
    

    public void Morte(){
        morreu=true;
        Controller.current.LockControl=true;
        Controller.current.m_IsPaused =true;
    }
}
