using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerNo2 : MonoBehaviour
{
    // polje koje sadrzi objekte koji se mogu spawnati -> objekti[0] ce biti "dusa"
    public GameObject[] objekti;
    // preostalo vrijeme do spawna
    private float vrijemeDoSpawna;
    // vrijeme izmedu spawna - ide od 0.9 sekunde do 0.5 sekunde ovisno o levelu
    public float pocetnoVrijemeIzmeduSpawnova = 0.9f;
    public float minVrijemeIzmeđuSpawnova = 0.5f;
    private float vrijemeIzmeduSpawnova;

    // brzine generiranih objekata
    public float pocetnaBrzinaObjekata = 10f;
    public float maksBrzinaObjekata = 13f;

    // polje koje sadrzi spawnpointe
    private Vector3[] spawnPoints = {
        new Vector3(8, 4, 0),
        new Vector3(8, 2, 0),
        new Vector3(8, 0, 0),
        new Vector3(8, -2, 0),
        new Vector3(8, -4, 0)
    };
    // lista mjesta na kojima se vise ne mogu instancirati objekti
    private List<int> zauzetaMjesta = new List<int>(); // incijalizacija zbog kasnije provjere


    // varijabla koja sprema trenutni level
    public int trenutniLevel = 1;



    void Start()
    {
        //postavi vrijeme izmedu spawnova
        vrijemeIzmeduSpawnova = pocetnoVrijemeIzmeduSpawnova;
    }

    // za svaki frame
    void Update()
    {
        if (vrijemeDoSpawna <= 0)
        {   
            // uvijek treba generirati baren jednu dusu -> ona se nalazi na objekti[0]
            int mjestoDuse = Random.Range(0, spawnPoints.Length - 1);
            zauzetaMjesta.Add(mjestoDuse);
            Instantiate(objekti[0], spawnPoints[mjestoDuse], Quaternion.identity);
            
            // generiranje andela oko duše -> oni se nalaze na objekti[1]
            if (mjestoDuse == 0)
            {
                // ako je dusa na najvisem mjestu generiraj andela ispod duse
                Instantiate(objekti[1], spawnPoints[mjestoDuse + 1], Quaternion.identity);
                zauzetaMjesta.Add(mjestoDuse + 1);
            }
            else if (mjestoDuse == spawnPoints.Length - 1)
            {
                //ako je dusa na najdonjem mjestu generiraj andela iznad
                Instantiate(objekti[1], spawnPoints[mjestoDuse - 1], Quaternion.identity);
                zauzetaMjesta.Add(mjestoDuse - 1);
            }
            else
            {
                //ako je dusa negdje izmedu, generiraj 2 andela
                Instantiate(objekti[1], spawnPoints[mjestoDuse + 1], Quaternion.identity);
                Instantiate(objekti[1], spawnPoints[mjestoDuse - 1], Quaternion.identity);
                zauzetaMjesta.Add(mjestoDuse + 1);
                zauzetaMjesta.Add(mjestoDuse - 1);
            }

            // generiranje preostalih objekata -> random izbor
            for (int brojac = 0; brojac < spawnPoints.Length; brojac++)
            {
                //ako se "brojac" nasao na mjestu koje je "zauzeto" drugim objektom - preskoci iteraciju
                if (zauzetaMjesta.Contains(brojac))
                {
                    continue;
                }

                // generiraj random indeks objekta koji ce se iscratati na spawnPoint[brojac]
                int randIndeks = Random.Range(0, objekti.Length);
                // instanciraj objekt
                Instantiate(objekti[randIndeks], spawnPoints[brojac], Quaternion.identity);

            }
            // reinicijaliziraj listu 
            zauzetaMjesta.Clear();
            // postavi vrijeme na pocetno
            vrijemeDoSpawna = vrijemeIzmeduSpawnova;
        }
        else
        {   
            // ako ima jos vremena do spawna, umanji ga
            vrijemeDoSpawna -= Time.deltaTime;
        }
    }


    // funkcija koja se poziva kada se promijeni level
    public void postaviLevel(int level)
    {   
        // postavi trenutni level
        trenutniLevel = level;
        // izmjeni vrijeme između spawnova - smanjuje se linearno od maskimalnog do minimalnog vremena
        vrijemeIzmeduSpawnova = pocetnoVrijemeIzmeduSpawnova - ((pocetnoVrijemeIzmeduSpawnova - minVrijemeIzmeđuSpawnova)/14f)*trenutniLevel;

        // izmjeni brzine generiranih objekata

        float promjenaBrzine =((pocetnaBrzinaObjekata - maksBrzinaObjekata) / 14f)* trenutniLevel;

        GameObject andeo = GameObject.FindGameObjectsWithTag("Angel")[0];
        GameObject dusa = GameObject.FindGameObjectsWithTag("Soul")[0];
        GameObject stit = GameObject.FindGameObjectsWithTag("Shield")[0];
        GameObject energija = GameObject.FindGameObjectsWithTag("Energy")[0];

        andeo.GetComponent <andeli> ().izmjeniBrzinu(promjenaBrzine);
        dusa.GetComponent<duse>().izmjeniBrzinu(promjenaBrzine);
        stit.GetComponent<stitovi>().izmjeniBrzinu(promjenaBrzine);
        energija.GetComponent<tamneEnergije>().izmjeniBrzinu(promjenaBrzine);
    }
}
