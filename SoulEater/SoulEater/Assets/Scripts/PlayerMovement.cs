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
    public GameObject animacijaHeal;
    public GameObject animacijaStete;
    public GameObject animacijaSakuplanjaDuse;
    public GameObject animacijaSakupljanjaStita;
    public GameObject animacijaDobivanjaPointa;


    // varijable koje ce spremati instancirene "animacije"
    private GameObject instancaAnimacijeZastite;
    private GameObject instancaHealAnimacije;
    private GameObject instancaAnimacijeStete;
    private GameObject instancaAnimacijeSakDuse;
    private GameObject instancaAnimacijeSakStita;
    private GameObject instancaAnimacijeDobPointa;

    void Start()
    {
        newposition = new Vector2(transform.position.x, transform.position.y);
        transform.position = newposition;
    }

    void Update()
    {
        // pomakni igraca
        transform.position = Vector2.MoveTowards(transform.position, newposition, brzina * Time.deltaTime);
        // pomakni i "animaciju" stita - ako je ima
        if (instancaAnimacijeZastite != null)
        {
            // mala izmjena pozicije
            Vector3 pozicijaInstance = newposition;
            pozicijaInstance.y = pozicijaInstance.y - 0.10f;
            instancaAnimacijeZastite.transform.position = pozicijaInstance;
        }
        // pomakni i "animaciju" sakupljanja stita - ako je ima
        if (instancaAnimacijeSakStita != null)
        {
            // mala izmjena pozicije
            Vector3 pozicijaInstance = newposition;
            pozicijaInstance.x = pozicijaInstance.x + 0.50f;
            pozicijaInstance.y = pozicijaInstance.y - 0.10f;
            instancaAnimacijeSakStita.transform.position = pozicijaInstance;
        }
        // pomakni i "animaciju" heal-anja - ako je ima
        if (instancaHealAnimacije != null)
        {
            // mala izmjena pozicije
            Vector3 pozicijaInstance = newposition;
            pozicijaInstance.y = pozicijaInstance.y + 1.0f;
            instancaHealAnimacije.transform.position = pozicijaInstance;
        }
        // pomakni i "animaciju" stete - ako je ima
        if (instancaAnimacijeStete != null)
        {
            // mala izmjena pozicije
            Vector3 pozicijaInstance = newposition;
            pozicijaInstance.x = pozicijaInstance.x + 0.6f;
            instancaAnimacijeStete.transform.position = pozicijaInstance;
        }
        // pomakni i "animaciju" sakuplanja duse - ako je ima
        if (instancaAnimacijeSakDuse != null)
        {
            // mala izmjena pozicije
            Vector3 pozicijaInstance = newposition;
            pozicijaInstance.x = pozicijaInstance.x - 1.0f;
            pozicijaInstance.y = pozicijaInstance.y + 0.3f;
            instancaAnimacijeSakDuse.transform.position = pozicijaInstance;
        }
        // pomakni i "animaciju" dobivanja point-a  - ako je ima
        if (instancaAnimacijeDobPointa != null)
        {
            // mala izmjena pozicije
            Vector3 pozicijaInstance = newposition;
            pozicijaInstance.x = pozicijaInstance.x + 0.50f;
            pozicijaInstance.y = pozicijaInstance.y - 0.1f;
            instancaAnimacijeDobPointa.transform.position = pozicijaInstance;
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
        // uvecaj nroj dusa
        brdusa += brojDusa;

        // postavi "animaciju" sakupljanja i dobivanja pointa
        instancaAnimacijeSakDuse = Instantiate(animacijaSakuplanjaDuse, transform.position, Quaternion.identity);
        instancaAnimacijeDobPointa = Instantiate(animacijaDobivanjaPointa, transform.position, Quaternion.identity);
        // unisti "animaciju" nakon 0.25 sekunde
        Destroy(instancaAnimacijeSakDuse, 0.25f);
        Destroy(instancaAnimacijeDobPointa, 0.25f);
    }


    // metoda koju aktivira STIT - igrac se stiti od smanjenja energije
    public void Zastiti(int trajanjeStita)
    {
        //ako igrac dosad nije bio zasticen - postavi "animaciju" zastite
        if (!zasticen)
        {
            instancaAnimacijeZastite = Instantiate(animacijaZastite, transform.position, Quaternion.identity);
        }

        // postavi "animaciju" sakupljanja stita
        instancaAnimacijeSakStita = Instantiate(animacijaSakupljanjaStita, transform.position, Quaternion.identity);
        // unisti "animaciju" nakon 0.25 sekundi
        Destroy(instancaAnimacijeSakStita, 0.25f);
      
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

            // postavi "animaciju" stete
            instancaAnimacijeStete = Instantiate(animacijaStete, transform.position, Quaternion.identity);
            // unisti "animaciju" nakon 0.25 sekunde
            Destroy(instancaAnimacijeStete, 0.25f);

            // umanji energiju
            energija -= stetaAndela;
            
            //ako je energija 0, prebaci na game over
            if ((energija == 0) || (energija < 0))
            {
                // postavi energiju na 0
                energija = 0;
                Debug.Log("game over");
                SceneManager.LoadScene(sceneName: "Game_over");
            }
        }
    }


    //metoda koju poziva TAMNA ENERGIJA - igracu se povecava energija
    public void PovecajEnergiju(int povecanjeEnergije)
    {   
        // ako energija nije na maksimumu
        if(energija < maksEnergija)
        {   
            // povecaj energiju
            energija += povecanjeEnergije;

            // postavi "animaciju" heal-anja
            instancaHealAnimacije = Instantiate(animacijaHeal, transform.position, Quaternion.identity);
            // unisti "animaciju" nakon 0.25 sekunde
            Destroy(instancaHealAnimacije, 0.25f);

            // ogranici energiju na maksimum
            if (energija > maksEnergija)
            {
                energija = maksEnergija;
            }
        }

    }



}
