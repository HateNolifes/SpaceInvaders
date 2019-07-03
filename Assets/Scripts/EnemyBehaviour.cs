using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour, IDamageable
{
    

    [SerializeField]
    protected GameObject _enemyExplosionPrefab;
    [SerializeField]
    protected GameObject _missilePrefab;
    [SerializeField]
    protected AudioClip _shootingClip;
    [SerializeField]
    protected AudioClip _explosionClip;
    [SerializeField]
    protected int scoreValue;
    protected float _speed = 0.3f;

    protected UIManager _uiManager;
    protected SpawnManager _spawnManager;
  
    public int score { get; set; }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        EnemyShoot();
        EnemyMoveToSide();
    }

    //Use this for initialization of children
    public virtual void Init()
    {
        score = scoreValue;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

   public void EnemyMoveDown()
   {
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);

        if (_speed > 0)
        {
            _speed = _speed + 0.15f;
        }
        else
        {
            _speed = _speed - 0.15f;
        }
        
        ChangeMoveDirection();
   }

   public void ChangeMoveDirection()
   {
       _speed = -_speed;
   }

   public void EnemyMoveToSide()
   {
       transform.Translate(Vector3.right * _speed * Time.deltaTime);
   }

   public void OnTriggerEnter2D(Collider2D collider)
   {
       if (collider.tag == "Laser")
       {
           if (collider.transform.parent != null)
           {
               Destroy(collider.transform.parent.gameObject);
           }
           Destroy(collider.gameObject);

           if (_uiManager != null)
           {
               _uiManager.UpdateScore(score);
           }

           Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
           AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, 1f);
           _spawnManager.EnemyDestroyed();
           Destroy(this.gameObject);
       }
   }

   abstract public void EnemyShoot();
}
