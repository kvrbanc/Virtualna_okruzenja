using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 newposition;
    public float pomak;
    public float brzina;
    public float maxy;
    public float miny;
    public int brdusa = 0;
    public Text BrojacDusa;


    void Start()
    {
        newposition = new Vector2(transform.position.x, transform.position.y);
        transform.position = newposition;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, newposition, brzina * Time.deltaTime);
        BrojacDusa.text = brdusa.ToString();

        if (Input.GetKeyDown(KeyCode.UpArrow) && (transform.position.y < maxy))
        {
            newposition = new Vector2(transform.position.x, transform.position.y + pomak);
        } 
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (transform.position.y > miny))
        {
            newposition = new Vector2(transform.position.x, transform.position.y - pomak);
        }
    }
}
