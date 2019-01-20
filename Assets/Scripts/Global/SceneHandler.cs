using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    // Static global instance of the class
    public static SceneHandler Instance;

    #region public members

    // selected character
    public string SelectedCharacter = "harry";

    #endregion

    #region scenes

    public int MainMenuSceneNo = 0;
    public int GamePlaySceneNo = 1;
    public int HighScoreSceneNo = 2;
    public int HelpSceneNo = 3;

    #endregion

    #region private members



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

    #region public scene mamagement functions

    // start a new game with a given character
    public void StartGame(string character)
    {
        SelectedCharacter = character;
        
        // load game scene
        SceneManager.LoadScene(GamePlaySceneNo);
    }

    // function to load the high score view
    public void ShowHighScore()
    {
        // load high score scene
        SceneManager.LoadScene(HighScoreSceneNo);
    }

    // function to load the help menu
    public void ShowHelp()
    {
        // load help scene & set it active
        SceneManager.LoadScene(HelpSceneNo);
    }

    // function to load the main menu
    public void ShowMainMenu()
    {
        // load main menu scene & set it active
        SceneManager.LoadScene(MainMenuSceneNo);
    }

    // function to close the Application 
    public void CloseApplication()
    {
        Application.Quit(0);
    }

    #endregion
}
