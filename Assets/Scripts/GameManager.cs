using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject lifePrefab;
    public GameObject powerupsParent;
    private int health;
    public Image[] lives;
    public Sprite heart;
    public Sprite emptyHeart;
    public bool gameOver = false;
    public GameObject explosionPrefab;
    public float lifeSpawnTime = 15.0f;
    public float lifeSpawnTimer = 0;
    public GameObject player;
    private Player p;
    public TMP_Text lbl_Score;
    public int score;
    public int bestScore = 0;
    public GameObject infoPanel;
    public TMP_Text infoMsg;
    private float waveMsgTimer = 0;
    public AsteroidSpawnController asc;
    public bool showWave = true;
    public GameObject atkBuff;
    public float atkBuffTimer = 0;
    private float atkBuffSpawnTime = 22.0f;
    public GameObject spawnNerf;
    public float spawnNerfTimer;
    private float spawnNerfTime = 20f;
    public TMP_Text endMsg;
    public TMP_Text nbs;
    public AudioClip asteroidExplosion, itemCollected, playerHit;
    private AudioSource audioSource;
    public GameObject restartBtn;

    public TMP_Text hiScore;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        health = 3;
        lbl_Score.text = "Score: 0";
        infoPanel.SetActive(true);
        p = player.GetComponent<Player>();
        bestScore = PlayerPrefs.GetInt("bestScore");
        hiScore.text = "Best Score: " + bestScore;
        restartBtn.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        lifeSpawnTimer += Time.deltaTime;       //Es preparen els comptadors
        waveMsgTimer += Time.deltaTime;
        spawnNerfTimer += Time.deltaTime;
        atkBuffTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if (!gameOver)
        {
            foreach (Image img in lives)        //per defecte els cors de les vides seran de color negre
            {
                img.sprite = emptyHeart;
            }

            for (int i = 0; i < health; i++)    //per cada punt de vida del jugador, la imatge de la vida serà el cor vermell
            {
                lives[i].sprite = heart;
            }

            if (health < 6 && lifeSpawnTimer > lifeSpawnTime)
            {
               SpawnLife();                     //spawnejar vides sempre que el jugador tingui menys de 6 vides
               lifeSpawnTimer = 0;
            }

            else if (health == 6)               //si el jugador té 6 vides i hi ha alguna vida instanciada, s'elimina
            {   
                GameObject[] livesSpawned = GameObject.FindGameObjectsWithTag("Life");

                foreach (GameObject life in livesSpawned)
                {
                    Destroy(life);
                }
            }
            
            else if (health == 0)
            {                       
                gameOver = true;       
                GameOver();
            }
           
            if (showWave == true)           //per a mostrar el missatge informatiu de les waves
            {
                EnableWaveMsg(true);
                waveMsgTimer = 0;
                showWave = false;

            }
            
            //El missatge serà visible durant 2s, passat aquest temps es desactivar'a l'element corresponent de la UI
            if (waveMsgTimer > 2)
            {
                EnableWaveMsg(false);
            }

            if (asc.wave > 2 && spawnNerfTimer > spawnNerfTime)
            {
                CreateSpawnNerf();              //per spawnejar el porweer up que reduexi el temps d'aparició d'asteroides
                spawnNerfTimer = 0;
            }


            if (asc.wave > 5 && atkBuffTimer > atkBuffSpawnTime && !p.laserActive)
            {
                SpawnAtkBuff();         //per spawnejar el làser, només a partir de la wave 6
                atkBuffTimer = 0;
            }
        }
     
    }

    /**
     * Funció per a activar o desactivar els elements de la UI del missatge que surt en cada nova fase
     * @param
     * bool active - si s'ha d'activar la UI valdrà true, en cas contrari false
     */

    public void EnableWaveMsg(bool active)
    {
        if (active)
        {
            infoPanel.SetActive(true);
            infoMsg.text = "  Wave: " + asc.wave;
        }
        else
        {
            infoPanel.SetActive(false);
        }
        
    }


    public void AddLives()
    {
        health++;
    }

    public void AddScore()
    {
        score++;
        lbl_Score.text = "Score: " + score;
    }
    public void TakeDamage()
    {
        health--;
    }

    public void SpawnLife()
    {
        float xPos = Random.Range(-7f, 7f);
        float yPos = Random.Range(-4f, 4f);
        Instantiate(lifePrefab, new Vector2(xPos, yPos), quaternion.identity, powerupsParent.transform);
        
    }

    public void CreateSpawnNerf()
    {
        float xPos = Random.Range(-7f, 7f);
        float yPos = Random.Range(-4f, 4f);
        Instantiate(spawnNerf, new Vector2(xPos, yPos), quaternion.identity, powerupsParent.transform);
        
    }

    public void SpawnAtkBuff()
    {
        float xPos = Random.Range(-7f, 7f);
        float yPos = Random.Range(-4f, 4f);
        Instantiate(atkBuff, new Vector2(xPos, yPos), quaternion.identity, powerupsParent.transform);
    }
    public void GameOver()
    {
        
        Instantiate(explosionPrefab, player.transform.position, Quaternion.identity);  //el player explota 
        Destroy(player);
        
        endMsg.text = "GAME OVER";
        
        if (score > bestScore)
        {
            bestScore = score;                              //es guarda la best score si s'ha superat l'anterior best score i es mostren missatges
            PlayerPrefs.SetInt("bestScore", bestScore);
            nbs.text = "New Best Score: " + bestScore;
        }
       
        
        restartBtn.SetActive(true);         //s'activa el botó per tornar a jugar
        
        //es destrueixen tots els objectes que pugui haver-hi instanciats en l'escena
        
        GameObject[] asteroidsL = GameObject.FindGameObjectsWithTag("AsteroidL");
        GameObject[] asteroidsM = GameObject.FindGameObjectsWithTag("AsteroidM");
        GameObject[] asteroidsS = GameObject.FindGameObjectsWithTag("AsteroidS");
        GameObject[] livesSpawned = GameObject.FindGameObjectsWithTag("Life");
        GameObject[] nerfSpawned = GameObject.FindGameObjectsWithTag("SpawnNerf");
        GameObject[] atkBuffSpawned = GameObject.FindGameObjectsWithTag("atkBuff");

        foreach (GameObject asteroid in asteroidsL)
        {
            Destroy(asteroid);
        }
        foreach (GameObject asteroid in asteroidsM)
        {
            Destroy(asteroid);
        }
        foreach (GameObject asteroid in asteroidsS)
        {
            Destroy(asteroid);
        }

        foreach (GameObject life in livesSpawned)
        {
            Destroy(life);
        }

        foreach (GameObject sNerf in nerfSpawned)
        {
            Destroy(sNerf);
        }

        foreach (GameObject aBuff in atkBuffSpawned)
        {
            Destroy(aBuff);
        }

    }
    
    //funcions per a reproduir sons
    public void PlayAsteroidExplosionSound()
    {
        audioSource.PlayOneShot(asteroidExplosion);
    }

    public void PlayItemCollected()
    {
        audioSource.PlayOneShot(itemCollected);
    }

    public void PlayPlayerHit()
    {
        audioSource.PlayOneShot(playerHit);
    }

    //carregar l'escena del joc
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    
}
