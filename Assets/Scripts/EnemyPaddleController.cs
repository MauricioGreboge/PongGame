using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaddleController : MonoBehaviour
{
    private Rigidbody2D Rb;
    private float Speed;

    public Vector2 Limits = new Vector2(-3.4f, 3.4f);

    private GameObject Ball;

    private bool _isTwoPlayerGame = false;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Ball = GameObject.Find("Ball");

        _isTwoPlayerGame = PlayerPrefs.GetInt(SettingsEnum.NumberPlayers.ToString(), 1) == 2;
        Speed = PlayerPrefs.GetInt(SettingsEnum.PlayerStartSpeed.ToString(), 6);
    }

    void Update()
    {
        if (Ball != null)
        {
            if (_isTwoPlayerGame)
            {
                float movimentInput = Input.GetAxis("VerticalPlayer2");

                Vector3 newPosition = transform.position + Vector3.up * movimentInput * Speed * Time.deltaTime;

                newPosition.y = Mathf.Clamp(newPosition.y, -3.4f, 3.4f);

                transform.position = newPosition;
            }
            else
            {
                float targetY = Mathf.Clamp(Ball.transform.position.y, ScenesManager.LevelsManager.EnemyLimitSize.x, ScenesManager.LevelsManager.EnemyLimitSize.y); // Limita a posição Y
                Vector2 targetPosition = new Vector2(transform.position.x, targetY);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * ScenesManager.LevelsManager.EnemySpeedMoviment); // Move gradualmente para a posição Y da bola
            }
        }
    }
}