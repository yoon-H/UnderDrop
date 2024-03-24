using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSize : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width /2 , Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
