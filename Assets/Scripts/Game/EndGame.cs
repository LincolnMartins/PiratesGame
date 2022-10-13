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
        GameController.gameManager.playerScore = 0;
        GameController.gameManager.minutes = GameController.gameManager.gameSessionTime;
        GameController.gameManager.seconds = 0;
        GameController.gameManager.victory = false;
        GetComponentInParent<GameMenuManager>().endGameScreen.SetActive(false);

        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = Vector3.zero;
        player.transform.rotation = new Quaternion(0, 0, -180, 0);
        player.GetComponent<PlayerController>().health = player.GetComponent<PlayerController>().maxHealth;
        
        foreach (var ball in player.GetComponent<PlayerController>().cannonBalls) ball.SetActive(false);
        player.GetComponent<PlayerController>().frontReloadTime = 0;
        player.GetComponent<PlayerController>().rightReloadTime = 0;
        player.GetComponent<PlayerController>().leftReloadTime = 0;

        foreach (var enemy in GameController.gameManager.enemies) Destroy(enemy);
        GameController.gameManager.enemyShips.Clear();
        GameController.gameManager.InstantiateEnemyShips();
        GameController.gameManager.spawnTimer = 0;
    }

    public void OnClickMainMenu()
    {
        OnClickPlayAgain();
        SceneManager.LoadScene("Menu");
    }
}
