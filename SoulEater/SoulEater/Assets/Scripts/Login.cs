using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField userinput;
    public InputField passwinput;
    public Text loginStatus;

    public string URL = "";
    public string parameters = "{\"username\":\"string\",\"password\":\"string\"}";

    public string scenename;
    
        
    [Serializable]
    public class Player
    {
        public string password;
        public string username;
    }

    [Serializable]
    public class response
    {
        public string id;
        public string username;
        public string roles;
        public string tokenType;
        public string accessToken;
    }



    // Ako se klikne Login
    public void StartLogin()
        {
            Player igrac = new Player();
            igrac.username = userinput.text;
            igrac.password = passwinput.text;
            StartCoroutine(login(igrac));   
        }

    // Ako se klikne Register
    public void StartRegister()
    {
        Player igrac = new Player();
        igrac.username = userinput.text;
        igrac.password = passwinput.text;
        StartCoroutine(register(igrac));
    }

    IEnumerator login(Player info)
    {
        Debug.Log(info.username);
        string logurl = URL + "login/";

        UnityWebRequest loginreq = new UnityWebRequest(logurl, UnityWebRequest.kHttpVerbPOST);
        Debug.Log("{ \"username\":\"" + info.username + "\", \"password\":\"" + info.password + "\"}");
        byte[] bytes = Encoding.UTF8.GetBytes("{ \"username\":\"" + info.username + "\", \"password\":\"" + info.password + "\"}");
        UploadHandlerRaw uH = new UploadHandlerRaw(bytes);
        uH.contentType = "application/json";
        loginreq.uploadHandler = uH;
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
        loginreq.downloadHandler = dH;
        loginreq.SetRequestHeader("Content-Type", "application/json");

        yield return loginreq.SendWebRequest();
		if (loginreq.isNetworkError)
        {
            Debug.Log(loginreq.error);
            loginStatus.text = "There are some network connectivity issues";
            yield break;
        }
        else if (loginreq.isHttpError)
        {
            Debug.Log(loginreq.error);
            Debug.Log("Incorrect password and username combination");
            loginStatus.text = "Incorrect password and username combination";
            yield break;
        }
        else
        {
            Debug.Log("logiran");
            Debug.Log(scenename);
            PlayerPrefs.SetString("name", info.username);
            Debug.Log(loginreq.downloadHandler.text);
            response resp = JsonUtility.FromJson<response>(loginreq.downloadHandler.text);
            Debug.Log(resp.accessToken);
            PlayerPrefs.SetString("token", resp.accessToken);
            SceneManager.LoadScene(scenename);
        }

    }

    // Ako se klikne Register
    IEnumerator register(Player info)
    {
        Debug.Log(info.username);
        string logurl = URL + "register/";
        Debug.Log(logurl);

        UnityWebRequest loginreq = new UnityWebRequest(logurl, UnityWebRequest.kHttpVerbPOST);
        Debug.Log("{ \"username\":\"" + info.username + "\", \"password\":\"" + info.password + "\"}");
        byte[] bytes = Encoding.UTF8.GetBytes("{ \"username\":\"" + info.username + "\", \"password\":\"" + info.password + "\"}");
        UploadHandlerRaw uH = new UploadHandlerRaw(bytes);
        uH.contentType = "application/json";
        loginreq.uploadHandler = uH;
        loginreq.SetRequestHeader("Content-Type", "application/json");

        yield return loginreq.SendWebRequest();
        if (loginreq.isNetworkError)
        {
            Debug.Log(loginreq.error);
            loginStatus.text = "There are some network connectivity issues";
            yield break;
        }
        else if (loginreq.isHttpError)
        {
            Debug.Log(loginreq.error);
            Debug.Log("The user with this username already exists");
            loginStatus.text = "The user with this username already exists";
            yield break;
        }
        else
        {
            StartCoroutine(login(info));
        }

    }

}
