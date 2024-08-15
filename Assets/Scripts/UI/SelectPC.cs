using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPC : MonoBehaviour
{
    public void SetPlayerIndex(int value)
    {
        GameManager.Instance.PlayerIndex = value;
        print(GameManager.Instance.PlayerIndex);
    }
}
