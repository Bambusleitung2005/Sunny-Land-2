using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpFeetHitbox : MonoBehaviour
{
    private Rigidbody2D _rigidPlayer;
    void Start()
    {
        _rigidPlayer = transform.parent.parent.GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            _rigidPlayer.velocity = new Vector2(_rigidPlayer.velocity.x, hit.Knockback);
            hit.Damage();
        }
    }
}
