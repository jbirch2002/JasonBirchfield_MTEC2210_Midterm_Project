using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;  

public class Score : MonoBehaviour
{
    public TextMeshPro scoreText;
    int score;

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        scoreText.text = $"Score: {score.ToString()}";
    }

    public void ScoreIncrease()
    {
        score += 1;
    }

}
