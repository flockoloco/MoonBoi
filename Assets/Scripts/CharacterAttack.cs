using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    //Obj
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Attack
        if (_animator != null && Input.GetKeyDown(KeyCode.Space) && CharacterMovement.canMove)
        {
            //_animator.SetBool("Attack", true);
            _animator.Play("HeroAttack");
            CharacterMovement.canMove = false;
        }
    }

    public void returnMovement()
    {
        //Call at end animation event
        CharacterMovement.canMove = true;
    }
}
