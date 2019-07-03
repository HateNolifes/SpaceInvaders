using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDeer : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    protected AudioClip _explosionClip;
    [SerializeField]
    private float _speed = 3.0f;
    private float _xLimit = 9.4f;

    private UIManager _uiManager;

    public int score { get; set; }

    void Start()
    {
        score = 500;
        transform.position = new Vector3(-9.4f, 3.6f, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // move from left to right
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //when off the screen destroy spacedeer
        if (transform.position.x >= _xLimit)
        {
            Destroy(this.gameObject);
        }
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

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
    }
}
