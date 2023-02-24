using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI endGameTitle;
    public TextMeshProUGUI endGameScore;

    private void OnEnable()
    {
        if (GameController.gameManager.victory) endGameTitle.text = "VICTORY";
        else endGameTitle.text = "DEFEAT";
        endGameScore.text = $"SCORE: {GameController.gameManager.playerScore}";
    }

    public void OnClickPlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickMainMenu()
    {
        //OnClickPlayAgain();
        SceneManager.LoadScene("Menu");
    }
}
