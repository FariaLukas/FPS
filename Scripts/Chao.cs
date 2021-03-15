using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Chao : MonoBehaviour
{

    public bool plantado;
    public bool colidino;
    ChaoBoss boss;
    public Action desativar;
    public bool control;

    void Start()
    {
        boss = FindObjectOfType<ChaoBoss>();
        boss.plantar += AllowPlant;
        boss.restaurar += Ativar;
    }

    public void AllowPlant()
    {
        if (colidino && !plantado)
        {
            boss.podePlantar = true;
            plantado = true;
        }

    }

    //Ativar quando o boss passar
    public void Ativar()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            plantado = false;
        }

        desativar?.Invoke();

    }
    //desativar quando explodir
    public void Desativ()
    {
        gameObject.SetActive(false);
        plantado = false;
        desativar?.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Inimigo")
        {
            colidino = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Inimigo")
        {
            colidino = false;
        }

    }

}
