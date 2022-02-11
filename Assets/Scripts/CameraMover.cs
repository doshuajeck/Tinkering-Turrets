using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0));
        _rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed));
    }
}
