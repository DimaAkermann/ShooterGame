using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBlue : Enemy
{
    [SerializeField] private EnemyBullet _enemyBulletPrefab;
    [SerializeField] private Transform _spawnPos;

    private List<EnemyBullet> _bullets = new List<EnemyBullet>();
   
    private NavMeshAgent _agent;
    private NavMeshPath _path;
    

    protected override void Start()
    {
        _path = new NavMeshPath();
        _agent = GetComponent<NavMeshAgent>();
        base.Start();
        StartCoroutine(CR_BlueEnemyAttack());
        _playerPosition.GetComponent<PlayerController>().KillAllEnemies += Disable;
    }
    
    public override void Attack()
    {
        PoolBullet();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator CR_BlueEnemyAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Attack();
        }
    }

   private EnemyBullet SpawnBullet()
   {
       var b = Instantiate(_enemyBulletPrefab);
       b.transform.position = _spawnPos.position;
       return b;
   }

   private void PoolBullet()
   {
       foreach (var b in _bullets)
       {
           if (!b.gameObject.activeSelf)
           {
               b.transform.position = _spawnPos.position;
               b.gameObject.SetActive(true);
               SetBulletDirection(b);
               return;
           }      
       }
        EnemyBullet enemyBullet = SpawnBullet();
        _bullets.Add(enemyBullet);
        SetBulletDirection(enemyBullet);
   }

   private void SetBulletDirection(EnemyBullet enemyBullet)
   {
       enemyBullet.SetPlayer(_playerPosition);
       enemyBullet.StartMove();
   }
   
   public IEnumerator CR_Patroling()
   {
       while (true)
       {
           yield return new WaitForSeconds(Time.deltaTime);
           _agent.SetDestination(_playerPosition.position);
           _agent.CalculatePath(_playerPosition.position, _path);
       }
   }

   private void OnTriggerEnter(Collider other)
   {
       if (other.TryGetComponent(out PlayerBullet playerBullet))
       {
           _playerPosition.GetComponent<PlayerController>().CollectStrength(_points);
       }
       gameObject.SetActive(false);
   }

   private void OnEnable()
   {
       StartCoroutine(CR_BlueEnemyAttack());
   }

   private void OnDisable()
   {
       StopCoroutine(CR_Patroling());
   }
   
   private void Disable()
   {
       gameObject.SetActive(false);
   }
}
