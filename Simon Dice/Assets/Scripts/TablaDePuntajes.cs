using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablaDePuntajes : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");


        entryTemplate.gameObject.SetActive(false);

        AddHighscoreEntry(10, "JLP");
    
        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        Highscores highscores =  JsonUtility.FromJson<Highscores>(jsonString);
        
        for (int i = 0;  i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j <highscores.highscoreEntryList.Count; j++)
            {
                if(highscores.highscoreEntryList[j].score> highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }


        highscoreEntryTransformList = new List<Transform>();
        foreach(HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

    }
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        {
            float templateHeight = 30f; 
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);


            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)
            {
                default:
                rankString = rank + "TH"; break;
                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }
            entryTransform.Find("Position text").GetComponent<Text>().text = rankString;
            int score = highscoreEntry.score;
            entryTransform.Find("Score text").GetComponent<Text>().text = score.ToString();
            string name = highscoreEntry.name;
            entryTransform.Find("Name text").GetComponent<Text>().text = name;
            transformList.Add(entryTransform);
        }

    }

    private void  AddHighscoreEntry(int score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
       
        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        if (JsonUtility.FromJson<Highscores>(jsonString) == null)
        {
            Debug.Log("oh no");
        }
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        Debug.Log(highscores);
       /* Debug.Log(highscores.highscoreEntryList);
        Debug.Log(highscoreEntry);*/
        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighScoreTable", json);
        PlayerPrefs.Save();

    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
