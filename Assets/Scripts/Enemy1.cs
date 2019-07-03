using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBehaviour
{
    private float _fireRate;
    private float _canFire;

    public override void Init()
    {
        base.Init();
        _canFire = Time.time + Random.Range(5f, 8f);
    }

    public override void EnemyShoot()
    {
        _fireRate = Random.Range(8f, 10f);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, -4.5f));
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if (hit.collider != null && hit.collider.tag == "Enemy")
        {
            Debug.Log(hit.collider.name);
        }
        else
        {
            if (Time.time > _canFire && Time.timeScale != 0)
            {
                Instantiate(_missilePrefab, transform.position + new Vector3(0f, -0.45f, 0), Quaternion.identity);
                _canFire = Time.time + _fireRate;
            }
        }
    }
}
