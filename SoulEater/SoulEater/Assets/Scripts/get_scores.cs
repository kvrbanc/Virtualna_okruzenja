using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;

public class get_scores : MonoBehaviour
{
    public string URL = "";
    public int i = 0;

    [Serializable]
    public class User
    {
        public int id;
        public string username;
    }

    [Serializable]
    public class Score
    {
        public int id;
        public User user = new User();
        public int value;
    }

    [Serializable]
    public class Scores
    {
        //employees is case sensitive and must match the string "employees" in the JSON.
        public Score[] scores;
    }


    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getscores());
    }



    // koorutina za dobavljanje rezultata
    IEnumerator getscores()
    {
        Debug.Log("getscores");
        string logurl = URL + "scores/";
        Debug.Log(logurl);
        string token = PlayerPrefs.GetString("token");
        print(token);

        UnityWebRequest loginreq = UnityWebRequest.Get(logurl);
        loginreq.SetRequestHeader("Authorization", "Bearer" + token);

        yield return loginreq.SendWebRequest();
        if (loginreq.isNetworkError || loginreq.isHttpError)
        {
            Debug.Log(loginreq.error);
            yield break;
        }
        else
        {
            Debug.Log(loginreq.downloadHandler.text);


            string str = loginreq.downloadHandler.text;
            Scores[] igr = JsonHelper.FromJson<Scores>(str);

            Debug.Log(igr[0]);
            Debug.Log(igr[1]);

            Scores ps = JsonUtility.FromJson<Scores>(loginreq.downloadHandler.text.ToString());

        }

    }


}
