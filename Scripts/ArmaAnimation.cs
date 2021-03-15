using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ArmaAnimation : MonoBehaviour
{
    // Start is called before the first frame update
   public float speed;
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
