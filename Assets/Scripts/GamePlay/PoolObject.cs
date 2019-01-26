using System.Collections;
using UnityEngine;

public class PoolObject : Object
{
    #region public member

    public GameObject GameObjectType;       // variable to store the prefab type of the objects
    public GameObject GameObject;           // var for the corresponding game object
    public Rigidbody2D GameObjectBody;      // var for the rigid boy of the game object
    public float YValueAtSpawn;             // the value of the y - coordinate at spawn
    public bool IsOnScreen;                 // flag to indicate if an object is currently in use

    #endregion

    #region Constructor

    public PoolObject(GameObject gameObjectType, Vector2 spawnPos)
    {
        GameObjectType = gameObjectType;
        IsOnScreen = false;
        GameObject = (Instantiate(GameObjectType, spawnPos, Quaternion.identity));
        GameObjectBody = GameObject.GetComponent<Rigidbody2D>();
        GameObjectBody.velocity = Vector2.zero;
        YValueAtSpawn = spawnPos.y;
    }

    #endregion
}
