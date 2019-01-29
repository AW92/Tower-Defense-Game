using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    //reference to ui countdown timer
    public Text waveCountdownText;
    

    //the .5 for these has been added so the ui text field for countdown doesnt seem to skip
    public float timeBetweenWaves = 5.5f;
    //two seconds before it spawns the first wave
    private float countdown = 2.5f;
    private int waveIndex = 0;


    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        //this rounds up the float number to a whole and puts it to string for the ui object. Mathf means mathfloat variable
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    //the method below has been changed to IEnumerator so that when spawning 2+ enemies, they aren't all on top of each other. 
    //Yield return new WaitForSeconds uses System.Collections above to put a delay in the for loop. 0.5 we hard coded can be used as variable to tweak but ok for now.

    IEnumerator SpawnWave()
    {
        //this has been moved to the top and called waveindex instead of wavenumber so its cleaner code
        waveIndex++;

        Debug.Log("Wave Incoming!");
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
