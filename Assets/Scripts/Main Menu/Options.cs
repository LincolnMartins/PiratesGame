using System;
using UnityEngine;
using TMPro;

public class Options : MonoBehaviour
{
    public TextMeshProUGUI gameSessionTime;
    public TextMeshProUGUI enemySpawnTime;

    private void OnEnable()
    {
        gameSessionTime.text = GameController.gameManager.gameSessionTime.ToString();
        enemySpawnTime.text = GameController.gameManager.enemySpawnTime.ToString();
    }
    
    public void OnClickApply()
    {
        GameController.gameManager.gameSessionTime = Int32.Parse(gameSessionTime.text);
        GameController.gameManager.enemySpawnTime = Int32.Parse(enemySpawnTime.text);
        GetComponentInParent<MenuManager>().optionsScreen.SetActive(false);
    }

    public void OnClickCancel()
    {
        GetComponentInParent<MenuManager>().optionsScreen.SetActive(false);
    }
}
