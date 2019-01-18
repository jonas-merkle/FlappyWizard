using System;
using UnityEngine;

public class MovedDistanceTrigger : MonoBehaviour
{
    #region public members

    public static MovedDistanceTrigger Instance;
    
    public Vector2 StartPos = new Vector2(1, 0);

    #endregion

    #region private members

    private Rigidbody2D _body;

    #endregion

    #region basic unity functions

    void Awake()
    {
        // set the reference to the current instance
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        _body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _body.position = StartPos;
    }

    // Update is called once per frame
    void Update()
    {
        // update Speed
        if (!GameControl.Instance.GamePaused && !GameControl.Instance.GameOver)
        {
            _body.velocity = new Vector2(GameControl.Instance.CurrentGameSpeed, 0);
        }
        else
        {
            _body.velocity = Vector2.zero;
        }
    }

    #endregion

    #region collision trigger function

    // called when a collision was detected by the polygon collider 2d of the character
    void OnTriggerEnter2D(Collider2D col)
    {
        // trigger movement event

        MovedDistanceTriggerEventArgs args = new MovedDistanceTriggerEventArgs();
        args.MovedDistance = StartPos.x - _body.position.x;
        OnUnitMovementDetected(args);

        // reset position
        _body.position = StartPos;
    }

    #endregion

    #region event managment

    protected virtual void OnUnitMovementDetected(MovedDistanceTriggerEventArgs e)
    {
        UnitMovementDetected?.Invoke(this, e);
    }

    public event EventHandler<MovedDistanceTriggerEventArgs> UnitMovementDetected;

    #endregion
}

#region event args definition

public class MovedDistanceTriggerEventArgs : EventArgs
{
    public float MovedDistance { get; set; }
}

#endregion

