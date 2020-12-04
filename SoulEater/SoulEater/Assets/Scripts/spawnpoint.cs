using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnpoint : MonoBehaviour
{
    public GameObject objekt;
    void Start()
    {
        Instantiate(objekt, transform.position, Quaternion.identity);
    }
}
