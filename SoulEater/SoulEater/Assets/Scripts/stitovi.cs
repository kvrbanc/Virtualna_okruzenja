using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stitovi : MonoBehaviour
{
    // brzina kojom se stit krece ulijevo
    public float brzina;
    // pocetna pozicija stita
    private Vector2 newposition;
    // trajanje zastite koju ce igrac dobiti - u sekundama
    public int trajanjeStita = 2;


    void Start()
    {
        // odredivanje i postavljanje pocetne pozicije stita
        newposition = new Vector2(transform.position.x, transform.position.y);
        transform.position = newposition;
    }

    // kada se aktivira "collider"
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ako se dogodio sudar sa igracem
        if (collision.CompareTag("Player"))
        {
            // igrac se postavi / poveca trajanje zastite
            collision.GetComponent<PlayerMovement>().Zastiti(trajanjeStita);
            // kontrolni ispis 
            Debug.Log("Igraceva zastita povecana za "+ trajanjeStita + " sekunde.");
            // uklanjanje samog objekta
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // pomak objekta za svaki frame
        transform.Translate(Vector2.left * brzina * Time.deltaTime);
    }
}

