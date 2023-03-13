using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _strenght;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Button _ultimatePower;
    [SerializeField] private GameObject _uiPanel;
    
    private Vector3 _moveVector;
    
    private CharacterController _characterController;
    
    private int _currentHealth;
    private int _currentStrenght;
    private int _killEnemyCounter;
    
    public event Action KillAllEnemies;

    public int KillEnemyCounter
    {
        get => _killEnemyCounter;
        set { _killEnemyCounter = value; }
    }

    public int CurrentHealth
    {
        get => _currentHealth;
        private set {
            if (value < 0)
            {
                _currentHealth = 0;
            }
            else if (value > 100)
            {
                _currentHealth = 100;
            }
            else
            {
                _currentHealth = value;
            }
        }
    }

    public int CurrentStrenght
    {
        get => _currentStrenght;
        private set
        {
            if (value < 0)
            {
                _currentStrenght = 0;
            }
            else if (value > 100)
            {
                _currentStrenght = 100;
            }
            else
            {
                _currentStrenght = value;
            }
        }
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _currentHealth = _health;
        _currentStrenght = _strenght;
        _ultimatePower.onClick.AddListener(()=> UltimatePower());
    }

    private void Update()
    {
        MovePlayer();
        PlayerDeath();
    }
    
    private void MovePlayer()
    {
        _moveVector = Vector2.zero;
        _moveVector.x = _joystick.Horizontal;
        _moveVector.z = _joystick.Vertical;

        _moveVector = transform.right * _moveVector.x + transform.forward * _moveVector.z +
                      transform.up * _moveVector.y;

        _characterController.Move(_moveVector * (_moveSpeed * Time.deltaTime));
    }

    private void UltimatePower()
    {
        if (_currentStrenght == 100)
        {
            KillAllEnemies?.Invoke();
        }
    }

    private void PlayerDeath()
    {
        if (_currentHealth <= 0)
        {
            _uiPanel.SetActive(true);
            _uiPanel.transform.DOScale(1, 5f);
        }
    }

    public void TakeDamageHealth()
    {
        CurrentHealth -= 15;
        Debug.Log(CurrentHealth + "Health");
    }

    public void TakeDamageStrengh()
    {
        CurrentStrenght -= 25;
        Debug.Log(CurrentStrenght + "srength");
    }

    public void CollectStrength(int i)
    {
        CurrentStrenght += i;
    }
    
}
