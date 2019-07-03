using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnTriggerEnter2D(Collider2D collider);
    int score { get; set; }
}
