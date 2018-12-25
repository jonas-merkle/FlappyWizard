using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    #region public members

    public float CurrentGameSpeed = -0.5f;      // the current scroll speed of the game
    public bool GamePaused = false;             // flag if the game is paused 
    public bool GameOver= false;                // flag if the game is over

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

    #endregion

    #region ui elements

    public Text ScoreText;
    public Text ItemText;

    #endregion

    // Awake is called before Start()
    void Awake()
    {
        // set the reference to the current instance
        Instance = this;

        // setup ui
        ScoreText.text = "0";
        ItemText.text = "-";

        GameOver = false;
        GamePaused = false;
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
