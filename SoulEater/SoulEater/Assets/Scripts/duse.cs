using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duse : MonoBehaviour
{
    public float brzina;
    private Vector2 newposition;

    // varijabla koja pohranjuje zvuk
    public AudioSource zvuk;

    void Start()
    {
        newposition = new Vector2(transform.position.x, transform.position.y);
        transform.position = newposition;

        // ucitavanje zvuka
        zvuk = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().PovecajBrojDusa(1);
            Debug.Log("Broj dusa: " + collision.GetComponent<PlayerMovement>().brdusa);
            // reprodukcija zvuka
            AudioSource.PlayClipAtPoint(zvuk.clip, transform.position);
            // uklanjanje samog objekta
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.left * brzina * Time.deltaTime);
    }
}
