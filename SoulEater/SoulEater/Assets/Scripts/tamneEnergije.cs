using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamneEnergije : MonoBehaviour
{
    // brzina kojom se tamna energija krece ulijevo
    public float brzina;
    // pocetna pozicija tamne energije
    private Vector2 newposition;
    // povecanje energije koje ce igrac dobitit igracu
    public int povecanjeEnergije = 1;

    // varijabla koja pohranjuje zvuk
    public AudioSource zvuk;

    // varijabla koja pohranjuje igraca
    private PlayerMovement igrac;


    void Start()
    {
        // odredivanje i postavljanje pocetne pozicije tamne energije
        newposition = new Vector2(transform.position.x, transform.position.y);
        transform.position = newposition;

        // ucitavanje zvuka
        zvuk = GetComponent<AudioSource>();
    }

    // kada se aktivira "collider"
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ako se dogodio sudar sa igracem
        if (collision.CompareTag("Player"))
        {
            // igracu se poveca energija
            igrac = collision.GetComponent<PlayerMovement>();
            // kontrolni ispis energije
            Debug.Log("Energija: " + collision.GetComponent<PlayerMovement>().energija);

            // ako je energija igraca manja od maksimalne
            if (igrac.energija < igrac.maksEnergija) 
            {
                // povecaj energiju
                igrac.PovecajEnergiju(povecanjeEnergije);
                // reprodukcija zvuka
                AudioSource.PlayClipAtPoint(zvuk.clip, transform.position);
            }
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

