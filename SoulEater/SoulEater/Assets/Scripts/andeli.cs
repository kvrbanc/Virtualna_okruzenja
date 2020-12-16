using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class andeli : MonoBehaviour
{
    // brzina kojom se andeo krece ulijevo
    public float brzina;
    // pocetna pozicija andela
    private Vector2 newposition;
    // steta koju ce andeo napraviti igracu
    private int steta;
    // parametar koji odreduje maksimalnu stetu 
    public int maksSteta = 5;


    // varijabla koja pohranjuje zvuk
    public AudioSource zvuk;


    void Start()
    {   
        // odredivanje i postavljanje pocetne pozicije andela
        newposition = new Vector2(transform.position.x, transform.position.y);
        transform.position = newposition;
        // odredivanje stete andela - nasumicni broj
        steta = Random.Range(0, maksSteta);

        // ucitavanje zvuka
        zvuk = GetComponent<AudioSource>();

    }

    // kada se aktivira "collider"
    void OnTriggerEnter2D(Collider2D collision)
    {   
        // ako se dogodio sudar sa igracem
        if (collision.CompareTag("Player"))
        {       
            // igracu se oduzme energija
            collision.GetComponent<PlayerMovement>().UmanjiEnergiju(steta);
            // kontrolni ispis energije
            Debug.Log( "Energija: " + collision.GetComponent<PlayerMovement>().energija);

            // reprodukcija zvuka
            AudioSource.PlayClipAtPoint(zvuk.clip, transform.position);
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

