using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public Text name;


    
    // Start is called before the first frame update
    void Update()
    {
        name.text = PlayerPrefs.GetString("name");
        name.text = "Name: " + name.text;
        Debug.Log(name.text);
    }
}
