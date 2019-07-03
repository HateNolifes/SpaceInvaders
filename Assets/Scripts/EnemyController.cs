 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject[] _enemyList;
    private SpawnManager _spawnManager;
    private EnemyBehaviour _enemyBehaviour;
    private float _coroutineGuard;

    void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _enemyList = new GameObject[_spawnManager.GetMaxEnemyCount()];
    }

    void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, -4.5f));
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        if (hit.collider != null && hit.collider.tag == "Enemy")
        {
            if (Time.time > _coroutineGuard && Time.timeScale != 0)
            {
                StartCoroutine(EnemiesGoDown());
                _coroutineGuard = Time.time + 2f;
            }
        }
    }

    public GameObject[] GetEnemies()
    {
        GameObject[] enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy");
        return enemiesAlive;
    }

    public IEnumerator EnemiesGoDown()
    {
        _enemyList = GetEnemies();
        foreach (GameObject enemy in _enemyList)
        {
            _enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
            _enemyBehaviour.EnemyMoveDown();
        }

        yield return new WaitForSeconds(3f);
    }
}
