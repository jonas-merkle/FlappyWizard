using UnityEngine;

public class SkyScroller : MonoBehaviour
{
    #region public members

    public float VelocityMultiplier = 1;                    // the multiplier for the game veocity
    public Vector2 DefaultPos = new Vector2(19.2f, 0);      // default position of the object

    #endregion

    
    #region private members

    private Rigidbody2D _body;          // the rigid body of the object

    #endregion

    #region basic unity functions

    // Awake is called before Start()
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // get the rigid body object of the sky object
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.Instance.GamePaused && !GameControl.Instance.GameOver)
        {
            // scroll the sky 
            _body.velocity = new Vector2(GameControl.Instance.CurrentGameSpeed * VelocityMultiplier, 0);

            // scroll back to start if object left the view file
            if (transform.position.x < -DefaultPos.x)
            {
                transform.position = DefaultPos;
            }
        }
        else
        {
            // stop sky scrolling
            _body.velocity = Vector2.zero;
            return;
        }
    }

    #endregion
}
