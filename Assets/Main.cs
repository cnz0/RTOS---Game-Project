using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    [SerializeField] GameObject dirt;
    [SerializeField] GameObject stone;
    [SerializeField] GameObject grass;
    [SerializeField] GameObject obstacle;
    [SerializeField] int level_length;
    [SerializeField] int minimal_height;
    [SerializeField] int maximal_height;

    // -> To jest print, polecam: Debug.Log(list_of_values[i]);

    void Start()
    {
        Generate();
    }
    void Update()
    {
        
    }

    void Generate() {

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

        for (i = 0 ; i < length ; i++) {
            value = random.Next(dynamic_minimum, dynamic_maximum);
            dynamic_minimum = value-4;
            if (dynamic_minimum < minNumber) {
                dynamic_minimum = minNumber;
            }
            dynamic_maximum = value+4;
            if (dynamic_maximum > maxNumber) {
                dynamic_maximum = maxNumber;
            }
            list_of_values.Add(value);
        }
        return list_of_values; 
    }

    public void GenerateTerrain() {

        List<int> lower_terrain_height = new List<int>();
        List<int> upper_terrain_height = new List<int>();
        SpriteRenderer spriteRenderer;
        Vector3 scale;
        int i;

        lower_terrain_height = RandomNumberGenerator(minimal_height,maximal_height,level_length);
        upper_terrain_height = RandomNumberGenerator(minimal_height,maximal_height,level_length);

        for (i = 0 ; i < level_length ; i++) {
            GameObject terrain = Instantiate(dirt, new Vector3(i*2, -30 ,0), Quaternion.identity);
            spriteRenderer = terrain.GetComponent<SpriteRenderer>();
            spriteRenderer.size = new Vector2(2, lower_terrain_height[i]);
        }

        for (i = 0 ; i < level_length ; i++) {
            GameObject terrain = Instantiate(dirt, new Vector3(i*2, 30 ,0), Quaternion.identity);
            spriteRenderer = terrain.GetComponent<SpriteRenderer>();
            spriteRenderer.size = new Vector2(2, upper_terrain_height[i]);
            scale = terrain.transform.localScale;
            scale.y *= -1;
            terrain.transform.localScale = scale;
        }
    }
}
