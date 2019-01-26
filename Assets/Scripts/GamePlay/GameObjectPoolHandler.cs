using UnityEngine;

public class GameObjectPoolHandler : MonoBehaviour
{
    #region public members

    public GameObject GameObjectPrefab;
    public int PoolSize = 10;
    public Vector2 PoolPos = new Vector2(20, 0);
    public float MaxY = 5.0f;
    public float MinY = -5.0f;
    public float OnTurboY = -2.0f;
    public float OnTrollYDecrement = 0;
    public float EndOfScreenX = -10.0f;

    #endregion

    #region private members 

    private Pool _objectPool;
    private int _nextFormPool;

    #endregion

    #region basic unity functions

    // Awake is called before Start()
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        _objectPool = new Pool(GameObjectPrefab, PoolSize, PoolPos);
        _nextFormPool = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if game is not running, do nothing
        if (GameControl.Instance.GameOver && GameControl.Instance.GamePaused)
        {
            _pauseGame();
            return;
        }

        // update the speed of the objects
        _updateObjectSpeeds();
    }

    #endregion

    #region private funktions

    private void _updateObjectSpeeds()
    {
        // get the current speed form game controller
        float currentVelocity = GameControl.Instance.CurrentGameSpeed;

        // scrolling on screen objects
        foreach (var obj in _objectPool.PoolObjects)
        {
            // check if an object is on screen and has to been moved
            if (obj.IsOnScreen)
            {
                // check if an object is on scree
                if (obj.GameObject.transform.position.x < EndOfScreenX)
                {
                    obj.GameObjectBody.velocity = Vector2.zero;
                    obj.GameObject.transform.position = _objectPool.StandardPoolPos;
                    obj.IsOnScreen = false;
                    continue;
                }

                // set new object speed
                obj.GameObjectBody.velocity = new Vector2(currentVelocity, 0);

                // check if an effect is active and react 
                if (GameControl.Instance.Turbo)
                {
                    if (!"Item".Equals(obj.GameObject.tag))
                    {
                        obj.GameObject.transform.position = new Vector2(obj.GameObject.transform.position.x, OnTurboY);
                    }
                }
                else if (GameControl.Instance.Troll)
                {
                    obj.GameObject.transform.position = new Vector2(obj.GameObject.transform.position.x, obj.YValueAtSpawn - OnTrollYDecrement);
                }
                else
                {
                    obj.GameObject.transform.position = new Vector2(obj.GameObject.transform.position.x, obj.YValueAtSpawn);
                }
            }
        }
    }

    private void _pauseGame()
    {
        // set scrolling speed on all active objects to 0
        foreach (var obj in _objectPool.PoolObjects)
        {
            // check if an object is on screen and has to been moved
            if (obj.IsOnScreen)
            {
                // set new object speed
                obj.GameObjectBody.velocity = Vector2.zero;
            }
        }
    }

    #endregion

    #region public functions

    public GameObject SpawnNextObject(Vector2 spawnPos)
    {
        GameObject currentGameObject = _objectPool.PoolObjects[_nextFormPool].GameObject;

        // spawn object
        currentGameObject.transform.position = spawnPos;

        // set on screen flag
        _objectPool.PoolObjects[_nextFormPool].IsOnScreen = true;
        _objectPool.PoolObjects[_nextFormPool].YValueAtSpawn = spawnPos.y;

        // select the next object from pool
        _nextFormPool++;
        if (_nextFormPool >= PoolSize)
            _nextFormPool = 0;

        return currentGameObject;
    }

    #endregion
}