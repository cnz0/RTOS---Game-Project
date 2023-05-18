using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float movement_speed = 10;
    [SerializeField] float acceleration = 0.6f;
    [SerializeField] float max_speed = 50;
    [SerializeField] float min_speed = 5;
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
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)  {
            if (speed < max_speed) {
                speed += acceleration/10;
            }
        }
        else {
            if (speed > min_speed) {
                speed -= acceleration/10;
            }
        }
    }

    void FixedUpdate() {
        rigid_body.velocity = movement_direction * speed;
    }
}
