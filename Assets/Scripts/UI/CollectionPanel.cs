using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanel : MonoBehaviour
{
    public GameObject[] Panels;

    private int PanelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (Panels[PanelIndex] != null)
        {
            Panels[PanelIndex].SetActive(true);
            Panels[PanelIndex].transform.SetAsLastSibling();
        }
    }

    public void SetPanelIndex(int index)
    {
        Panels[PanelIndex].SetActive(false);

        PanelIndex = index;

        Panels[PanelIndex].SetActive(true);
        Panels[PanelIndex].transform.SetAsLastSibling();
    }

}
