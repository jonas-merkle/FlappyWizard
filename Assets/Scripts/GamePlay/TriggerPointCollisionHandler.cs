﻿using System;
using UnityEngine;

public class TriggerPointCollisionHandler : MonoBehaviour
{
    // Static global instance of the class
    public static TriggerPointCollisionHandler Instance;

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

    #region collision trigger function

    // called when a collision was detected by the polygon collider 2d of the character
    void OnTriggerEnter2D(Collider2D col)
    {
        TriggerPointEventArgs args = new TriggerPointEventArgs();
        args.Collider = col;
        args.TimeOfCollision = Time.time;
        OnCollisionDetected(args);
    }

    #endregion

    #region event managment

    protected virtual void OnCollisionDetected(TriggerPointEventArgs e)
    {
        TriggerPointCollisionDetected?.Invoke(this, e);
    }

    public event EventHandler<TriggerPointEventArgs> TriggerPointCollisionDetected;

    #endregion
}

#region event args definition

public class TriggerPointEventArgs : EventArgs
{
    // the object that caused the collision
    public Collider2D Collider { get; set; }
    // the GetInGameTime when the collision occured
    public float TimeOfCollision { get; set; }
}

#endregion
