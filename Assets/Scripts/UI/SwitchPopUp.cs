using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPopUp : MonoBehaviour
{
    public GameObject BlurPanel;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        if (!BlurPanel) { return; }
        BlurPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchFlag(bool isVisible)
    {
        if (isVisible) 
        {
            gameObject.SetActive(true);
            BlurPanel.gameObject.SetActive(true);
            BlurPanel.transform.SetAsLastSibling();
            gameObject.transform.SetAsLastSibling();
        }
        else
        {
            gameObject.SetActive(false);
            BlurPanel.gameObject.SetActive(false);
        }
    }
}
