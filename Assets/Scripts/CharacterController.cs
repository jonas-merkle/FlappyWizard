using UnityEngine;

public class CharacterController : MonoBehaviour
{   
    #region public members

    public Vector2 CharacterPos = new Vector2(-100, 0);         // Position where the character starts at the beginning of a game
    public GameObject CharacterTypPrefab;                       // The Prefab of the selected character
    public float LiftForce = 150;                               // The Force which lifts the character up on interaction    

    #endregion

    #region private members

    private Rigidbody2D _body;          // the rigid body of the character object 
    private GameObject _character;      // the character object 

    #endregion

    #region basic unitiy function

    // Start is called before the first frame update
    void Start()
    {
        // Load the Prefab for the selected character and set it to the default position
        _character = Instantiate(CharacterTypPrefab, CharacterPos, Quaternion.identity);

        // get the rigid body object of the character
        _body = _character.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Accelerate the character upwards when space bar is pressed 
        if (!GameController.Instance.GamePaused  && !GameController.Instance.GameOver && Input.GetButton("Jump"))
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
