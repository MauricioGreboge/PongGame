using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalGameMenu : MonoBehaviour
{
    public AudioSource LoseSound;

    [SerializeField]
    Button _mainMenu;

    [SerializeField]
    Button _playAgain;

    public TextMeshProUGUI TextWinner;

    private bool _isTwoPlayerGame = false;

    private void Awake()
    {
        _isTwoPlayerGame = PlayerPrefs.GetInt(SettingsEnum.NumberPlayers.ToString(), 1) == 2;

        if (LoseSound != null)
            LoseSound.Play();

        if (_isTwoPlayerGame)
        {
            TextWinner.text = "Vitória: " + ScenesManager.ColorManager.Winner;
        }
        else
        {
            if (ScenesManager.ColorManager.Winner == "Pong")
                TextWinner.text = "Você perdeu o jogo. Vitória: " + ScenesManager.ColorManager.Winner;
            else
                TextWinner.text = "Você ganhou o jogo! Vitória: " + ScenesManager.ColorManager.Winner;
        }

        PlayerPrefs.SetString(SettingsEnum.SavedWinner.ToString(), ScenesManager.ColorManager.Winner);

        _mainMenu.onClick.AddListener(MainMenu);
        _playAgain.onClick.AddListener(PlayAgain);
    }

    private void PlayAgain()
    {
        if (_isTwoPlayerGame)
            StartTwoPlayer();
        else
            StartOnePlayer();
    }

    private void MainMenu()
    {
        ScenesManager.Instance.LoadScene(SceneEnum.MainMenu);
    }

    private void StartOnePlayer()
    {
        PlayerPrefs.SetInt(SettingsEnum.NumberPlayers.ToString(), 1);
        PlayerPrefs.SetInt(SettingsEnum.PlayerStartSpeed.ToString(), 6);

        ScenesManager.Instance.LoadScene(SceneEnum.Game);
    }

    private void StartTwoPlayer()
    {
        PlayerPrefs.SetInt(SettingsEnum.NumberPlayers.ToString(), 2);
        PlayerPrefs.SetInt(SettingsEnum.PlayerStartSpeed.ToString(), 6);

        ScenesManager.Instance.LoadScene(SceneEnum.Game);
    }
}