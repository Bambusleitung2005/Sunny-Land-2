using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [SerializeField] private float _speed;
    [SerializeField] private float _climbSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private bool _grounded = true;
    [SerializeField] private bool _groundReset;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private float horizontalInput;
    private float verticalInput;
    private RaycastHit2D hitInfo;
    private PlayerAnimation _playerAnimation;
    private CapsuleCollider2D _collider;
    private bool _isDamageable = true;
    private bool _onLadder = false;

    [SerializeField] public int Health{get; set;}
    public int Knockback{get; set;}

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (_onLadder == false)
        {
            Movement();
        }
        else if (_onLadder)
        {
            LadderMovement();
        }
    }

    public void Movement()
    {
        //walking
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
        _playerAnimation.Run(horizontalInput);

        rigid.velocity = new Vector2(_speed * horizontalInput, rigid.velocity.y);

        if (horizontalInput != 0)
        {

            if(horizontalInput > 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }

        //jumping

        hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, _groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down * 1.0f, Color.green);

        if(hitInfo == true)
        {
            _playerAnimation.Falling(false);
        }

        if (hitInfo == true && _groundReset == true)
        {
            _collider.enabled = true;
            _grounded = true;
            _playerAnimation.Jump(false);
        }

        if (hitInfo == false)
        {
            StartCoroutine(GroundLoss());
        }

        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpHeight);
            _collider.enabled = false;
            _groundReset = false;
            _grounded = false;
            _playerAnimation.Jump(true);

            StartCoroutine(GroundReset());
        }

        IEnumerator GroundReset()
        {
            yield return new WaitForSeconds(0.2f);
            
            _groundReset = true;
        }

        IEnumerator GroundLoss()
        {
            yield return new WaitForSeconds(0.1f);
            if(_playerAnimation.InJump() == false && hitInfo == false)
            {
                _playerAnimation.Falling(true);
                _collider.enabled = false;
            }
            _grounded = false;
        }
    }

    public void ActivateOnLadder()
    {
        rigid.gravityScale = 0;
        _onLadder = true;
        _playerAnimation.Climbing();
        rigid.velocity = Vector2.zero;
    }

    public void DeactivateOnLadder()
    {
        _onLadder = false;
        _playerAnimation.StopClimbChill();
        _playerAnimation.StopClimbing();
        rigid.gravityScale = 1;
    }

    public void LadderMovement()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(horizontalInput == 0 && verticalInput == 0)
        {
            _playerAnimation.ClimbChill();
        }
        else
        {
            _playerAnimation.StopClimbChill();
        }

        rigid.velocity = new Vector2(_climbSpeed * horizontalInput, _climbSpeed * verticalInput);

    }

    public void Damage() 
    {
        if(_isDamageable == true)
        {
            Health--;
            _playerAnimation.Hurt();
            _isDamageable = false;
            Debug.Log("PlayerDamage");
        }
        StartCoroutine(resetIsDamagable());
        StartCoroutine(blink());
    }

    IEnumerator resetIsDamagable()
    {
        yield return new WaitForSeconds(1.0f);
        _isDamageable = true;
    }

    IEnumerator blink()
    {
        while (_isDamageable == false)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.05f);
        }
        sprite.enabled = true;
    }
}