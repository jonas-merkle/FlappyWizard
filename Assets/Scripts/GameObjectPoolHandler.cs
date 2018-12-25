using UnityEngine;

public class GameObjectPoolHandler : MonoBehaviour
{
    #region public members

    public GameObject GameObjectPrefab;
    public double SpanRate = 1;
    public double SpanProbability = 0.5;
    public static int PoolSize = 10;

    #endregion

    #region private members 

    private GameObjectPool _objectPool;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _objectPool = new GameObjectPool(GameObjectPrefab, PoolSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
