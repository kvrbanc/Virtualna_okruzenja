using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    // vrijeme nakon koliko ce se objekt unistiti
    public float vrijemePostojanja;

    // Objekt se ukloni nakon odredenog vremena 
    void Start()
    {
        Destroy(gameObject, vrijemePostojanja);
    }

}
