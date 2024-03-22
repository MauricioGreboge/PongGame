using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    public static LevelsManager LevelsManager;
    public static ColorManager ColorManager;

    private void Awake()
    {
        Instance = this;

        if (LevelsManager == null)
            LevelsManager = new LevelsManager();

        if (ColorManager == null)
            ColorManager = new ColorManager();

        //ColorManager.ColorPlayer = Color.white;
        //ColorManager.ColorEnemy = Color.white;
    }

    public void LoadScene(SceneEnum sceneEnum)
    {
        SceneManager.LoadScene((int)sceneEnum);
    }
}

public class ColorManager
{
    public Color ColorPlayer { get; set; } = Color.white;
    public Color ColorEnemy { get; set; } = Color.white;

    public string NamePlayer { get; set; }
    public string NameEnemy { get; set; }

    public string Winner { get; set; }
}

public class LevelsManager
{
    public float EnemySpeedMoviment { get; set; }

    /// <summary>
    /// Máximo: -3.4f, 3.4f
    /// </summary>
    public Vector2 EnemyLimitSize { get; set; }

    public void LoadLevel1()
    {
        EnemySpeedMoviment = 2f;
        EnemyLimitSize = new Vector2(-2.5f, 2.5f);
    }

    public void LoadLevel2()
    {
        EnemySpeedMoviment = 2.5f;
        EnemyLimitSize = new Vector2(-2.7f, 2.7f);
    }

    public void LoadLevel3()
    {
        EnemySpeedMoviment = 2.8f;
        EnemyLimitSize = new Vector2(-2.9f, 2.9f);
    }

    public void LoadLevel4()
    {
        EnemySpeedMoviment = 3.6f;
        EnemyLimitSize = new Vector2(-3.1f, 3.1f);
    }

    public void LoadLevel5()
    {
        EnemySpeedMoviment = 4f;
        EnemyLimitSize = new Vector2(-3.4f, 3.4f);
    }
}

public enum SceneEnum
{
    MainMenu = 0,
    Game = 1,
    FinalGame = 2
}

public enum SettingsEnum
{
    NumberPlayers = 0,
    PlayerStartSpeed = 1,
    SavedWinner = 2,
}