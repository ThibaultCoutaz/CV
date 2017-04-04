using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InitGame : MonoBehaviour {

    public string nameScene;

    void Awake()
    {
        Invoke("LaunchGame", 1.0f);
    }


    private void LaunchGame()
    {
        SceneManager.LoadScene(nameScene);
        DontDestroyOnLoad(HUDManager.Instance.gameObject);
        DontDestroyOnLoad(InputManager.Instance.gameObject);
    }
}
