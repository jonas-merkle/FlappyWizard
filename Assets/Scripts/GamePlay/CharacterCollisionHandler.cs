using System;
using UnityEngine;

public class CharacterCollisionHandler : MonoBehaviour
{
    // Static global instance of the class
    public static CharacterCollisionHandler Instance;

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

    #endregion

    #region collision trigger function

    // called when a collision was detected by the polygon collider 2d of the character
    void OnTriggerEnter2D(Collider2D col)
    {
        CollisionEventArgs args = new CollisionEventArgs();
        args.Collider = col;
        args.TimeOfCollision = Time.time;
        args.CollisionObjectTag = col.gameObject.tag;
        OnCollisionDetected(args);
    }

    #endregion

    #region event managment

    protected virtual void OnCollisionDetected(CollisionEventArgs e)
    {
        CollisionDetected?.Invoke(this, e);
    }

    public event EventHandler<CollisionEventArgs> CollisionDetected;

    #endregion
}

#region event args definition

public class CollisionEventArgs : EventArgs
{
    // the object that caused the collision
    public Collider2D Collider { get; set; }
    // the GetInGameTime when the collision occured
    public float TimeOfCollision { get; set; }
    // the tag which the object that caused the collision has
    public string CollisionObjectTag { get; set; }
}

#endregion
