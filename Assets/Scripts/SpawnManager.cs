using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyShipPrefab;
    [SerializeField]
    private GameObject _spaceDeerPrefab;
    [SerializeField]
    private AudioClip _newRoundClip;

    private GameObject[,] _enemies;
    private int columns, rows;
    private int _enemyCount = 0;
    private Vector2 enemyPosition;
    private float startX = -6f;
    private float startY = 3;

    // Use this for initialization
    void Start()
    {
        columns = 12;
        rows = 4;
        _enemies = new GameObject[columns,rows];
        StartCoroutine(SpawnSpaceDeer());
        AudioSource.PlayClipAtPoint(_newRoundClip, Camera.main.transform.position, 0.7f);
    }

    void Update()
    {
        if(_enemyCount == 0)
        {
            StartCoroutine(SpawnEnemy());    
        }
    }

    public void EnemyDestroyed()
    {
        if(_enemyCount > 0)
        {
            _enemyCount--;
        }
        else if(_enemyCount == 0)
        {
            AudioSource.PlayClipAtPoint(_newRoundClip, Camera.main.transform.position, 0.7f);
        }
    }

    public int GetMaxEnemyCount()
    {
        int maxEnemyCount = columns * rows;
        return maxEnemyCount;
    }

    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3.0f);

        while (_enemyCount == 0)
        {
            for (int i = 0; i < rows; i++)
            {
                int enemyPrefab = Random.Range(0, 4);
                for (int j = 0; j < columns; j++)
                {
                    GameObject newEnemy;
                    enemyPosition.x = startX + j * 1.1f;
                    enemyPosition.y = startY - i * 0.8f;

                    newEnemy = Instantiate(_enemyShipPrefab[enemyPrefab], enemyPosition, Quaternion.identity);

                    _enemies[j, i] = newEnemy;
                    _enemyCount++;
                }
            }
        } 
    }

    public IEnumerator SpawnSpaceDeer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            Instantiate(_spaceDeerPrefab, transform.position, Quaternion.Euler(0, 0, -90));
            yield return new WaitForSeconds(5.0f);
        }
    }
}
