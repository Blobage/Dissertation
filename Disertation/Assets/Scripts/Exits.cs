using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exits : MonoBehaviour
{

    [SerializeField]
    public bool used = false;

    void OnTriggerStay(Collider other)
    {
        //print("Colliding " + other.tag);
        if(other.tag == "Exit")
        {
            used = true;
        }
    }
}
