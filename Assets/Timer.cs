using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer_text;
    public float current_time;

    void Update()
    {
        if (current_time  >= 0) {
            current_time -= Time.deltaTime;
            timer_text.text = current_time.ToString();   
        }
        if (current_time  <= 0) {
            timer_text.text = "00,00000";
            SceneManager.LoadScene("Lose");
        }
    }
}
