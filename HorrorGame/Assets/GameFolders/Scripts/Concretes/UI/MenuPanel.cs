using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] GameObject HelpPanel;
    
    public void HelpButton()
    {
        if(HelpPanel.activeSelf)
        {
            HelpPanel.SetActive(false);
        }
        else
        {
            HelpPanel.SetActive(true);
        }
    }
    public void QuitButton()
    {
        GameManager.Instance.ExitGame();
    }
    public void PlayButton()
    {
        GameManager.Instance.StartGame();
    }
}
