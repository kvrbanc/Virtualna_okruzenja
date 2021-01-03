using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    //polje sa svim pozadinama
    public GameObject[] pozadine;

    // varijabla koja sprema trenutno iscrtanu pozadinu
    private GameObject trenutnaPozadina;

    // Ştart se poziva na pocetku
    void Start()
    {
        // iscrtaj prvu pozadinu
        trenutnaPozadina = Instantiate(pozadine[0], transform.position, Quaternion.identity);
    }

    // metoda za izmjenu pozadine
    public void izmjenaPozadine(int level)
    {
        // ukloni trenutnu pozadinu
        Destroy(trenutnaPozadina);

        // iscrtaj novu pozadinu 
        int brojPozadine = (level - 1) % 7;
        trenutnaPozadina = Instantiate(pozadine[brojPozadine], transform.position, Quaternion.identity);
    }
}
