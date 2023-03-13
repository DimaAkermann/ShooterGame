using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _killEnemyScore;
    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartLevel);
    }

    private void Update()
    {
        _killEnemyScore.text = "";
        _killEnemyScore.text = "Enemy killed : " + _playerController.KillEnemyCounter;
    }

    private void RestartLevel()
    {
        gameObject.transform.DOScale(0, 4f);
        gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }
}
