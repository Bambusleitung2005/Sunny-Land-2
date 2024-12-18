using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] int _movementSpeed;
    [SerializeField] Transform pointA, pointB;
    protected Vector3 _currentTarget;
    protected SpriteRenderer _sprite;
    protected Animator _anim;
    protected AudioManager _audio;
    public virtual void Init()
    {
        _currentTarget = pointA.position;
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _audio = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        _anim = GetComponentInChildren<Animator>();

    }
    void Start()
    {
        Init();
    }

    void Update()
    {
        Movement();
    }
    public virtual void Damage()
    {
        health--;
        if (health < 1)
        {
            _audio.PlaySFX(_audio.enemyKill);
            Destroy(this.gameObject, 0.5f);
        }
    }

    public virtual void Movement()
    {
        if(_anim.GetBool("Dead") == false)
        {
            if (transform.position == pointA.transform.position)
        {
            _currentTarget = pointB.position;
            _sprite.flipX = true;
        }
        else if (transform.position == pointB.position)
        {
            _currentTarget = pointA.transform.position;
            _sprite.flipX = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, Time.deltaTime * _movementSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if (hit != null)
            {
                hit.Damage();
            }
        }
    }
}
