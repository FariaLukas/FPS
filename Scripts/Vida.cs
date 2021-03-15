using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public float vida;
    AudioSource source;

    public AudioClip dano;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void TakeDamage()
    {
        vida--;
        source.PlayOneShot(dano);
    }
}
