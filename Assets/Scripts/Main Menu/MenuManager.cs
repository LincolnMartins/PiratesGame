using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsScreen;

    public void OnClickOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
    }
}
