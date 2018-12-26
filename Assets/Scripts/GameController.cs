using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    #region public members

    public float StartSpeed = -0.25f;                // the speed at the beginning of a game
    public float SpeedIncrementPerSecond = -0.001f;   // the speed increment per second during the game;
    public float CurrentGameSpeed;                  // the current scroll speed of the game
    public bool GamePaused = false;                 // flag if the game is paused 
    public bool GameOver= false;                    // flag if the game is over

    // item stuff
    public bool Troll = false;                  // flag to indicate if the 'Troll' item was hit
    public bool Invulnerability = false;        // flag to indicate if the 'Invulnerability' item was hit
    public bool Turbo = false;                  // flag to indicate if the 'Turbo' item was hit
    public bool DoublePoints = false;           // flag to indicate if the 'DoublePoints' item was hit
    public float EffectDuration = 5;            // the duration of item caused effects 
    public float TimeOfEffectStart;             // the system time of the start of an item effect

    #endregion

    #region private members

    private double _score = 0;                  // the reached score 
    private float _lastSpeedUpdateTime = 0;     // time of last speed update

    #endregion

    #region ui elements

    public Text ScoreText;
    public Text ItemText;
    public GameObject PausedScreen;
    public GameObject GameOverScreen;

    #endregion

    // Awake is called before Start()
    void Awake()
    {
        // set the reference to the current instance
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        // setup ui
        ScoreText.text = ((long) _score * (-1)).ToString(CultureInfo.InvariantCulture);
        ItemText.text = "-";

        // setting some game variables
        CurrentGameSpeed = StartSpeed;
        GameOver = false;
        GamePaused = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        // init the score
        _score += -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // speed up the scrolling
        if (Time.time > _lastSpeedUpdateTime + 1)
        {
            CurrentGameSpeed += SpeedIncrementPerSecond;
            _lastSpeedUpdateTime = Time.time;
        }
        
        // check for pause key
        if (!GamePaused && !GameOver && Input.GetButton("Cancel"))
        {
            Time.timeScale = 0.0f;
            GamePaused = true;
            PausedScreen.SetActive(true);
        }

        // update score
        if (!GamePaused && !GameOver)
        {
            _score += (Time.time * CurrentGameSpeed) / 1000;

            ScoreText.text = ((long)_score * (-1)).ToString(CultureInfo.InvariantCulture);
        }
    }
}
