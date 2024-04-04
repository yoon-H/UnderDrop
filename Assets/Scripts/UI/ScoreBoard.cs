using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text ScoreText;
    public Text BestText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(int score)
    {
        ScoreText.text = GameManager.Instance.BestScore.ToString() + "km";
        BestText.text = "BEST " + score.ToString();
    }
}
