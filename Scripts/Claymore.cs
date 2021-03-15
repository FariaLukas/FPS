using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claymore : MonoBehaviour
{
    float fieldOfView;
    public float angulo;
    public float raio;
    bool explode;
    public GameObject explosao;
    AudioSource source;
    public AudioClip boom;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        float dist = Vector3.Distance(Controller.current.transform.position, transform.position);
        if (dist <= 3 && !HandlePlayerMorte.current.morreu)
        {

            HandlePlayerMorte.current.Morte();
            Explodir();

        }


    }



    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Chao" && explode)
        {
            other.gameObject.GetComponent<Chao>().Desativ();
        }
    }


    public void Explodir()
    {
        explode = true;
        Instantiate(explosao, transform.position, Quaternion.identity);
        source.PlayOneShot(boom);
        Destroy(gameObject, 0.1f);
    }


}
