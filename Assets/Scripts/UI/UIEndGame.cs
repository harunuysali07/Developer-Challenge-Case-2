using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndGame : MonoBehaviour
{
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject failPanel;

    public void ShowEndGame(bool isWin)
    {
        successPanel.SetActive(isWin);
        failPanel.SetActive(!isWin);
    }

    public void OnPlayAgainButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnContinueButtonClick()
    {
        gameObject.SetActive(false);

        GameController.Instance.LevelStart();
    }
}
