using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //SerializeField
    [SerializeField]
    private GameObject _enemyPrefab;
    //Normal variables
    public int amountToSpawn = 3;
    private float _timer, _maxTimer = 3.0f;
    // Update is called once per frame
    void Update()
    {
        //enemy spawn with timer
        _timer -= Time.deltaTime;
        if( _timer <= 0 && amountToSpawn > 0)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            _timer = _maxTimer;
            amountToSpawn--;
        }
    }
}
