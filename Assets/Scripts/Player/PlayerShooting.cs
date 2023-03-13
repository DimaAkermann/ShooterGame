using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerBullet _bulletPref;
    [SerializeField] private Button _fireButton;
    [SerializeField] private Transform _spawnPos;

    private List<PlayerBullet> _playerBullets = new List<PlayerBullet>();
    
    private void Start()
    {
        _fireButton.onClick.AddListener(PoolBullet);
    }
    
    private PlayerBullet SpawnBullet()
    {
        PlayerBullet bullet = Instantiate(_bulletPref);
        _spawnPos.LookAt(CameraRay());
        bullet.transform.position = _spawnPos.position;
        bullet.transform.eulerAngles = _spawnPos.eulerAngles;
        return bullet;
    }

    private Vector3 CameraRay()
    {
        Vector3 dir = Camera.main.transform.forward * 100f;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray,out RaycastHit hit))
        {
            dir = hit.point;
        }
        
        return dir;
    }

    private void PoolBullet()
    {
        foreach (var b in _playerBullets)
        {
            if (!b.gameObject.activeSelf)
            {
                _spawnPos.LookAt(CameraRay());
                b.transform.position = _spawnPos.position;
                b.transform.eulerAngles = _spawnPos.eulerAngles;
                b.gameObject.SetActive(true);
                return;
            }      
        }
        PlayerBullet playerBullet = SpawnBullet();
        _playerBullets.Add(playerBullet);
    }
}
