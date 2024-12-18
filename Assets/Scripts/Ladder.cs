using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Player _player;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player.ActivateOnLadder();
            Debug.Log("OnTriggerEnter2D triggered");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collider2D ladderCollider = GetComponent<Collider2D>();
            Collider2D playerCollider = other.GetComponent<Collider2D>();

            if (ladderCollider.bounds.Intersects(playerCollider.bounds))
            {
                Debug.Log("False exit detected. Player still in ladder bounds.");
                return; // Beende die Methode, wenn der Spieler noch in der Leiter ist
            }

            Debug.Log("Player fully exited the ladder.");
            _player.DeactivateOnLadder();
        }
    }

}
