using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public int score;

    private void Update()
    {
        scoreText.text = score.ToString();
    }
}
