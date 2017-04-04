using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public bool IsPressingHaut { get; private set; }
    public bool PauseCamera { get; private set; }
    public bool IsPressingSpace { get; private set; }

    void Update()
    {
        IsPressingHaut = Input.GetButton("Haut");
        PauseCamera = Input.GetButtonDown("Cancel");
        IsPressingSpace = Input.GetButton("Jump");
    }

    public float GetHorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }

    public float GetHorizontalMouse()
    {
        return Input.GetAxis("Mouse X");
    }

    public float GetVerticalMouse()
    {
        return Input.GetAxis("Mouse Y");
    }
}
