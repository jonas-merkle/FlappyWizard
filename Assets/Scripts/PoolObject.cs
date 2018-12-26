using UnityEngine;

public class PoolObject : Object
{
    #region public member

    public GameObject GameObjectType;
    public GameObject GameObject;
    public Rigidbody2D GameObjectBody;
    public bool IsOnScreen;

    #endregion

    #region Constructor

    public PoolObject(GameObject gameObjectType, Vector2 spawnPos)
    {
        GameObjectType = gameObjectType;
        IsOnScreen = false;
        GameObject = (Instantiate(GameObjectType, spawnPos, Quaternion.identity));
        GameObjectBody = GameObject.GetComponent<Rigidbody2D>();
        GameObjectBody.velocity = Vector2.zero;
    }

    #endregion
}
