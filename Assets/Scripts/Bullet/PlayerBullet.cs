using System.Collections;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private PlayerController _playerController;

    private Coroutine _move;
    
    private void OnEnable()
    {
        _move = StartCoroutine(CR_Move());
        StartCoroutine(CR_Timer());
    }

    private void Disable()
    {
        StopCoroutine(_move);
        gameObject.SetActive(false);
    }
    
    private IEnumerator CR_Move()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    
    private IEnumerator CR_Timer()
    {
        yield return new WaitForSeconds(_lifeTime);
        Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        Disable();
        if (other.TryGetComponent(out Enemy enemy))
        {
            _playerController.CollectStrength(enemy.Points);
            _playerController.KillEnemyCounter += 1;
            other.gameObject.SetActive(false);
        }
    }
}
