using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public GameObject endGameScreen;

    // Update is called once per frame
    void Update()
    {
        if (!endGameScreen.activeSelf)
            if (GameController.gameManager.victory || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health < 1)
                endGameScreen.SetActive(true);
    }
}
