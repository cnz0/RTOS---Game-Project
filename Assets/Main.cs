using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    [SerializeField] GameObject dirt;
    [SerializeField] GameObject stone;
    [SerializeField] GameObject finish;
    [SerializeField] GameObject start;
    [SerializeField] GameObject obstacle;
    [SerializeField] int level_length;
    [SerializeField] int minimal_height;
    [SerializeField] int maximal_height;

    void Start()
    {
        int i,j;
        for (i = 0 ; i < 2 ; i++) {
            for (j = 0 ; j < 22 ; j++) {
                GameObject cell = Instantiate(stone, new Vector3(-25+j*50, -25+i*50 ,0), Quaternion.identity);
            }
        }
        GenerateTerrain();
    }

    public List<int> RandomNumberGenerator(int minNumber, int maxNumber, int length) 
    {
        System.Random random = new System.Random();
        int i = 0;
        int value, dynamic_minimum, dynamic_maximum;
        List<int> list_of_values = new List<int>();

        dynamic_minimum = minNumber;
        dynamic_maximum = maxNumber;

        for (i = 0 ; i < length+100 ; i++) {
            value = random.Next(dynamic_minimum, dynamic_maximum);
            dynamic_minimum = value-4;
            if (dynamic_minimum < minNumber) {
                dynamic_minimum = minNumber;
            }
            dynamic_maximum = value+4;
            if (dynamic_maximum > maxNumber) {
                dynamic_maximum = maxNumber;
            }
            list_of_values.Add(value+20);
        }
        return list_of_values; 
    }

    public void GenerateTerrain() {

        List<int> lower_terrain_height = new List<int>();
        List<int> upper_terrain_height = new List<int>();
        SpriteRenderer spriteRenderer;
        BoxCollider2D boxCollider;
        Vector3 scale;
        GameObject terrain;
        int i;

        lower_terrain_height = RandomNumberGenerator(minimal_height,maximal_height,level_length);
        upper_terrain_height = RandomNumberGenerator(minimal_height,maximal_height,level_length);

        terrain = Instantiate(start, new Vector3(2, -30 ,0), Quaternion.identity);
        spriteRenderer = terrain.GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(2, 60);
        boxCollider = terrain.GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector3(2, 119);

        terrain = Instantiate(finish, new Vector3(level_length*2-2, -30 ,0), Quaternion.identity);
        spriteRenderer = terrain.GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(2, 60);
        boxCollider = terrain.GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector3(2, 119);

        for (i = 1 ; i < level_length+50 ; i++) {
            terrain = Instantiate(dirt, new Vector3(i*2-50, -50 ,0), Quaternion.identity);
            spriteRenderer = terrain.GetComponent<SpriteRenderer>();
            spriteRenderer.size = new Vector2(2, lower_terrain_height[i]);
            boxCollider = terrain.GetComponent<BoxCollider2D>();
            boxCollider.size = new Vector3(2, lower_terrain_height[i]*2-1);
        }

        for (i = 1 ; i < level_length+50 ; i++) {
            terrain = Instantiate(dirt, new Vector3(i*2-50, 50 ,0), Quaternion.identity);
            spriteRenderer = terrain.GetComponent<SpriteRenderer>();
            spriteRenderer.size = new Vector2(2, upper_terrain_height[i]);
            boxCollider = terrain.GetComponent<BoxCollider2D>();
            boxCollider.size = new Vector3(2, upper_terrain_height[i]*2-1);
            scale = terrain.transform.localScale;
            scale.y *= -1;
            terrain.transform.localScale = scale;
        }

        GenerateObstacles(lower_terrain_height, upper_terrain_height);
    }

    public void GenerateObstacles (List<int> lower_terrain_height, List<int> upper_terrain_height) {
        string level_char;
        Scene scene = SceneManager.GetActiveScene();
        string scene_name = scene.name;
        level_char = scene_name[scene_name.Length-1].ToString();
        int level_int;
        level_int = Convert.ToInt32(level_char);

        SpriteRenderer spriteRenderer;
        BoxCollider2D boxCollider;
        int i, pos_y, obstacle_shape_rand;
        double random;
        double addition = 0;
        for (i = 20 ; i < level_length ; i++) {
            random = Convert.ToDouble(GetRng(0,0.5));
            addition += 0.05*level_int/2;
            if (random + addition > 0.7) {
                pos_y = Convert.ToInt32(GetRng((lower_terrain_height[i]-47), (47-upper_terrain_height[i])));
                obstacle_shape_rand = Convert.ToInt32(GetRng(1, 9));
                if (obstacle_shape_rand <= 3) {
                    GameObject obs = Instantiate(obstacle, new Vector3(i*2, pos_y ,0), Quaternion.identity);
                    spriteRenderer = obs.GetComponent<SpriteRenderer>();
                    spriteRenderer.size = new Vector2(4, 4);
                    boxCollider = obs.GetComponent<BoxCollider2D>();
                    boxCollider.size = new Vector3(4, 4);
                    boxCollider.offset = new Vector2(0, 2);
                }
                if (obstacle_shape_rand > 3 && obstacle_shape_rand <= 6) {
                    GameObject obs = Instantiate(obstacle, new Vector3(i*2, pos_y ,0), Quaternion.identity);
                    spriteRenderer = obs.GetComponent<SpriteRenderer>();
                    spriteRenderer.size = new Vector2(2, 4);
                    boxCollider = obs.GetComponent<BoxCollider2D>();
                    boxCollider.size = new Vector3(2, 4);
                    boxCollider.offset = new Vector2(0, 2);
                }
                if (obstacle_shape_rand > 6 && obstacle_shape_rand <= 9) {
                    GameObject obs = Instantiate(obstacle, new Vector3(i*2, pos_y ,0), Quaternion.identity);
                    spriteRenderer = obs.GetComponent<SpriteRenderer>();
                    spriteRenderer.size = new Vector2(4, 2);
                    boxCollider = obs.GetComponent<BoxCollider2D>();
                    boxCollider.size = new Vector3(4, 2);
                    boxCollider.offset = new Vector2(0, 1);
                }
                addition = 0;
                i += 5;
            }

        }
    }

    public double GetRng(double minNumber, double maxNumber) {
        System.Random random = new System.Random();
        double return_val = random.NextDouble() * (maxNumber - minNumber) + minNumber;
        return return_val; 
    }
}
