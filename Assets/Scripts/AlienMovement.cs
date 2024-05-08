using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    //Public 
    [Range(0f, 1f)]
    public float _horizontal = 0.4f;
    public bool dead = false;

    //private normal var
    private int _score = 1;
    private float _speed = 5.0f;
    private bool _flipSprite = false;
    private float _impulseForce = 4f;

    //SerializeField
    [SerializeField]
    private int _health = 2;
    [SerializeField]
    private bool _onGround = true;

    //Objects
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private GameObject _pTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get components
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _pTarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Go player direction
        if ( (transform.position.x - _pTarget.transform.position.x) > 0) _horizontal = -0.4f;
        else _horizontal = 0.4f;
        //Flip image
        if (_horizontal != 0) Flip();
        //Death
        if (_health <= 0 && _onGround && !dead)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezePosition;
            dead = true;
            Invoke("Death", 1f);
            _animator.SetBool("Die", true);
        }

    }

    private void FixedUpdate()
    {
        //movement
        if (_onGround && !dead) _rb.velocity = new Vector2(_horizontal * _speed, _rb.velocity.y);
        if (_animator) _animator.SetFloat("xSpeed", Mathf.Abs(_horizontal));
    }
    private void Flip()
    {
        //Flip to left
        if (!_flipSprite && _horizontal < 0f || _flipSprite && _horizontal > 0f)
        {
            _flipSprite = !_flipSprite;
            _spriteRenderer.flipX = _flipSprite;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword" && _onGround)
        {
            //Knock back
            if (_health > 1)
            {
                _onGround = false;
                float ImpulseDirection = transform.position.x - collision.gameObject.transform.parent.parent.position.x;
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ImpulseDirection * _impulseForce, 6f), ForceMode2D.Impulse);
                _animator.Play("AlienHit");
            }
            _health -= 1;
        }
    }
    private void Death()
    {
        _onGround = false;
        Score.currentScore += _score;
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Floor") ) _onGround = true;
    }
}
