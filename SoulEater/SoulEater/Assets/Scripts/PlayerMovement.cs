﻿using System.Collections;
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

    // dodana ENERGIJA
    // trenutni iznos energije
    public int energija = 25;
    // maksimalni iznos energije
    public int maksEnergija = 25;


    // dodana ZASTITA
    // trajanje zastite koju daju stitovi - u sekundama
    private float trajanjeZastite = 0.0f;
    // po default-u igrac nije zasticen
    private bool zasticen = false;


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

        // ako je igrac zasticen , umanji trajnaje zastite
        if (zasticen)
        {
            trajanjeZastite -= Time.deltaTime;
            // ako trajanje zastite otide ispod nule, igrac vise nije zasticen
            if(trajanjeZastite <= 0.0f)
            {
                trajanjeZastite = 0.0f;
                zasticen = false;
            }
        }
    }


    // metoda koju zove DUSA - igracu se povecava brojac dusa
    public void PovecajBrojDusa(int brojDusa)
    {
        brdusa += brojDusa;
    }


    // metoda koju aktivira STIT - igrac se stiti od smanjenja energije
    public void Zastiti(int trajanjeStita)
    {   
        // postavi igraca u "zasticeno stanje"
        zasticen = true;
        // uvecaj trajanje zastite
        trajanjeZastite += trajanjeStita;
    }


    // metoda koju aktivira ANDEO - igracu se oduzima energija
    public void UmanjiEnergiju(int stetaAndela)
    {
        // ako igrac nije zasticen, umanji mu energiju
        if (!zasticen)
        {
            energija -= stetaAndela;
            // tu ce se dodat uvjet za GAME OVER - kad energija padne ispod 0
        }
    }


    //metoda koju poziva TAMNA ENERGIJA - igracu se povecava energija
    public void PovecajEnergiju(int povecanjeEnergije)
    {   
        energija += povecanjeEnergije;
        // ogranici energiju na maksimum
        if(energija > maksEnergija)
        {
            energija = maksEnergija;
        }
    }



}
