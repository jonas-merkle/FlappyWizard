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
    public bool GameOver = false;                       // flag if the game is over

    // item stuff
    public bool Troll = false;                  // flag to indicate if the 'Troll' item was hit
    public bool Invulnerability = false;        // flag to indicate if the 'Invulnerability' item was hit
    public bool Turbo = false;                  // flag to indicate if the 'Turbo' item was hit
    public bool DoublePoints = false;           // flag to indicate if the 'DoublePoints' item was hit
    public double EffectDuration = 5;           // the duration of item caused effects 
    public double TimeOfEffectStart;            // the system time of the start of an item effect

    // high score 
    public bool NewHighScore = false;           // flag to indelicate if a new high score has been reached

    // system status
    public bool GameIsStarted = false;

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
    public GameObject GamePlayScreen;

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
        // start game
        if (!GameIsStarted)
        {
            StartGame();
        }
        
        // speed up the scrolling
        if (time() > _lastSpeedUpdateTime + 1)
        {
            CurrentGameSpeed += SpeedIncrementPerSecond;
            _lastSpeedUpdateTime = time();
        }

        // check for pause key
        if (!GamePaused && !GameOver && Input.GetButton("Cancel"))
        {
            DoGamePause();
        }

        // update score
        if (!GamePaused && !GameOver)
        {
            // check if double point effect is active
            if (DoublePoints)
            {
                _score += ((time() * CurrentGameSpeed) / 1000) * 2;
            }
            else
            {
                _score += (time() * CurrentGameSpeed) / 1000;
            }

            ScoreText.text = ((long)_score * (-1)).ToString(CultureInfo.InvariantCulture);
        }

        // check if effect is over
        if ((Troll || Invulnerability || Turbo || DoublePoints) && time() > TimeOfEffectStart + EffectDuration)
        {
            // reset all effects
            Troll = false;
            Invulnerability = false;
            Turbo = false;
            DoublePoints = false;

            // reset item text box
            ItemText.text = "-";
        }
    }

    #endregion

    #region public function

    // function to get the real time since a game has started
    public float time()
    {
        return Time.time - _timeOfStartOfGame;
    }

    // start a new Game
    public void StartGame()
    {
        // register event bindings (delegates)
        CharacterCollisionHandler.Instance.CollisionDetected += ItemHit;

        // set time offset
        _timeOfStartOfGame = Time.time;

        // setup ui
        PausedScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        GamePlayScreen.SetActive(true);
        
        ScoreText.text = ((long)_score * (-1)).ToString(CultureInfo.InvariantCulture);
        ItemText.text = "-";

        // setting some game variables
        CurrentGameSpeed = StartSpeed;
        GameOver = false;
        GamePaused = false;
        Troll = false; 
        Invulnerability = false;
        Turbo = false;
        DoublePoints = false;
        NewHighScore = false;

        // rest
        GameIsStarted = true;

        // init the score
        _score += -1.0f;
    }

    #endregion

    #region private functions

    private void DoGamePause()
    {
        Time.timeScale = 0.0f;
        GamePaused = true;
        PausedScreen.SetActive(true);
    }

    private void DoGameOver()
    {
        Time.timeScale = 0.0f;
        GameOver = true;
        GameOverScreen.SetActive(true);

        // calculate high score
        int reachedScore = Convert.ToInt32(ScoreText.text);
        int currentHighScore = PlayerPrefs.GetInt("HighScore");
        if (reachedScore > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentHighScore);
            NewHighScore = true;
        }
    }

    #endregion

    #region envent handler

    // event that gets called when the player collides with another object
    private void ItemHit(object sender, CollisionEventArgs e)
    {
        // null check
        if (e.Collider == null)
            return;

        // check if  player has invulnerability
        if (Invulnerability)
        {
            // Move the object out of the way 
            if (e.Collider.gameObject.GetComponent<Rigidbody2D>() != null)
                e.Collider.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(-1000, 0);

            return;
        }

        // collision with death zone 
        if (!Invulnerability && "DeathZone".Equals(e.CollisionObjectTag))
        {
            DoGameOver();
        }

        // collision with item
        else if ("Item".Equals(e.CollisionObjectTag))
        {
            string itemType = e.Collider.name.Replace("(Clone)", "");

            // reset all effects
            Troll = false;
            Invulnerability = false;
            Turbo = false;
            DoublePoints = false;

            // read item type
            if ("item_red".Equals(itemType))
            {
                Troll = true;
                ItemText.text = "Troll";
            }
            else if ("item_green".Equals(itemType))
            {
                Invulnerability = true;
                ItemText.text = "Invulnerability";
            }
            else if ("item_blue".Equals(itemType))
            {
                Turbo = true;
                ItemText.text = "Turbo";
            }
            else if ("item_silver".Equals(itemType))
            {
                DoublePoints = true;
                ItemText.text = "Double Points";
            }
            else
            {
                // Move the object out of the way 
                e.Collider.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(-1000, 0);

                return;
            }

            // Move the object out of the way 
            e.Collider.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(-1000, 0);

            // set time of effect start
            TimeOfEffectStart = time();
        }
    }

    #endregion
}
