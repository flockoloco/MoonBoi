using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    /// <summary>
    /// Updates Score on UI based on current score (current score +1 when enemy dies)
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public static int currentScore = 0;
    //public int score { get { return _currentScore; } set { onScoreChange(value); } }

    private void Update()
    {
        scoreText.text = currentScore.ToString();
    }
//     private void onScoreChange(int value)
//     {
//         _currentScore = value;
//         scoreText.text = value.ToString();
//     }
}
