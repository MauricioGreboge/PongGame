using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddleController : MonoBehaviour
{
    private float Speed;


    void Start()
    {
        Speed = PlayerPrefs.GetInt(SettingsEnum.PlayerStartSpeed.ToString(), 6);
    }

    void Update()
    {
        float movimentInput = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.position + Vector3.up * movimentInput * Speed * Time.deltaTime;

        newPosition.y = Mathf.Clamp(newPosition.y, -3.4f, 3.4f);

        transform.position = newPosition;
    }
}
