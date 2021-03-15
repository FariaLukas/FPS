using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.UI;

public class ChaoBoss : MonoBehaviour
{
    #region Variaveis
    //plantar bomba
    public int index;
    public Action plantar;
    [HideInInspector]
    public Transform plantLocal;
    public bool podePlantar;
    public float alturaPlant;
    //prefab
    public GameObject mina;

    //andar
    NavMeshAgent agent;
    public Action restaurar;

    //pisou no gelo 
    [HideInInspector]
    public static bool pisouNoGelo;

    //tempo que demora para levantar
    public float tempoParado;

    //ataque
    public GameObject game;
    public ParticleSystem atack;
    //saber se o player esta em range para atacar
    [HideInInspector]
    public bool playerEntrou;
    [HideInInspector]
    public bool atacando;
    [HideInInspector]
    public bool acabouAtaque;


    public static Vida vida;
    float vidatotal;
    public static bool vulneravel;
    public Image vidaUI;

    public static bool endGame;

    [Header("Som")]
    public AudioSource bgsource;
    AudioSource source;
    public AudioClip passo1;
    public AudioClip passo2;
    #endregion

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vida = GetComponent<Vida>();
        atack.Stop();
        vidatotal = vida.vida;
        source = GetComponent<AudioSource>();
    }

    public void Update()
    {
        game.SetActive(playerEntrou);
        vidaUI.fillAmount = vida.vida / vidatotal;
    }
    //posicao para plantar bomba
    public void IrParaPosicao()
    {
        if (plantLocal != null)
            agent.SetDestination(plantLocal.position);
    }

    //posicionar bomba no local
    public void Colocar()
    {
        Vector3 local = new Vector3(plantLocal.position.x, alturaPlant, plantLocal.position.z);
        Instantiate(mina, local, Quaternion.identity);
    }


    public void TerminouAnima()
    {
        podePlantar = false;
    }

    //uso pra escolher o tempo que o ataque fica ligado
    public void Ataque()
    {
        atacando = !atacando;
        atack.gameObject.SetActive(atacando);
        if (atack.isStopped)
            atack.Play();
    }

    //terminou de fazer o ataque
    public void Acabou()
    {
        acabouAtaque = true;
        atack.Stop();
    }


    public void SomBG(AudioClip clip)
    {
        bgsource.Stop();
        bgsource.clip = clip;
        bgsource.loop = true;
        bgsource.Play();
    }
    public void ResturarChao()
    {
        restaurar?.Invoke();
    }

    public void DanoPlayer()
    {
        HandlePlayerMorte.current.Morte();
    }

    public void Passo1()
    {
        source.PlayOneShot(passo1,0.5f);

    }
    public void Passo2()
    {
        source.PlayOneShot(passo2, 0.5f);

    }


    void OnTriggerEnter(Collider collision)
    {
        //a cada 5 blocos que ele percorrer ele planta uma bomba
        if (collision.gameObject.tag == "Chao" && !atacando)
        {
            index++;
            if (index == 5)
            {
                plantLocal = collision.transform;
                StartCoroutine(Timer());
                index = 0;
            }


        }
        //se pisar no gelo ele escorrega
        if (collision.gameObject.tag == "Congelado")
        {
            pisouNoGelo = true;
            StartCoroutine(Parado());
        }

    }


    //A o trigger tava atualizando muito cedo e pegando o anterior
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        plantar?.Invoke();
    }

    IEnumerator Parado()
    {
        yield return new WaitForSeconds(5f);
        pisouNoGelo = false;
    }

}
