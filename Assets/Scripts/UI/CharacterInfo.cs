using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    private int CharacterIndex = 0;

    public GameObject[] Characters;
    private bool[] UnLocked = { true, true, false, false, false, false };
    public Image SelectImage;
    public Sprite OnImage;
    public Sprite OffImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeIndex(int value)
    {
        Characters[CharacterIndex].SetActive(false);

        CharacterIndex += value;

        if(CharacterIndex >= Characters.Length)
        {
            CharacterIndex = Characters.Length -1;
        }
        else if(CharacterIndex < 0)
        {
            CharacterIndex = 0;
        }

        Characters[CharacterIndex].SetActive(true);

        if (UnLocked[CharacterIndex])
        {
            SelectImage.sprite = OnImage;
        }
        else
        {
            SelectImage.sprite = OffImage;
        }
    }

    public void SelectPC()
    {
        if (UnLocked[CharacterIndex])
            GameManager.Instance.PlayerIndex = CharacterIndex;
    }
}
