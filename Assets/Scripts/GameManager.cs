using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlayerPaddle;
    public SpriteRenderer PlayerPaddleRenderer;

    public Transform EnemyPaddle;
    public SpriteRenderer EnemyPaddleRenderer;

    public AudioSource LoseSound;

    public BallController BallController;

    public int PlayerScore = 0;
    public int EnemyScore = 0;

    public TextMeshProUGUI TextPointsPlayer;
    public TextMeshProUGUI TextPointsEnemy;
    public TextMeshProUGUI TextLevel;
    public TextMeshProUGUI TextTimer;
    public TextMeshProUGUI TextNamePlayer;
    public TextMeshProUGUI TextNameEnemy;

    private int WinPoints = 3;
    private int Level = 0;
    private bool _isTwoPlayerGame = false;

    void Start()
    {
        _isTwoPlayerGame = PlayerPrefs.GetInt(SettingsEnum.NumberPlayers.ToString(), 1) == 2;

        PlayerPaddleRenderer.color = ScenesManager.ColorManager.ColorPlayer;
        EnemyPaddleRenderer.color = ScenesManager.ColorManager.ColorEnemy;

        TextNamePlayer.text = ScenesManager.ColorManager.NamePlayer;

        if (_isTwoPlayerGame)
            TextNameEnemy.text = ScenesManager.ColorManager.NameEnemy;
        else
        {
            ScenesManager.ColorManager.NameEnemy = "Pong";
            TextNameEnemy.text = ScenesManager.ColorManager.NameEnemy;
        }
            
        ResetGame();
    }

    public void CheckWin()
    {
        if (_isTwoPlayerGame)
        {
            if (EnemyScore >= WinPoints || PlayerScore >= WinPoints)
            {
                
                if (EnemyScore >= WinPoints)
                    ScenesManager.ColorManager.Winner = ScenesManager.ColorManager.NameEnemy;
                else
                    ScenesManager.ColorManager.Winner = ScenesManager.ColorManager.NamePlayer;

                EndGame();
            }
        }
        else
        {
            if (EnemyScore >= WinPoints)
            {
                LoseSound.Play();
                Level = 1;
                ScenesManager.ColorManager.Winner = ScenesManager.ColorManager.NameEnemy;
                EndGame();
            }
            else if (PlayerScore >= WinPoints)
            {
                Level++;

                TextLevel.text = $"Level {Level}";

                ResetScore();

                switch (Level)
                {
                    case 1:
                        ScenesManager.LevelsManager.LoadLevel1();
                        break;
                    case 2:
                        ScenesManager.LevelsManager.LoadLevel2();
                        break;
                    case 3:
                        ScenesManager.LevelsManager.LoadLevel3();
                        break;
                    case 4:
                        ScenesManager.LevelsManager.LoadLevel4();
                        break;
                    case 5:
                        ScenesManager.LevelsManager.LoadLevel5();
                        break;
                    default:
                        LoseSound.Play();
                        Level = 1;
                        ScenesManager.ColorManager.Winner = ScenesManager.ColorManager.NamePlayer;
                        EndGame();
                        break;
                }
                
            }
        } 
    }

    public void EndGame()
    {
        ScenesManager.Instance.LoadScene(SceneEnum.FinalGame);
    }

    public void ResetGame()
    {
        PlayerPaddle.position = new Vector3(-7f, 0, 0);
        EnemyPaddle.position = new Vector3(7f, 0, 0);

        BallController.ResetBall();
        StartCoroutine(SetTimer());

        if (_isTwoPlayerGame)
        {
            TextLevel.text = string.Empty;
        }
        else
        {
            Level = 1;
            TextLevel.text = $"Level {Level}";
            ScenesManager.LevelsManager.LoadLevel1();
        }
    }

    private void ResetScore()
    {
        PlayerScore = 0;
        EnemyScore = 0;

        TextPointsEnemy.text = EnemyScore.ToString();
        TextPointsPlayer.text = PlayerScore.ToString();
    }

    private int SecondsTimer = 4;

    IEnumerator SetTimer()
    {
        SecondsTimer--;
        TextTimer.text = SecondsTimer.ToString();

        yield return new WaitForSeconds(1);

        if (SecondsTimer == 0)
        {
            SecondsTimer = 4;
            TextTimer.text = "";
        }
        else
        {
            StartCoroutine(SetTimer());
        }   
    }

    public void ScorePlayer()
    {
        PlayerScore++;
        TextPointsPlayer.text = PlayerScore.ToString();
        CheckWin();
    }

    public void ScoreEnemy()
    {
        EnemyScore++;
        TextPointsEnemy.text = EnemyScore.ToString();
        CheckWin();
    }
}