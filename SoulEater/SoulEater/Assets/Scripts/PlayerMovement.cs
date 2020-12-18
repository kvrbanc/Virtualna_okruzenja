using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 newposition;
    public float pomak;
    public float brzina;
    public float maxy;
    public float miny;
    public int brdusa = 0;
    public Text BrojacDusa;
    // dodan brojac ENERGIJE
    public Text trenutnaEnergija;

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

    // objekti koji ce predstvaljati "animacije"
    public GameObject animacijaZastite;



    // varijable koje ce spremati instancirene "animacije"
    private GameObject instancaAnimacijeZastite;

    void Start()
    {
        newposition = new Vector2(transform.position.x, transform.position.y);
        transform.position = newposition;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, newposition, brzina * Time.deltaTime);
        // pomakni i "animaciju" stita - ako je ima
        if (instancaAnimacijeZastite != null)
        {
            instancaAnimacijeZastite.transform.position = newposition;
        }
        // prikazi broj dusa
        BrojacDusa.text = brdusa.ToString();
        // prikazi energiju
        trenutnaEnergija.text = energija.ToString();

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

                // ukloni "animaciju" zastite
                Destroy(instancaAnimacijeZastite);
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
        //ako igrac dosad nije bio zasticen - postavi "animaciju" zastite
        if (!zasticen)
        {
            instancaAnimacijeZastite = Instantiate(animacijaZastite, transform.position, Quaternion.identity);
        }
      
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
            
            //ako je energija 0, prebaci na game over
            if ((energija == 0) || (energija < 0))
            {
                Debug.Log("game over");
                SceneManager.LoadScene(sceneName: "Game_over");
            }
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
