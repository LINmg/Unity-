using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Moving,
    Idle,
}


public class PlayerAnimation : MonoBehaviour
{
    private PlayerMove pm;
    private Animation ani;
    // Use this for initialization
    void Start()
    {
        pm = gameObject.GetComponent<PlayerMove>();
        ani = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        PlayAnimation(pm.state);
    }

    void PlayAnimation(PlayerState s)
    {
        if (s == PlayerState.Moving)
            ani.CrossFade("Run");
        else if (s == PlayerState.Idle)
            ani.CrossFade("Idle");
    }
}
