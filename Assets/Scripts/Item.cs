using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    private AudioManager _audio;
    void Start()
    {
        _audio = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _audio.PlaySFX(_audio.itemCollected);
            Destroy(gameObject);
        }
    }
}
