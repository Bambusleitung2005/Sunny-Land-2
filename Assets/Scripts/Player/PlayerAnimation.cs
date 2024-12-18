using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private AudioManager _audio;
    private bool _runReset = true;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _audio = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
    }

    public void Run(float _horizontalInput)
    {
        _anim.SetFloat("Run", System.Math.Abs(_horizontalInput));
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Run") && _runReset)
        {
            StartCoroutine(playRunSound());
        }
    }

    IEnumerator playRunSound()
    {
        _runReset = false;
        _audio.PlaySFX(_audio.stepSound);
        yield return new WaitForSeconds(0.2f);
        _runReset = true;
    }

    public void Jump(bool jump)
    {
        
        _anim.SetBool("Jump", jump);
        if (jump)
        {
            _audio.PlaySFX(_audio.jump);
        }
    
    }

    public void Falling(bool fall)
    {
        
        _anim.SetBool("Falling", fall);
        
    }

    public bool InJump()
    {
        if (_anim.GetBool("Jump"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Hurt()
    {
        _anim.SetTrigger("Damage");
    }

    public void Climbing()
    {
        _anim.SetBool("Climbing", true);
    }

    public void StopClimbing()
    {
        _anim.SetBool("Climbing", false);
    }

    public void ClimbChill()
    {
        _anim.SetBool("ClimbChill", true);
    }

    public void StopClimbChill()
    {
        _anim.SetBool("ClimbChill", false);
    }
}
