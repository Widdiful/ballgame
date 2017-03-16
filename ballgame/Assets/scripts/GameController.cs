using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject zaku;
    public GameObject zaku_com;
    public GameObject zaku_char;
    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;
    public GameObject rock4;
    public GameObject rock5;
    public bool hardMode;
    public int wave = 1;
    public Text waveUI;
    public Text remaining;
    public int asteroidCount;

    private GameObject[] enemies;
    private int spawnCount;
    private Vector3 spawnposition;
    private Quaternion spawnrotation;
    private Vector3 ballpos;
    private GameObject spawnEnemy;
    private Vector3 rockPos;
    private Vector3 rockRot;
    private GameObject spawnRock;

	// Use this for initialization
	void Start () {
        spawnRocks(1, 1, 1, 1, 1, 1);
	}

    // Update is called once per frame
    void Update() {

        //Spawn enemies
        enemies = GameObject.FindGameObjectsWithTag("Actor");
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0) {
            ballpos = GameObject.Find("Mobile Pod Ball").transform.position;
        }
        if (enemies.Length == 0) {
            spawnCount = wave;
            if (hardMode) {
                spawnCount = wave * wave;
            }
            for (int i = 1; i <= spawnCount; i++) {
                int randOutput = Random.Range(0, 100);
                if (randOutput <= 90) {
                    spawnEnemy = zaku;
                }
                else if (randOutput <= 98) {
                    spawnEnemy = zaku_com;
                }
                else {
                    spawnEnemy = zaku_char;
                }
                spawnposition = new Vector3(ballpos.x + Random.Range(-50, 50), ballpos.y + Random.Range(-50, 50), ballpos.z + Random.Range(-10, 50));
                spawnrotation = new Quaternion();
                GameObject spawnedZaku = Instantiate(spawnEnemy, spawnposition, spawnrotation) as GameObject;
            }
            wave++;
        }

        //Update UI
        waveUI.text = "Wave: " + (wave - 1);
        remaining.text = "Remaining: " + enemies.Length;
    }

    void spawnRocks(int negX, int posX, int negY, int posY, int negZ, int posZ) {
        for (int i = 0; i <= asteroidCount * (asteroidCount / 1000); i++)
        {
            switch (Random.Range(0, 5))
            {
                case 0:
                    spawnRock = rock1;
                    break;
                case 1:
                    spawnRock = rock2;
                    break;
                case 2:
                    spawnRock = rock3;
                    break;
                case 3:
                    spawnRock = rock4;
                    break;
                case 4:
                    spawnRock = rock5;
                    break;
            }
            rockPos = new Vector3(Random.Range(ballpos.x + asteroidCount * -1 * negX, ballpos.x + asteroidCount * posX), Random.Range(ballpos.y + asteroidCount * -1 * negY, ballpos.y + asteroidCount * posY), Random.Range(ballpos.z + asteroidCount * -1 * negZ, ballpos.z + asteroidCount * posZ));
            rockRot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            GameObject spawnedRock = Instantiate(spawnRock, rockPos, Quaternion.Euler(rockRot.x, rockRot.y, rockRot.z)) as GameObject;
            spawnedRock.transform.localScale = new Vector3(Random.Range(1, 51), Random.Range(1, 51), Random.Range(1, 51));
            spawnedRock.GetComponent<Rigidbody>().AddForce(new Vector3(spawnedRock.transform.forward.x, spawnedRock.transform.forward.y, spawnedRock.transform.forward.z) * 10);
            spawnedRock.GetComponent<Rigidbody>().mass = ((spawnedRock.transform.localScale.x + spawnedRock.transform.localScale.y + spawnedRock.transform.localScale.z) / 3) * 5;
        }
    }
}
