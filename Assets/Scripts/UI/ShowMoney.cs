using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoney : MonoBehaviour
{
    public Text MoneyText;
    // Start is called before the first frame update
    void Start()
    {
        SetMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMoney()
    {
        MoneyText.text = GameManager.Instance.Money.ToString();
    }
}
