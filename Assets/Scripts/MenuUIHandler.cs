using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField NameInput;

    private void Start()
    {
        NameInput.text = GameDataManager.Instance.PlayerName;
    }

    public void StartGame()
    {
        string playerName = NameInput.text.Trim();

        if (string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "Jugador";
        }

        GameDataManager.Instance.PlayerName = playerName;

        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}