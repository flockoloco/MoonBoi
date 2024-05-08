using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HealthManager : MonoBehaviour
{
    //SerializeField
    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private GameObject deathPanel;

    //Normal var
    public static bool _invul = false;
    private float _invulTimer = 2.0f;
    private float _invulTimerReset = 2.0f;
    private float _impulseForce = 3f;

    //Obj
    public GameObject[] listHealth = new GameObject[3];    

    private void Update()
    {
        //Call death
        if (_health <= 0)
        {
            CharacterMovement.canMove = false;
            Invoke("EndGame", 1.0f);
        }
        //Invulnerability time
        if(_invul)
        {
            _invulTimer -= Time.deltaTime;
            if (_invulTimer < 0) invencibleReset();
        }
    }
    private void EndGame()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Knock back from enemy
        if (collision.collider.tag == "Enemy" && _health >= 0 && !_invul && !collision.collider.gameObject.GetComponent<AlienMovement>().dead)
        {
            updateUI();
            CharacterMovement.canMove = false;
            float forceDirection = Vector2.Distance(transform.position, collision.collider.transform.position);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceDirection * _impulseForce, 5f), ForceMode2D.Impulse);
        }
    }

    private void updateUI()
    {
        //Update Health UI
        _invul = true;
        _health -= 1;
        listHealth[_health].gameObject.GetComponent<Animator>().Play("HealthUI");
        gameObject.GetComponent<Animator>().Play("HeroDie");
    }
    private void invencibleReset()
    {
        //reset invul
        _invul = false;
        _invulTimer = _invulTimerReset;
    }
}
