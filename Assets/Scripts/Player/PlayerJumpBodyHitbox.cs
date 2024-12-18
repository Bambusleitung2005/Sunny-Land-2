using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBodyHitbox : MonoBehaviour
{
    private Player _player;
    void Start()
    {
        _player = transform.parent.parent.GetComponent<Player>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            _player.Damage();
        }
    }
}
