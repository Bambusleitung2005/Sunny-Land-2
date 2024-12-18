using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Living_Platform : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    Vector3 _currentTarget;
    void Start()
    {
        _currentTarget = pointA.position;
    }
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if(transform.position == pointA.position)
        {
            _currentTarget = pointB.position;
        }
        if(transform.position == pointB.position)
        {
            _currentTarget = pointA.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, Time.deltaTime * _speed);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
