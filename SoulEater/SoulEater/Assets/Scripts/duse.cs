using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duse : MonoBehaviour
{
    public float brzina;
    private Vector2 newposition;

    void Start()
    {
        newposition = new Vector2(transform.position.x, transform.position.y);
        transform.position = newposition;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().brdusa += 1;
            Debug.Log(collision.GetComponent<PlayerMovement>().brdusa);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.left * brzina * Time.deltaTime);
    }
}
