using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private GameObject _coopPlayerPrefab;
    [SerializeField]
    private GameObject _barrierPrefab;
    [SerializeField]
    private GameObject _spawnManagerPrefab;
    [SerializeField]
    private GameObject _enemyControllerPrefab;
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private AudioClip _gameOverClip;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private EnemyController[] _enemyController;
    private GameObject[] _enemiesList;
    private Barrier[] _barriers;
    private Player[] _players;

    public bool coopModeOn = false;
    public bool gameStarted = false;
    public bool gamePaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameStarted == false)
        {
            GameStarted();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameStarted == false)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.P) && gameStarted == true && gamePaused == false)
        {
            gamePaused = true;
            _pauseMenu.SetActive(true);
            _uiManager.ShowTitleScreen();
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.P) && gamePaused == true)
        {
            _uiManager.HideTitleScreen();
            gamePaused = false;
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void GameStarted()
    {
        gameStarted = true;
        Time.timeScale = 1;

        if (coopModeOn == false)
        {
            Instantiate(_playerPrefab, new Vector2(0, -4), Quaternion.identity);
            Instantiate(_barrierPrefab, new Vector2(-6, -2.7f), Quaternion.identity);
            Instantiate(_barrierPrefab, new Vector2(-2, -2.7f), Quaternion.identity);
            Instantiate(_barrierPrefab, new Vector2(2, -2.7f), Quaternion.identity);
            Instantiate(_barrierPrefab, new Vector2(6, -2.7f), Quaternion.identity);
        }
        else if (coopModeOn == true)
        {
            Instantiate(_playerPrefab, new Vector2(-2, -4), Quaternion.identity);
            Instantiate(_coopPlayerPrefab, new Vector2(2, -4), Quaternion.identity);
            Instantiate(_barrierPrefab, new Vector2(-6, -2.7f), Quaternion.identity);
            Instantiate(_barrierPrefab, new Vector2(-2, -2.7f), Quaternion.identity);
            Instantiate(_barrierPrefab, new Vector2(2, -2.7f), Quaternion.identity);
            Instantiate(_barrierPrefab, new Vector2(6, -2.7f), Quaternion.identity);
        }

        Instantiate(_spawnManagerPrefab, transform.position, Quaternion.identity);
        Instantiate(_enemyControllerPrefab, new Vector2(-8.5f, 4.5f), Quaternion.identity);
        Instantiate(_enemyControllerPrefab, new Vector2(8.5f, 4.5f), Quaternion.identity);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.HideTitleScreen();
        _uiManager.HideInstructions();
        _uiManager.UpdateLives(3);
    }

    public void GameOver()
    {
        AudioSource.PlayClipAtPoint(_gameOverClip, Camera.main.transform.position, 1f);

        _spawnManager = FindObjectOfType<SpawnManager>();
        if (_spawnManager != null)
        {
            Destroy(_spawnManager.gameObject);
        }

        _enemyController = FindObjectsOfType<EnemyController>();
        _enemiesList = _enemyController[0].GetEnemies();
        foreach (GameObject enemy in _enemiesList)
        {
            Destroy(enemy);
        }
        foreach (EnemyController enemyController in _enemyController)
        {
            Destroy(enemyController.gameObject);
        }

        _barriers = FindObjectsOfType<Barrier>();
        foreach (Barrier barrier in _barriers)
        {
            Destroy(barrier.gameObject);
        }

        _players = FindObjectsOfType<Player>();
        foreach (Player player in _players)
        {
            Destroy(player.gameObject);
        }

        gameStarted = false;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.CheckForTopScore();
        _uiManager.totalScore = 0;
        _uiManager.scoreText.text = "Score: " + _uiManager.totalScore;
        _uiManager.ShowTitleScreen();
        _uiManager.ShowInstructions();
    }

    public void ResumeGame()
    {
        gamePaused = false;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
