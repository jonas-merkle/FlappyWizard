using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;

    #region public members

    public float StartSpeed = -0.25f;                   // the speed at the beginning of a game
    public float SpeedIncrementPerSecond = -0.001f;     // the speed increment per second during the game;
    public float CurrentGameSpeed;                      // the current scroll speed of the game
    public bool GamePaused = false;                     // flag if the game is paused 
    public bool GameOver= false;                        // flag if the game is over

    // item stuff
    public bool Troll = false;                  // flag to indicate if the 'Troll' item was hit
    public bool Invulnerability = false;        // flag to indicate if the 'Invulnerability' item was hit
    public bool Turbo = false;                  // flag to indicate if the 'Turbo' item was hit
    public bool DoublePoints = false;           // flag to indicate if the 'DoublePoints' item was hit
    public float EffectDuration = 5;            // the duration of item caused effects 
    public float TimeOfEffectStart;             // the system time of the start of an item effect

    public bool NewHighScore = false;           // flag to indelicate if a new high score has been reached

    #endregion

    #region private members

    private double _score = 0;                  // the reached score 
    private float _lastSpeedUpdateTime = 0;     // time of last speed update
    private float _timeOfStartOfGame = 0;       // the time when a new game has started

    #endregion

    #region ui elements

    public Text ScoreText;                      // the text box where the score is displayed
    public Text ItemText;                       // the text box where the current activated power-up item is displayed
    public GameObject PausedScreen;             // the instance of the pause screen
    public GameObject GameOverScreen;           // the instance of the game over screen

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
        // register event bindings (delegates)
        CharacterCollisionHandler.Instance.CollisionDetected += ItemHit;

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

    #endregion

    #region public function

    // function to get the real time since a game has started
    public float time()
    {
        return Time.time - _timeOfStartOfGame;
    }

    #endregion

    #region envent handler

    // event that gets called when the player collides with another object
    private static void ItemHit(object sender, CollisionEventArgs e)
    {
        // null check
        if (e.Collider == null)
            return;
        
        // collision with death zone 
        if (e.CollisionObjectTag == "DeathZone")
        {
            Time.timeScale = 0.0f;
            GameControl.Instance.GameOver = true;
            GameControl.Instance.GameOverScreen.SetActive(true);

            // calculate high score
            int reachedScore = Convert.ToInt32(GameControl.Instance.ScoreText.text);
            int currentHighScore = PlayerPrefs.GetInt("HighScore");
            if (reachedScore > currentHighScore)
            {
                PlayerPrefs.SetInt("HighScore", currentHighScore);
                GameControl.Instance.NewHighScore = true;
            }

        }
        // collision with item
        else if (e.CollisionObjectTag == "Item")
        {
            string itemType = e.Collider.name.Replace("(Clone)", "");

            // read item type
            if ("item_red".Equals(itemType))
                GameControl.Instance.Troll = true;
            else if ("item_green".Equals(itemType))
                GameControl.Instance.Invulnerability = true;
            else if ("item_blue".Equals(itemType))
                GameControl.Instance.Turbo = true;
            else if ("item_silver".Equals(itemType))
                GameControl.Instance.DoublePoints = true;
            else
                return;
            
            // Move the object out of the way 
            e.Collider.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(-1000, 0);
            GameControl.Instance.TimeOfEffectStart = Time.time;
        }
    }

    #endregion
}
