using UnityEngine;

class GameObjectPool : MonoBehaviour
{
    #region public member

    public GameObject GameObjectType { get; set; }
    public int PoolSize { get; set; }
    public GameObject[] GameObjects { get; set; }
    public bool[] IsOnScreen { get; set; }
    public bool[] IsNew { get; set; }
    public bool[] IsOld { get; set; }
    public float TimeOfLastSpawn { get; set; }

    #endregion

    #region Constructor

    public GameObjectPool(GameObject obGameObjectType, int poolSize)
    {
        GameObjectType = gameObject;
        PoolSize = poolSize;

        // init the arrays
        GameObjects = new GameObject[PoolSize];
        IsNew = new bool[PoolSize];
        IsOld = new bool[PoolSize];
        IsOnScreen = new bool[PoolSize];
    }

    #endregion
}