using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    #region public members

    public double CurrentGameSpeed = -0.5;
    public bool GameIsPaused = false;
    public bool PlayerDied = false;

    #endregion

    #region private members

    private double _score = 0;

    #endregion

    #region ui elements

    public Text ScoreText;
    public Text ItemText;

    #endregion

    #region game objects

    public GameObject Character;

    #endregion

    // Awake is called before Start()
    void Awake()
    {
        // setup ui
        ScoreText.text = "0";
        ItemText.text = "-";

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
