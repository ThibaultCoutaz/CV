using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HUDMenu : HUDElement {

    // Use this for initialization
    void Start () {

    }

    public void startGame()
    {
        PlayerPrefs.SetInt("story", 0);
        HUDManager.Instance.displayMenu(false);
        LaunchGame("Dialogue");
    }

    public void LaunchGame(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        DontDestroyOnLoad(HUDManager.Instance.gameObject);
        DontDestroyOnLoad(InputManager.Instance.gameObject);
    }

    public void OpenCredit()
    {
        HUDManager.Instance.displayMenu(false);
        HUDManager.Instance.DisplayCredit(true);
    }

    public void OpenCV()
    {
        HUDManager.Instance.displayMenu(false);
        HUDManager.Instance.DisplayCV(true);
    }
}
