using System.Collections;
using UnityEngine;


public class EnemyRed : Enemy
{
    [SerializeField] private float _speedBoost;
    [SerializeField] private float _maxUpperPoint;

    private Coroutine _coroutine;

    protected override void Start()
    {
        base.Start();
        Attack();
        _playerPosition.GetComponent<PlayerController>().KillAllEnemies += Disable;
    }

    public override void Attack()
    {
        _coroutine = StartCoroutine(CR_MoveUp());
        Debug.Log("Red");
    }

    private IEnumerator CR_MoveUp()
    {
        while (transform.position.y < _maxUpperPoint)
        {
            transform.Translate(Vector3.up * (MoveSpeed * Time.deltaTime));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(CR_MoveDown());
    }

    private IEnumerator CR_MoveDown()
    {
        StopCoroutine(_coroutine);
        Vector3 pos = _playerPosition.position;
        Vector3 dir = (pos - transform.position).normalized;
        while (true)
        {
            transform.Translate(dir * (_speedBoost * Time.deltaTime));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            playerController.TakeDamageHealth();
        }

        if (other.TryGetComponent(out PlayerBullet playerBullet))
        {
            _playerPosition.GetComponent<PlayerController>().CollectStrength(_points);
        }
        gameObject.SetActive(false);
        Debug.Log("Rozbilsa");
    }

    private void OnEnable()
    {
        Attack();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
    
}
