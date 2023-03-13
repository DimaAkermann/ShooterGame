using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBlue _enemyBluePrefab;
    [SerializeField] private Transform _spawnZoneCenter;
    [SerializeField] private float _spawnPointRadius;
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private EnemyRed _enemyRedPrefab;
    
    private Vector3 _randomBluePoint;
    private Vector3 _randomRedPoint;
    
    private List<EnemyBlue> _blues = new List<EnemyBlue>();
    private List<EnemyRed> _reds = new List<EnemyRed>();

    public List<EnemyBlue> EnemyBlues => _blues;
    
    private void Start()
    {
       StartCoroutine(GenerateBlueSpawnPoint());
       StartCoroutine(GenerateRedSpawnPoint());
    }

    private IEnumerator GenerateBlueSpawnPoint()
    {
        while (true)
        {
            if (_timeToSpawn > 2)
            {
                yield return new WaitForSeconds(_timeToSpawn);
                _timeToSpawn--;
                NavMesh.SamplePosition(Random.insideUnitSphere * _spawnPointRadius + _spawnZoneCenter.position, out NavMeshHit navHit,
                    _spawnPointRadius, NavMesh.AllAreas);
                _randomBluePoint = navHit.position;
                PoolBlueEnemy();
            }
            else if(_timeToSpawn == 2)
            {
                yield return new WaitForSeconds(_timeToSpawn);
                NavMesh.SamplePosition(Random.insideUnitSphere * _spawnPointRadius + _spawnZoneCenter.position, out NavMeshHit navHit,
                    _spawnPointRadius, NavMesh.AllAreas);
                _randomBluePoint = navHit.position;
                PoolBlueEnemy();
            }
            else
            {
                _timeToSpawn = 2;
            }
        }
    }

    private IEnumerator GenerateRedSpawnPoint()
    {
        while (true)
        {
            if (_timeToSpawn >= 2)
            {
                yield return new WaitForSeconds(_timeToSpawn);
                _timeToSpawn--;
                NavMesh.SamplePosition(Random.insideUnitSphere * _spawnPointRadius + _spawnZoneCenter.position, out NavMeshHit _hit,
                    _spawnPointRadius, NavMesh.AllAreas);
                _randomRedPoint = _hit.position;
                PoolRedEnemy();
            }
            else if(_timeToSpawn == 2)
            {
                yield return new WaitForSeconds(_timeToSpawn);
                NavMesh.SamplePosition(Random.insideUnitSphere * _spawnPointRadius + _spawnZoneCenter.position, out NavMeshHit _hit,
                    _spawnPointRadius, NavMesh.AllAreas);
                _randomRedPoint = _hit.position;
                PoolRedEnemy();
                
            }
            else
            {
                _timeToSpawn = 2;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_spawnZoneCenter.position,_spawnPointRadius);
    }


    private EnemyBlue SpawnBlueEnemy()
    {
        EnemyBlue enemyBlue = Instantiate(_enemyBluePrefab);
        enemyBlue.transform.position = _randomBluePoint;
        return enemyBlue;
    }

    private void PoolBlueEnemy()
    {
        foreach (var b in _blues)
        {
            if (!b.gameObject.activeSelf)
            {
                b.transform.position = _randomBluePoint;
                b.gameObject.SetActive(true);
                 StartCoroutine(b.CR_Patroling());
                return;
            }      
        }
        EnemyBlue enemyBlue = SpawnBlueEnemy();
        _blues.Add(enemyBlue);
        StartCoroutine(enemyBlue.CR_Patroling());
    }

    private EnemyRed SpawnRedEnemy()
    {
        EnemyRed enemyRed = Instantiate(_enemyRedPrefab);
        enemyRed.transform.position = _randomRedPoint;
        return enemyRed;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void PoolRedEnemy()
    {
        foreach (var r in _reds)
        {
            if (!r.gameObject.activeSelf)
            {
                r.transform.position = _randomRedPoint;
                r.gameObject.SetActive(true);
                r.Attack();
                return;
            }
        }
        EnemyRed enemyRed = SpawnRedEnemy();
        _reds.Add(enemyRed);
        enemyRed.Attack();
    }
}
