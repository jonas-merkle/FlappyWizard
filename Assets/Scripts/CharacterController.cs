using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region public members

    public Vector2 CharacterPos = new Vector2(-100, 0);
    public GameObject CharacterTypPrefab;
    public float LiftForce = 2;

    #endregion

    #region private members

    private Rigidbody2D _body;
    private GameObject _character;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _character = Instantiate(CharacterTypPrefab, CharacterPos, Quaternion.identity);
        _body = _character.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            _body.velocity = Vector2.zero;
            _body.AddForce(new Vector2(0, LiftForce));
        }
    }
}
