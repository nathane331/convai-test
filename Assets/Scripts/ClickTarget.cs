using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTarget : MonoBehaviour
{
 
    [SerializeField] ClickableFaceFeatureSO clickableFaceFeatureSO;
    [SerializeField] CustomizeMenuPanel customizeMenuPanel;
    // Update is called once per frame
    void Update()
    {
       
    }

    void OnMouseDown()
    {
        Debug.Log(gameObject.name);

        if(customizeMenuPanel.IsActive())
                {
                    customizeMenuPanel.Hide();
                    Debug.Log("Hide panel");
                }
                else
                {
                    customizeMenuPanel.Show(clickableFaceFeatureSO);
                    Debug.Log("Show panel");
                }
    }







}
