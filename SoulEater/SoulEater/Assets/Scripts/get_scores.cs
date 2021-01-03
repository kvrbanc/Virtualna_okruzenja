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
        string logurl = URL + "scores/top";
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


            string str = loginreq.downloadHandler.text.ToString();
            Scores playerScores = JsonUtility.FromJson<Scores>("{\"scores\":" + str + "}");

            int position = 0;

            string scoresOutput = "";
            foreach (Score playerScore in playerScores.scores)
            {
                position++;
                string username = playerScore.user.username;
                int value = playerScore.value;
                scoresOutput = scoresOutput + position + ". " + username + " " + value + "\n";
            }
            
            //score output ispisati na ekran jedan ispod drugog

            Debug.Log("Scores list result:" + scoresOutput);


        }

    }


}
