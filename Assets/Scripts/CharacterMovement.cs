using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //Normal variables
    private float _horizontal;
    private bool _flipSprite = false;
    public static bool canMove = true;
    private float _speed = 5f;

    //Objs
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Transform _granChild;

    //Call anything before game "opens"
    void Awake()
    {
        //Get components
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _granChild = transform.GetChild(0).GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Get axis from ARrows or WASD
        if (canMove) _horizontal = Input.GetAxis("Horizontal");
        else _horizontal = 0;
        //Flip sprite
        if (_horizontal != 0) Flip();
    }

    private void FixedUpdate()
    {
        //movement
        if(canMove) _rb.velocity = new Vector2(_horizontal * _speed, _rb.velocity.y);
        if (_animator) _animator.SetFloat("xSpeed", Mathf.Abs( _horizontal) );
    }
    public void Flip()
    {
        //technically flip is called both ways 
        if( !_flipSprite && _horizontal < 0f || _flipSprite && _horizontal > 0f)
        {
            _flipSprite = !_flipSprite;
            _spriteRenderer.flipX = _flipSprite;
            //flip swordPath collision
            Vector3 ls = _granChild.localScale;
            ls.x *= -1f;
            _granChild.localScale = ls;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Floor")) canMove = true;
    }


}
