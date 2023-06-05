using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField] float movement_speed = 10;
    [SerializeField] float acceleration = 0.6f;
    [SerializeField] float max_speed = 100;
    [SerializeField] float min_speed = 10;
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
                speed += acceleration/2;
            }
        }
        else {
            if (speed > min_speed) {
                speed -= acceleration/2;
            }
        }
    }

    void FixedUpdate() {
        rigid_body.velocity = movement_direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        string level_char;
        Scene scene = SceneManager.GetActiveScene();
        string scene_name = scene.name;
        level_char = scene_name[scene_name.Length-1].ToString();
        int level = int.Parse(level_char);
        
        if (collider.gameObject.tag == "Finish") {
            if (level < 9) {
                level_char = (level+1).ToString();
                string new_level_name = "Level" + level_char;
                SceneManager.LoadScene(new_level_name);
            }
            if (level == 9) {
                SceneManager.LoadScene("Win");
            }

        }
    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.tag == "Obstacle") {
            SceneManager.LoadScene("Level1");
        }
    }
}
