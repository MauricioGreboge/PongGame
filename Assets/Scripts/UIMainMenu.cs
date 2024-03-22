using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField]
    Button _startOnePlayerGame;

    [SerializeField]
    Button _startTwoPlayerGame;

    [SerializeField]
    Button _exitGame;

    public GameObject Player;
    public GameObject Enemy;

    public TMP_InputField InputFieldPlayer;
    public TMP_InputField InputFieldEnemy;
    public TextMeshProUGUI TextLastWinner;

    void Start()
    {
        _startOnePlayerGame.onClick.AddListener(StartOnePlayer);
        _startTwoPlayerGame.onClick.AddListener(StartTwoPlayer);
        _exitGame.onClick.AddListener(ExitGame);
    }

    private void Awake()
    {
        Player.GetComponent<Image>().color = ScenesManager.ColorManager.ColorPlayer;
        Enemy.GetComponent<Image>().color = ScenesManager.ColorManager.ColorEnemy;

        InputFieldPlayer.text = ScenesManager.ColorManager.NamePlayer;
        InputFieldEnemy.text = ScenesManager.ColorManager.NameEnemy;
        
        string lastWinner = PlayerPrefs.GetString(SettingsEnum.SavedWinner.ToString(), string.Empty);
        TextLastWinner.text = $"Último Ganhador: {lastWinner}";
    }

    private void StartOnePlayer()
    {
        PlayerPrefs.SetInt(SettingsEnum.NumberPlayers.ToString(), 1);
        PlayerPrefs.SetInt(SettingsEnum.PlayerStartSpeed.ToString(), 6);

        ScenesManager.ColorManager.NamePlayer = InputFieldPlayer.text;
        ScenesManager.ColorManager.NameEnemy = InputFieldEnemy.text;

        ScenesManager.Instance.LoadScene(SceneEnum.Game);
    }

    private void StartTwoPlayer()
    {
        PlayerPrefs.SetInt(SettingsEnum.NumberPlayers.ToString(), 2);
        PlayerPrefs.SetInt(SettingsEnum.PlayerStartSpeed.ToString(), 6);

        ScenesManager.ColorManager.NamePlayer = InputFieldPlayer.text;
        ScenesManager.ColorManager.NameEnemy = InputFieldEnemy.text;

        ScenesManager.Instance.LoadScene(SceneEnum.Game);
    }

    private void ExitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        TextLastWinner.text = $"Último Ganhador: ";
    }

    public void SetPlayerColor(Button button)
    {
        ScenesManager.ColorManager.ColorPlayer = button.colors.normalColor;
        Player.GetComponent<Image>().color = ScenesManager.ColorManager.ColorPlayer;
    }

    public void SetEnemyColor(Button button)
    {
        ScenesManager.ColorManager.ColorEnemy = button.colors.normalColor;
        Enemy.GetComponent<Image>().color = ScenesManager.ColorManager.ColorEnemy;
    }
}