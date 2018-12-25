using UnityEngine;

public class SkyScroller : MonoBehaviour
{
    #region public members

    public float VelocityMultiplier = 1;
    public Vector2 DefaultPos = new Vector2(19.2f, 0);

    #endregion

    
    #region private members

    private Rigidbody2D _body;

    #endregion

    #region basic unity functions

    // Awake is called before Start()
    void Awake()
    {
        // get the rigid body object of the sky object
        _body = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!GameController.Instance.GamePaused && !GameController.Instance.GameOver)
        if (true)
        {

            // scroll the sky 
            _body.velocity = new Vector2(GameController.Instance.CurrentGameSpeed * VelocityMultiplier, 0);

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
