using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Armas { CAVAL, GELO, RAY }
public class weapon : MonoBehaviour
{
    public Transform muzzle;
    public Transform muzzleProj;
    public Armas armas;
    public int qtdArmas = 0;


    [Header("Arma Decal")]
    public GameObject arma_Pistol;
    public GameObject hitDecal;
    public GameObject hitSemDecal;
    public GameObject pistolUI;
    public Animator pistolAnimator;


    [Header("Arma Projetil")]
    public GameObject arma_Cavalo;
    public GameObject bulletPrefab;
    public GameObject fumacaPrefab;
    public float municao;
    public GameObject projetilUI;
    public Text textMunicao;
    public Animator cavaloAnimator;



    [Header("Arma Particula")]
    public GameObject arma_Gelo;
    public GameObject colisor;
    public ParticleSystem particle;
    public GameObject particleUI;
    public Animator geloAnimator;

    [Header("Som")]
    AudioSource source;
    public AudioClip geloSom;
    public AudioClip cavaloSom;
    public AudioClip silentSom;
    public AudioClip pegarSom;


    static bool savepos = false;
    void Awake()
    {
        if (!savepos)
        {
            Save();
            savepos = true;
        }

        particle.Stop();
        Load();
        if (qtdArmas > 0)
        {

            arma_Cavalo.SetActive(true);

            // //UI
            projetilUI.SetActive(arma_Pistol.activeSelf);
        }
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!Controller.current.m_IsPaused)
        {

            if (Input.GetMouseButtonDown(0) && qtdArmas > 0)
            {
                if (armas == Armas.CAVAL)
                {
                    ShootObject();

                }
                if (armas == Armas.GELO)
                {
                    ShootParticle();
                }
                if (armas == Armas.RAY)
                {
                    ShootRaycast();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (armas == Armas.GELO)
                {

                    StopParticle();
                }

            }
            //CAVALO
            if (Input.GetKeyDown(KeyCode.Alpha1) && qtdArmas > 0)
            {
                armas = Armas.CAVAL;
                arma_Cavalo.SetActive(true);

                arma_Gelo.SetActive(false);
                arma_Pistol.SetActive(false);
            }
            //GELO
            if (Input.GetKeyDown(KeyCode.Alpha2) && qtdArmas >= 2)
            {
                armas = Armas.GELO;
                arma_Gelo.SetActive(true);

                arma_Cavalo.SetActive(false);
                arma_Pistol.SetActive(false);
            }

            //RAYCAST
            if (Input.GetKeyDown(KeyCode.Alpha3) && qtdArmas >= 3)
            {
                armas = Armas.RAY;
                arma_Pistol.SetActive(true);

                arma_Cavalo.SetActive(false);
                arma_Gelo.SetActive(false);

            }
            projetilUI.SetActive(arma_Cavalo.activeSelf);
            particleUI.SetActive(arma_Gelo.activeSelf);
            pistolUI.SetActive(arma_Pistol.activeSelf);
        }

    }

    //  Projetil
    void ShootObject()
    {

        if (municao > 0)
        {
            cavaloAnimator.SetTrigger("Shoot");
            municao--;
            textMunicao.text = municao.ToString();
            Instantiate(bulletPrefab, muzzleProj.position, muzzleProj.rotation);
            Instantiate(fumacaPrefab, muzzleProj.position, muzzleProj.rotation);
            source.PlayOneShot(cavaloSom);
        }


    }

    void ShootRaycast()
    {
        pistolAnimator.SetTrigger("Shoot");
        RaycastHit hit;

        if (Physics.Raycast(muzzle.position, muzzle.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Boom")
                hit.transform.GetComponent<Claymore>().Explodir();

            if (ChaoBoss.vulneravel && hit.transform.tag == "Inimigo")
            {

                ChaoBoss.vida.TakeDamage();
                ChaoBoss.vulneravel = false;
                ChaoBoss.pisouNoGelo = false;
            }
            //Decal
            if (hit.transform.tag == "Chao" || hit.transform.tag == "Parede")
                Instantiate(hitDecal, hit.point, Quaternion.LookRotation(hit.normal));
            else
                Instantiate(hitSemDecal, hit.point, Quaternion.LookRotation(hit.normal));

            source.PlayOneShot(silentSom);
        }
        else
        {

        }
    }

    void ShootParticle()
    {
        if (!particle.gameObject.activeSelf)
            particle.gameObject.SetActive(true);

        colisor.gameObject.SetActive(true);
        particle.Play();
        geloAnimator.SetTrigger("Shoot");

        source.Stop();
        source.loop = true;
        source.clip = geloSom;
        if(!source.isPlaying)
            source.Play();

    }

    void StopParticle()
    {
        source.loop = false;
        source.Stop();

        geloAnimator.SetTrigger("StopShoot");
        colisor.gameObject.SetActive(false);
        particle.Stop();
    }

    //Pegar armas e save
    void OnTriggerEnter(Collider other)
    {
        //1 por 1 pq quando faz load o checkpoint aumenta 
        if (other.tag == "Arma")
        {

            armas = Armas.CAVAL;
            arma_Cavalo.SetActive(true);
            qtdArmas = 1;
            PegarArma();
            Destroy(other.gameObject);
        }
        if (other.tag == "ArmaGelo")
        {
            qtdArmas = 2;
            PegarArma();
        }
        if (other.tag == "ArmaPistol")
        {
            qtdArmas = 3;
            PegarArma();
        }
        if (other.tag == "Checkpoint")
        {
            other.gameObject.SetActive(false);
            municao = 3;
            textMunicao.text = municao.ToString();
            Save();
        }

    }

    void PegarArma()
    {
        municao = 3;
        textMunicao.text = municao.ToString();
        Save();
        source.PlayOneShot(pegarSom);
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("x_pos", transform.position.x);
        PlayerPrefs.SetFloat("y_pos", transform.position.y);
        PlayerPrefs.SetFloat("z_pos", transform.position.z);
        PlayerPrefs.SetInt("qtdArma", qtdArmas);

    }

    public void Load()
    {
        qtdArmas = PlayerPrefs.GetInt("qtdArma");
    }
}
