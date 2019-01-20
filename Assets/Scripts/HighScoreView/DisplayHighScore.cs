using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScore : MonoBehaviour
{
    public Text HighScoreTextbox;

    // Start is called before the first frame update
    void Start()
    {
        HighScoreHandler.Instance.ReadHighScoreList();
        HighScoreTextbox.text = HighScoreHandler.Instance.GetBestPlayers();
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
