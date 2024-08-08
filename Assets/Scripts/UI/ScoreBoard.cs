using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text ScoreText;
    public Text BestText;
    public Text CoinText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(int score, int coin)
    {
        ScoreText.text = score.ToString() + "m";
        BestText.text = "BEST " + GameManager.Instance.BestScore.ToString();
        CoinText.text = "x" + coin.ToString();
    }
}
