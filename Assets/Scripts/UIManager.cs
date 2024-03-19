using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> customizePanels = new List<GameObject>();
    [SerializeField] GameObject ChatObject;
    [SerializeField] GameObject CustomizeObject;

    [SerializeField] GameObject skinColorPanel;
    
    [SerializeField] Button[] skinButtons;
    [SerializeField] Button[] hairButtons;
    [SerializeField] CubeColorModifier colorModifier;
    [SerializeField] HairColorChanger hairColorChanger;

    // Start is called before the first frame update
    void Start()
    {
        //colorButtons = skinColorPanel.GetComponentsInChildren<Button>();

        for (int i = 0; i < skinButtons.Length; i++)
        {
            int buttonIndex = i;
            //colorButtons[i].onClick.AddListener(() => puppetCustoms.ColorChanger(buttonIndex, currentButton));
            skinButtons[i].onClick.AddListener(() => colorModifier.ChangeSkinColorPreset(buttonIndex));
            skinButtons[i].GetComponent<Image>().color = colorModifier.skinColors[buttonIndex];
        }

        for (int i = 0; i < hairButtons.Length; i++)
        {
            int buttonIndex = i;
            //colorButtons[i].onClick.AddListener(() => puppetCustoms.ColorChanger(buttonIndex, currentButton));
            //skinButtons[i].onClick.AddListener(() => colorModifier.ChangeSkinColorPreset(buttonIndex));
            hairButtons[i].GetComponent<Image>().color = hairColorChanger.hairColors[buttonIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPanel(GameObject newPanel) //when button is clicked, show this panel
    {
        foreach (GameObject panel in customizePanels) //if any other panel is active, turn it off
        {
            if (panel.activeSelf)
            {
                panel.SetActive(false); 
            }
        }
        newPanel.SetActive(true);
    }

    public void ShowCustomizeMenu()
    {
        if(ChatObject.activeSelf)
        {
            ChatObject.SetActive(false);
        }

        if(!CustomizeObject.activeSelf)
        {
            CustomizeObject.SetActive(true); 
        }
        else
            CustomizeObject.SetActive(false);
    }

    public void ShowChatMenu()
    {
        if (CustomizeObject.activeSelf)
        {
            CustomizeObject.SetActive(false);
        }

        if (!ChatObject.activeSelf)
        {
            ChatObject.SetActive(true);
        }
        else
            ChatObject.SetActive(false);
    }

    public void Reload()
    {
        SceneManager.LoadScene("ChatGPT Sample");
    }
}
