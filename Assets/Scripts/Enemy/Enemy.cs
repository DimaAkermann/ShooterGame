using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] protected int _points;
    protected Transform _playerPosition;

    public float MoveSpeed => _moveSpeed;
    public int Points => _points;

    protected virtual void Start()
    {
        _playerPosition = FindObjectOfType<PlayerController>().transform;
    }


    public abstract void Attack();
   
}
