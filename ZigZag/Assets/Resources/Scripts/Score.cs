using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static Text t;
    public static int ScoreCount = 0; // количество очков

    void Start()
    {
        t = GetComponent<Text>();
    }

    public static void AddScore()
    {
        ScoreCount++;
        t.text = Convert.ToString(ScoreCount);
    }

    public static void Reinit()
    {
        ScoreCount = 0;
        t.text = Convert.ToString(ScoreCount);
    }
}