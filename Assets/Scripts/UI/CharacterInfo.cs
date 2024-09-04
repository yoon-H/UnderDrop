using JetBrains.Annotations;
using System;
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
    public GameObject OnObject;
    public Sprite OffImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        ChangeIndex(GameManager.Instance.PlayerIndex);
    }

    public void AddIndex(int value)
    {
        int res = CharacterIndex + value;

        if (res >= Characters.Length)
        {
            res = Characters.Length - 1;
        }
        else if (res < 0)
        {
            res = 0;
        }

        ChangeIndex(res);
    }

    public void ChangeIndex(int index)
    {
        Characters[CharacterIndex].SetActive(false);

        CharacterIndex = index;

        Characters[CharacterIndex].SetActive(true);

        //if (UnLocked[CharacterIndex])
        //{
        //    OnObject.SetActive(true);
        //    SelectImage.enabled = false;
        //}
        //else
        //{
        //    OnObject.SetActive(false);
        //    SelectImage.enabled = true;
        //    SelectImage.sprite = OffImage;
        //}
    }

    public void SelectPC()
    {
        if (UnLocked[CharacterIndex])
            GameManager.Instance.PlayerIndex = CharacterIndex;
    }
}
