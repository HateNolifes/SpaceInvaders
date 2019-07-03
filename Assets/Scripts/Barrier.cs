using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour,IDamageable
{
    [SerializeField]
    private Sprite[] _barrierSprites;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private AudioClip _explosionClip;

    private int _health;

    private SpriteRenderer _spriteRenderer;

    public int score { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _health = 5;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "EnemyMissile")
        {
            _health--;

            if (collider.transform.parent != null)
            {
                Destroy(collider.transform.parent.gameObject);
            }
            Destroy(collider.gameObject);

            switch (_health)
            {
                case 4:
                    _explosionPrefab.transform.localScale = new Vector2(0.5f, 0.5f);
                    Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, -1f), Quaternion.identity);
                    _spriteRenderer.sprite = _barrierSprites[1];
                    break;

                case 3:
                    _explosionPrefab.transform.localScale = new Vector2(0.5f, 0.5f);
                    Instantiate(_explosionPrefab, new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, -1f), Quaternion.identity);
                    _spriteRenderer.sprite = _barrierSprites[2];
                    break;

                case 2:
                    _explosionPrefab.transform.localScale = new Vector2(0.5f, 0.5f);
                    Instantiate(_explosionPrefab, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, -1f), Quaternion.identity);
                    _spriteRenderer.sprite = _barrierSprites[3];
                    break;

                case 1:
                    _explosionPrefab.transform.localScale = new Vector2(0.8f, 0.8f);
                    Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, -1f), Quaternion.identity);
                    _spriteRenderer.sprite = _barrierSprites[4];
                    break;

                case 0:
                    _explosionPrefab.transform.localScale = new Vector2(1.5f, 1.5f);
                    Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y, -1f), Quaternion.identity);
                    Destroy(this.gameObject);
                    break;

                default:
                    break;
            }

            AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, 1f);
        }
        else if (collider.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
