using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private AudioClip _loseLifeClip;
    [SerializeField]
    private AudioClip _shootingClip;
    [SerializeField]
    private AudioClip _explosionClip;

    [SerializeField]
    public GameObject[] thrusters;

    [SerializeField]
    private bool isPlayerOne = false;
    [SerializeField]
    private int playerLife;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    public bool leftGunFired = false;

    [SerializeField]
    private float _speed = 5.0f;
    private float _xLimit = 9.4f;
    private float horizontalInput;
    public bool canMove = true;

    private UIManager _uiManager;
    private GameManager _gameManager;

    private void Start()
    {
        playerLife = 3;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        PlayerMovement();
        if(isPlayerOne == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                FireLaser();
            }
        }
        else if (isPlayerOne == false)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                FireLaser();
            }
        }
    }

    private void PlayerMovement()
    {
        if (isPlayerOne == true)
        {
            horizontalInput = Input.GetAxis("HorizontalPlayerOne");
        }
        else if (isPlayerOne == false)
        {
            horizontalInput = Input.GetAxis("HorizontalPlayerTwo");
        }

        if (canMove == true)
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        }

        //If player pos on x is greater than 8.4 show him on other side of screen
        if (transform.position.x > _xLimit)
        {
            transform.position = new Vector3(-_xLimit, transform.position.y, 0);
        }
        else if (transform.position.x < -_xLimit)
        {
            transform.position = new Vector3(_xLimit, transform.position.y, 0);
        }
    }

    private void FireLaser()
    {
        if (Time.time > _canFire && Time.timeScale != 0)
        {
            if (leftGunFired == false)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(-0.23f, 0.414f, 0), Quaternion.identity);
                leftGunFired = true;
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0.23f, 0.414f, 0), Quaternion.identity);
                leftGunFired = false;
            }

            AudioSource.PlayClipAtPoint(_shootingClip, Camera.main.transform.position, 0.15f);
            _canFire = Time.time + _fireRate;
        }
    }

    public void LoseLife()
    {
        playerLife = playerLife - 1;
        _uiManager.UpdateLives(playerLife);
        AudioSource.PlayClipAtPoint(_loseLifeClip, Camera.main.transform.position, 1f);

        if (playerLife == 0)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, 1f);
            _gameManager.GameOver();
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "EnemyMissile")
        {
            if (collider.transform.parent != null)
            {
                Destroy(collider.transform.parent.gameObject);
            }
            Destroy(collider.gameObject);

            LoseLife();
        }
        else if(collider.tag == "Enemy")
        {
            _gameManager.GameOver();
        }
    }
}
