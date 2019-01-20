using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    // Static global instance of the class
    public static GameControl Instance;

    #region public members

    // general game settings & co.
    public float StartSpeed = -0.25f;                   // the speed at the beginning of a game
    public float SpeedIncrement = -0.001f;              // the speed increment per second during the game
    public float SpeedIncrementDeltaTime = 1;           // the delta time between speed increments
    public float CurrentGameSpeed;                      // the current scroll speed of the game
    public bool GamePaused = false;                     // flag if the game is paused 
    public bool GameOver = false;                       // flag if the game is over
    public bool NewHighScore = false;                   // flag to indelicate if a new high score has been reached

    // item stuff
    public bool Troll = false;                  // flag to indicate if the 'Troll' item was hit
    public bool Invulnerability = false;        // flag to indicate if the 'Invulnerability' item was hit
    public bool Turbo = false;                  // flag to indicate if the 'Turbo' item was hit
    public bool DoublePoints = false;           // flag to indicate if the 'DoublePoints' item was hit
    public double EffectDuration = 5;           // the duration of item caused effects 
    public double TimeOfEffectStart = 0;        // the system GetInGameTime of the start of an item effect

    #endregion

    #region private members

    private double _score = 0;                  // the reached score 
    private float _lastSpeedUpdateTime = 0;     // GetInGameTime of last speed update
    private float _timeOfStartOfGame = 0;       // the GetInGameTime when a new game has started
    private float _origSpeed = 0;               // the original in game speed 

    #endregion

    #region ui elements

    public Text ScoreText;                      // the text box where the score is displayed
    public Text ItemText;                       // the text box where the current activated power-up item is displayed
    public Text ScoreAtGameOverText;            // the text box where the score in the game over screen is displayed
    public Text PlayerNameInputText;            // the text box where the player can enter his player name
    public GameObject PausedScreen;             // the instance of the pause screen
    public GameObject GameOverScreen;           // the instance of the game over screen
    public GameObject GamePlayScreen;           // the instance of the game play screen

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
        // start a new Game
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {   
        // speed up the scrolling
        _updateSpeed();

        // check for pause key
        _checkForPausedKeyPressed();

        // update score
        _updateScore();

        // check if effect is over
        _updateEffects();
    }

    #endregion

    #region public function

    // function to get the real GetInGameTime since a game has started
    public float GetInGameTime()
    {
        return Time.time - _timeOfStartOfGame;
    }

    #endregion

    #region private functions

    // function that checks the state of effects and updates them
    private void _updateEffects()
    {
        if ((Troll || Invulnerability || Turbo || DoublePoints) && GetInGameTime() > TimeOfEffectStart + EffectDuration)
        {
            // reset speed
            if (Turbo)
            {
                CurrentGameSpeed = _origSpeed;
            }

            // reset all effects
            Troll = false;
            Invulnerability = false;
            Turbo = false;
            DoublePoints = false;

            // reset item text box
            ItemText.text = "-";
        }
    }

    // function to update the score
    private void _updateScore()
    {
        if (!GamePaused && !GameOver)
        {
            // check if double point effect is active
            if (DoublePoints)
            {
                _score += ((GetInGameTime() * CurrentGameSpeed) / 1000) * 2;
            }
            else
            {
                _score += (GetInGameTime() * CurrentGameSpeed) / 1000;
            }

            ScoreText.text = ((long)_score * (-1)).ToString(CultureInfo.InvariantCulture);
        }
    }

    // function to check if the paused key has been pressed
    private void _checkForPausedKeyPressed()
    {
        if (!GamePaused && !GameOver && Input.GetButton("Cancel"))
        {
            DoGamePause();
        }
    }

    // function to update the speed
    private void _updateSpeed()
    {
        if (GetInGameTime() > _lastSpeedUpdateTime + SpeedIncrementDeltaTime)
        {
            CurrentGameSpeed += SpeedIncrement;
            _lastSpeedUpdateTime = GetInGameTime();
        }
    }

    // function which gets called when a new game gets started
    private void StartGame()
    {
        // register event bindings (delegates)
        CharacterCollisionHandler.Instance.CollisionDetected += ItemHit;

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

        // init the score
        _score += -1.0f;

        // start the GetInGameTime & set GetInGameTime offset
        Time.timeScale = 1.0f;
        _timeOfStartOfGame = Time.time;
    }

    // function which gets called if the game is paused
    private void DoGamePause()
    {
        Time.timeScale = 0.0f;
        GamePaused = true;
        PausedScreen.SetActive(true);
    }

    // function which gets called if the game is over
    private void DoGameOver()
    {
        Time.timeScale = 0.0f;
        GameOver = true;
        GameOverScreen.SetActive(true);
        ScoreAtGameOverText.text = ScoreText.text;
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
        if (Invulnerability && !"Item".Equals(e.CollisionObjectTag))
        {
            // Move the object out of the way 
            if (e.Collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                e.Collider.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(-1000, 0);
            }

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
                _origSpeed = CurrentGameSpeed;
                CurrentGameSpeed *= 2;
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

            // set GetInGameTime of effect start
            TimeOfEffectStart = GetInGameTime();
        }
    }

    #endregion
}
