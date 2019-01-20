using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public class HighScoreHandler : MonoBehaviour
{
    // Static global instance of the class
    public static HighScoreHandler Instance;

    #region public members

    public string PathToFile = "HIGHSCORE.json";

    #endregion

    #region private members

    private List<HighScoreElement> _highScoreList;

    #endregion

    #region basic unity functions

    // Awake is called before Start()
    void Awake()
    {
        // set the reference to the current instance
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region public funktions

    public void ReadHighScoreList()
    {
        // if the high score file doesn't exist create it
        if (!File.Exists(PathToFile))
        {
            // create the dictionary 
            _highScoreList = new List<HighScoreElement>();
        }
        else
        {
            _highScoreList = JsonConvert.DeserializeObject<List<HighScoreElement>>(File.ReadAllText(PathToFile));
        }
    }

    public void WriteHighScoreList()
    {
        try
        {
            File.WriteAllText(PathToFile, JsonConvert.SerializeObject(_highScoreList));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddScore(string playerName, int score)
    {
        ReadHighScoreList();
        
        _highScoreList.Add(new HighScoreElement(playerName, score));

        _highScoreList.Sort((a, b) => (a.Score.CompareTo(b.Score) * (-1)));

        WriteHighScoreList();
    }

    public string GetBestPlayers()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < 20 && i < _highScoreList.Count; i++)
        {
            sb.Append(i+1 + ". " + _highScoreList[i].PlayerName + " -> " + _highScoreList[i].Score + "\r\n");
        }

        return sb.ToString();
    }

    public void ResetHighScore()
    {
        _highScoreList = new List<HighScoreElement>();
        File.Delete(PathToFile);
    }

    #endregion
}

internal class HighScoreElement
{
    public string PlayerName { get; private set; }
    public int Score { get; private set; }

    public HighScoreElement(string playerName, int score)
    {
        PlayerName = playerName;
        Score = score;
    }
}
