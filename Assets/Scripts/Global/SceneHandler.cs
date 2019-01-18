using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    #region public members

    // SceneHandler instance 
    public static SceneHandler Instance;

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

    public void StartGame(string character)
    {
        SelectedCharacter = character;
        
        // load game scene
        SceneManager.LoadScene(GamePlaySceneNo);
    }

    public void ShowHighScore()
    {
        // load high score scene
        SceneManager.LoadScene(HighScoreSceneNo);
    }

    public void ShowHelp()
    {
        // load help scene & set it active
        SceneManager.LoadScene(HelpSceneNo);
    }

    public void ShowMainMenu(int indexOfCurrentScene)
    {
        SceneManager.LoadScene(MainMenuSceneNo);
    }

    public void CloseApplication()
    {
        Application.Quit(0);
    }

    #endregion
}
