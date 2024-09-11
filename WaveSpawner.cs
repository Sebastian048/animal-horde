using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public int waveNum = 1;
    public Text waveNumText;

    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    public Transform[] allEnemies;

    public Transform[] bossEnemies;

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform[] enemy;
        public int[] enemyCount;
        public float rate;
    }

    public Wave[] waves;
    public int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves;
    private float waveCountDown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    int totalEnemies;

    public Text enemiesLeft;

    public Transform placeMap1;

    public bool roundPause;

    public GunManager gun;

    int totalEnemiesAdded;
    public int enemiesThatWereRemoved;

    public Animator bossTextAnim;

    void Start () {
        waveCountDown = timeBetweenWaves;
    }

    void Update() {
        placeMap1.rotation = Quaternion.Euler(0, 0, 0);

        if (roundPause == false) {
            enemiesLeft.text = "Enemies Left: " + GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();

            if (state == SpawnState.WAITING) {
            if (!EnemyIsAliveTest()) {
                WaveCompleted();
            } else {
                return;
            }
        }
           if (waveCountDown <= 0) {
            if (state != SpawnState.SPAWNING) {
                MakeWave();
            }
        } else {
            waveCountDown -= Time.deltaTime;
        } 
        }
    }

    void WaveCompleted () {

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        totalEnemiesAdded = 0;

        if (nextWave + 1 > waves.Length - 1) {
            for (int i = 0; i < waves[nextWave].enemy.Length; i++) {
                waves[nextWave].enemyCount[i] += 1;
            }
        } else {
            nextWave++;
        }

        waveNum += + 1;
        waveNumText.text = "Round: " + waveNum;
    }

    public bool EnemyIsAliveTest() {
        int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesLeft.text = "Enemies Left: " + enemiesAlive.ToString();
        if(enemiesAlive == 0) {
            FindObjectOfType<Shop>().OpenShop();

            roundPause = true;

            //filling ammo
            gun.FillAllAmmo();
            return false;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        state = SpawnState.SPAWNING;

        for (int w = 0; w < _wave.enemyCount.Length; w++) {
            for (int i = 0; i < _wave.enemyCount[w]; i++) {
                SpawnEnemy(_wave.enemy[w]);
                totalEnemies += 1;
                yield return new WaitForSeconds(_wave.rate);
            }
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy (Transform _enemy){
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Transform _enemyI = Instantiate (_enemy, sp.position, sp.rotation);
        _enemyI.GetComponent<Enemy>().wave = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        _enemyI.GetComponent<Enemy>().UpdateEnemyDifficulty();
    }

    void MakeWave () {
        int enemiesAdded;

        if (waveNum < 5) {
            enemiesAdded = 3;
        } else if (waveNum < 10) {
            enemiesAdded = 2;
        } else if (waveNum < 20) {
            enemiesAdded = 1;
        } else {
            enemiesAdded = 0;
        }

        if (waveNum%2 == 0) {
            enemiesAdded = (int)(enemiesAdded * 0.6f);
        }

        Transform[] newWaveSize = new Transform[waves[nextWave].enemy.Length + enemiesAdded];
        waves[nextWave].enemy = newWaveSize;

        if (waveNum%2 == 0) {
            enemiesThatWereRemoved = newWaveSize.Length - (int)(newWaveSize.Length * 0.6f);

            bossTextAnim.SetBool("playTextAnim", true);
        } else {
            bossTextAnim.SetBool("playTextAnim", false);
        }

        int[] newWaveCount = new int[waves[nextWave].enemy.Length + enemiesAdded];
        waves[nextWave].enemyCount = newWaveCount;
        

        for (int i = 0; i < waves[nextWave].enemy.Length; i++) {

            int lowNum;
            int highNum;

            if (waveNum < 3) {
                lowNum = 0;
                highNum = 5;
            } else if (waveNum < 5) {
                lowNum = 1;
                highNum = 6;
            } else if (waveNum < 6) {
                lowNum = 2;
                highNum = 7;
            } else if (waveNum < 7){
                lowNum = 3;
                highNum = 8;
            } else if (waveNum < 9){
                lowNum = 4;
                highNum = 9;
            } else if (waveNum < 10){
                lowNum = 4;
                highNum = 10;
            } else{
                lowNum = 4;
                highNum = 11;
            }

            if (waves[nextWave].enemy.Length - totalEnemiesAdded <= 2 && waveNum%2 == 0) {
                waves[nextWave].enemy[i] = bossEnemies[Random.Range(lowNum, highNum)];
                Debug.Log("worked");
            } else {

            
            
            waves[nextWave].enemy[i] = allEnemies[Random.Range(lowNum, highNum)];

            }

            waves[nextWave].enemyCount[i] += 1;
            totalEnemiesAdded += 1;
        }

        waves[nextWave].rate = 2; //2

        StartCoroutine(SpawnWave (waves[nextWave]));
    }
}
