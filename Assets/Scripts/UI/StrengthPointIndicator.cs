using TMPro;
using UnityEngine;

public class StrengthPointIndicator : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
     
    [SerializeField] private TextMeshProUGUI _strenghtPoint;
    [SerializeField] private TextMeshProUGUI _healthPoint;
    
    private void Update()
    {
        SetStrengthPoints();
        SetHealthPoints();
    }

    private void SetStrengthPoints()
    {
        _strenghtPoint.text = "";
        _strenghtPoint.text = "Strength : " + _playerController.CurrentStrenght;
    }

    private void SetHealthPoints()
    {
        _healthPoint.text = "";
        _healthPoint.text = "Health: " + _playerController.CurrentHealth;
    }
}
