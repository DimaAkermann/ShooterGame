using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Transform _playerTransform;
    private Coroutine _coroutine;

    public void SetPlayer(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }
    
    public void StartMove()
    {
        _coroutine = StartCoroutine(CR_Move());
    }

    private IEnumerator CR_Move()
    {
        while (true)
        {
            transform.Translate((_playerTransform.position - transform.position).normalized * (_speed * Time.deltaTime));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StopCoroutine(_coroutine);
        gameObject.SetActive(false);
        if (other.TryGetComponent(out PlayerController playerController))
        {
            playerController.TakeDamageStrengh();
        }
        Debug.Log("NS");
    }
}
