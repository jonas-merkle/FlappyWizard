using System.Collections.Generic;
using UnityEngine;

class Pool : ScriptableObject
{   
    #region public member

    public GameObject GameObjectType;
    public int PoolSize;
    public PoolObject[] PoolObjects;
    public Vector2 StandardPoolPos;

    #endregion

    #region Constructor

    public Pool(GameObject gameObjectType, int poolSize, Vector2 standardPoolPos)
    {
        GameObjectType = gameObjectType;
        PoolSize = poolSize;
        StandardPoolPos = standardPoolPos;

        PoolObjects = new PoolObject[PoolSize];
        for (int i = 0; i < PoolSize; i++)
        {
            PoolObjects[i] = new PoolObject(GameObjectType, StandardPoolPos);
        }
    }

    #endregion

    #region public functions

    #endregion
}