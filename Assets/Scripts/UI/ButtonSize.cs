using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSize : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        // 캔버스의 스케일링 모드에 따라 화면 크기 계산
        

        // 버튼의 원래 크기
        

        RectTransform rectTransform = GetComponent<RectTransform>();
        if (!rectTransform) return;

        //Get referenceResolution from Canvas
        CanvasScaler scaler = GetComponentInParent<CanvasScaler>();
        if (!scaler) return;

        float referenceWidth = scaler.referenceResolution.x;
        float scaledWidth = referenceWidth;

        rectTransform.sizeDelta = new Vector2(scaledWidth, rectTransform.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
