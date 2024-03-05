using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTarget : MonoBehaviour
{
 
    [SerializeField] ClickableFaceFeatureSO clickableFaceFeatureSO;
    [SerializeField] CustomizeMenuPanel customizeMenuPanel;
    // Update is called once per frame
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log(gameObject.name);

        customizeMenuPanel.Hide();
        Debug.Log("Hide panel");

        customizeMenuPanel.Show(clickableFaceFeatureSO);
        Debug.Log("Show panel");

    }







}
