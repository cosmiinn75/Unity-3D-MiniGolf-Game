using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ScoreData
{
    public int strokes;
    public string timeDisplay; // Textul formatat (ex: "5:20")
    public float rawTime;      // Timpul în secunde (pentru sortare)

    public ScoreData(int s, string td, float rt)
    {
        strokes = s;
        timeDisplay = td;
        rawTime = rt;
    }
}

public static class BestScoreManager
{
    private const string SAVE_KEY = "HighScores";

    // Acum primim 3 argumente pentru a sorta corect
    public static void SaveNewScore(int strokes, string timeDisplay, float rawTime)
    {
        List<ScoreData> scores = LoadScores();
        scores.Add(new ScoreData(strokes, timeDisplay, rawTime));

        // SORTARE: Întâi după lovituri (mai puține e mai bine)
        // Apoi după timp (mai puține secunde e mai bine)
        var sortedScores = scores
            .OrderBy(s => s.strokes)
            .ThenBy(s => s.rawTime)
            .Take(5)
            .ToList();

        string json = JsonHelper.ToJson(sortedScores.ToArray());
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public static List<ScoreData> LoadScores()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY)) return new List<ScoreData>();

        string json = PlayerPrefs.GetString(SAVE_KEY);
        try
        {
            ScoreData[] loadedArray = JsonHelper.FromJson<ScoreData>(json);
            return loadedArray.ToList();
        }
        catch
        {
            // Dacă datele vechi sunt incompatibile, returnăm o listă goală
            return new List<ScoreData>();
        }
    }

    // Functia de reset
    public static void ClearScores()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
        Debug.Log("Scorurile au fost șterse!");
    }
}

// JsonHelper rămâne neschimbat sub BestScoreManager...
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

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}