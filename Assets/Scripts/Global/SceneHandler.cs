using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    #region public members

    public static SceneHandler Instance;

    #endregion

    #region scenes

    public static Scene MainMenuScene { get; set; }
    public static Scene GamePlayScene { get; set; }
    public static Scene HighScoreScene { get; set; }
    public static Scene HelpScene { get; set; }

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

    public void StartGame(GameObject character)
    {
        // load character prefab
        CharacterControl.Instance.CharacterTypPrefab = character;

        // load game scene & set it active
        SceneManager.LoadScene(GamePlayScene.name);
        GamePlayScene = SceneManager.GetSceneByName(GamePlayScene.name);
        SceneManager.SetActiveScene(GamePlayScene);
    }

    public void ShowHighScore()
    {
        // load high score scene & set it active
        SceneManager.LoadScene(HighScoreScene.name);
        HighScoreScene = SceneManager.GetSceneByName(HighScoreScene.name);
        SceneManager.SetActiveScene(HighScoreScene);
    }

    public void ShowHelp()
    {
        // load help scene & set it active
        SceneManager.LoadScene(HelpScene.name);
        HelpScene = SceneManager.GetSceneByName(HelpScene.name);
        SceneManager.SetActiveScene(HelpScene);
    }

    public void ShowMainMenu(string nameOfCurrentScene)
    {
        SceneManager.UnloadSceneAsync(nameOfCurrentScene);
        SceneManager.SetActiveScene(MainMenuScene);
    }

    public void CloseApplication()
    {
        SceneManager.UnloadSceneAsync(HelpScene.name);
        SceneManager.UnloadSceneAsync(HighScoreScene.name);
        SceneManager.UnloadSceneAsync(GamePlayScene.name);

        Application.Quit(0);
    }

    #endregion
}
