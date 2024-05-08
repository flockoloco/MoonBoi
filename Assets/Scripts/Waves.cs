using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Waves : MonoBehaviour
{
    //Normal var
    private float _timer = 15f;
    private float _timerMax = 15f;
    private int _currentWave = 0;
    private int amountEnemies = 3;

    //SerializeField
    [SerializeField]
    private TextMeshProUGUI waveText;
    [SerializeField]
    private TextMeshProUGUI incText;
    [SerializeField]
    private List<GameObject> waveSpawns = new List<GameObject>();

 

    // Update is called once per frame
    void Update()
    {

        //Start portals every 15 seconds, previous portal resets.
        _timer -= Time.deltaTime;
        if ( _timer <= 0 && _currentWave < waveSpawns.Count)
        {
            waveSpawns[_currentWave].GetComponent<Animator>().enabled = true;
            waveSpawns[_currentWave].GetComponent<EnemySpawn>().enabled = true;
            for(int i = 0; i <= _currentWave; i++) {
                waveSpawns[i].GetComponent<EnemySpawn>().amountToSpawn = amountEnemies;
            }
            _currentWave++;
            _timer = _timerMax;
            waveText.text = _currentWave.ToString();
        }
        incText.text = "Next wave in: " + _timer.ToString("F1");
    }
}
