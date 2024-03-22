using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenu : MonoBehaviour
{
    [SerializeField]
    Button _mainMenu;

    //[SerializeField]
    //Button _pause;

    void Start()
    {
        _mainMenu.onClick.AddListener(OpenMainMenu);
        //_pause.onClick.AddListener(Pause);
    }

    private void Pause()
    {
        throw new NotImplementedException();
    }

    private void OpenMainMenu()
    {
        ScenesManager.Instance.LoadScene(SceneEnum.MainMenu);
    }
}