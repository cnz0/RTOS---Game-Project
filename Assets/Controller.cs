using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float movement_speed = 10;
    [SerializeField] float acceleration = 0.6f;
    private Rigidbody2D rigid_body;
    private Vector2 movement_direction;
    private float speed;

    void Start()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        speed = movement_speed;
    }

    // Update is called once per frame
    void Update()
    {
        movement_direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        speed += movement_speed + acceleration * Time.deltaTime/1000;
        Debug.Log(speed);
    }

    void FixedUpdate() {
        rigid_body.velocity = movement_direction * speed;
    }
}
