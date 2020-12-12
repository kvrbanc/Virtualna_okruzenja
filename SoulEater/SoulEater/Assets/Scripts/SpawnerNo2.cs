using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNo2 : MonoBehaviour
{
    // polje koje sadrzi objekte koji se mogu spawnati -> objekti[0] ce biti "dusa"
    public GameObject[] objekti;
    // preostalo vrijeme do spawna
    private float vrijemeDoSpawna;
    // vrijeme izmedu spawna
    public float vrijemeIzmeduSpawnova;
    // polje koje sadrzi spawnpointe
    private Vector3[] spawnPoints = {
        new Vector3(8, 4, 0),
        new Vector3(8, 2, 0),
        new Vector3(8, 0, 0),
        new Vector3(8, -2, 0),
        new Vector3(8, -4, 0)
    };


    // za svaki frame
    void Update()
    {
        if (vrijemeDoSpawna <= 0)
        {
            // uvijek treba generirati baren jednu dusu -> ona se nalazi na objekti[0]
            int mjestoDuse = Random.Range(0, spawnPoints.Length);
            Instantiate(objekti[0], spawnPoints[mjestoDuse], Quaternion.identity);

            // generiranje preostalih objekata -> random izbor
            for(int brojac = 0; brojac < spawnPoints.Length; brojac++)
            {
                //ako se "brojac" nasao na "mjestoDuse" preskoci iteraciju
                if (brojac == mjestoDuse)
                {
                    continue;
                }

                // generiraj random indeks objekta koji ce se iscratati na spawnPoint[brojac]
                int randIndeks = Random.Range(0, objekti.Length);
                // instanciraj objekt
                Instantiate(objekti[randIndeks], spawnPoints[brojac], Quaternion.identity);

            }
           
            // postavi vrijeme na pocetno
            vrijemeDoSpawna = vrijemeIzmeduSpawnova;
        }
        else
        {   
            // ako ima jos vremena do spawna, umanji ga
            vrijemeDoSpawna -= Time.deltaTime;
        }
    }
}
