using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawn;
    private float tizmedu;
    public float startt;

    // Update is called once per frame
    void Update()
    {
        if (tizmedu <= 0)
        {
            int rand = Random.Range(0, spawn.Length);
            Instantiate(spawn[rand], transform.position, Quaternion.identity);
            tizmedu = startt;
        }
        else
        {
            tizmedu -= Time.deltaTime;
        } 
    }
}
