using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Helpers;


public class UIManager : Instancable<UIManager>
{
    public void LevelUpButton()
    {
        Destroy(GameManager.Instance._parent);
        LevelManager.Instance.LevelUp();
        GameManager.Instance.SetLevel();
        GameManager.Instance.nextLevelCanvas.SetActive(false);
        GameManager.Instance.buttonPanel.SetActive(true);
    }

    public void Reload()
    {
        Destroy(GameManager.Instance._parent);
        LevelManager.Instance.Reload();
        GameManager.Instance.SetLevel();
      
        GameManager.Instance.reloadPanel.SetActive(false);
        GameManager.Instance.buttonPanel.SetActive(true);
    }
}
