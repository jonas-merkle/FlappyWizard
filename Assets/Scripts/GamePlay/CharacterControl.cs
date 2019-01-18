using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public static CharacterControl Instance;

    #region public members

    public Vector2 CharacterPos = new Vector2(-100, 0);     // Position where the character starts at the beginning of a game
    public GameObject CedricPrefab;                             // the prefab for cedric
    public GameObject HarryPrefab;                              // the prefab for harry
    public GameObject LunaPrefab;                               // the prefab for luna
    public GameObject MelfoyPrefab;                             // the prefab for malfoy
    public GameObject CharacterTypPrefab;                       // The Prefab of the selected character
    public float LiftForce = 150;                               // The Force which lifts the character up on interaction    

    #endregion

    #region private members

    private Rigidbody2D _body;          // the rigid body of the character object 
    private GameObject _character;      // the character object 

    #endregion

    #region basic unitiy function

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
        // select character
        if (SceneHandler.Instance.SelectedCharacter != null)
        {
            if (SceneHandler.Instance.SelectedCharacter.Equals("cedric"))
            {
                CharacterTypPrefab = CedricPrefab;
            }
            else if (SceneHandler.Instance.SelectedCharacter.Equals("harry"))
            {
                CharacterTypPrefab = HarryPrefab;
            }
            else if (SceneHandler.Instance.SelectedCharacter.Equals("luna"))
            {
                CharacterTypPrefab = LunaPrefab;
            }
            else if (SceneHandler.Instance.SelectedCharacter.Equals("malfoy"))
            {
                CharacterTypPrefab = MelfoyPrefab;
            }
        }
        
        // Load the Prefab for the selected character and set it to the default position
        _character = Instantiate(CharacterTypPrefab, CharacterPos, Quaternion.identity);

        // get the rigid body object of the character
        _body = _character.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Accelerate the character upwards when space bar is pressed 
        if (!GameControl.Instance.GamePaused  && !GameControl.Instance.GameOver && Input.GetButton("Jump"))
        {
            Vector2 currentPos = _body.position;
            currentPos.x = CharacterPos.x;
            _body.position = currentPos;
            _body.velocity = Vector2.zero;
            _body.AddForce(new Vector2(0, LiftForce));
        }
    }

    #endregion
}
