using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AsteroidSpawnController : MonoBehaviour
{
    public GameObject asteroidL;
    public GameObject asteroidM;
    public GameObject asteroidS;
    public float spawnTime = 7.0f;
    public float spawnTimer;
    public GameManager gm;
    private float maxWaveDuration = 30f;
    public float waveTimer = 0;
    public int wave = 1;

    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;         
        SpawnAsteroid(1);       //s'instancien directament els primers asteroides per no esperar a que passi el temps
       
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        waveTimer += Time.deltaTime;

        if (!gm.gameOver)
        {
            if (spawnTimer > spawnTime)     //mentre no sigui game over, s'instanciaran asteroides de diferents maneres depenent de la wave en que estiguem
            {
                SpawnAsteroid(wave);
                spawnTimer = 0;
            }
        }

        if (waveTimer > maxWaveDuration)
        {
            wave++;
            spawnTime -= 0.4f;      //a cada nova wave, el temps d'sintanciació d'asteroides es redueix 0,4s
            waveTimer = 0;
            gm.showWave = true;
        }
        
    }
    
    /**
     * Mètode per instanciar asteroides.
     * @param int w : numero de la wave en la que estiguem
     */
    public void SpawnAsteroid(int w)
    {

        //es calculen posicions en base a la distancia entre l'àrea de joc i els coliders externs a aquesta
        
        float xLeftPos = Random.Range(-11.5f, -9f);     
        float xRightPos = Random.Range(9f, 11.5f);
        float yLowPos = Random.Range(-7f, -5f);
        float yUpPos = Random.Range(5f, 7f);
        float yPos = Random.Range(-7f, 7f);
        float xPos = Random.Range(-11.5f, 11.5f);

       if (w == 1)
       {
         
           Instantiate(asteroidS, new Vector3(xLeftPos, yPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidS, new Vector3(xLeftPos, yPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           
         
       }
       else if (w == 2)
       {
           Instantiate(asteroidS, new Vector3(xRightPos, yPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidS, new Vector3(xRightPos, yPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
          
        
       }
       else if (w == 3)
       {
           Instantiate(asteroidM, new Vector3(xPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
       }
       
       else if (w == 4)
       {
           Instantiate(asteroidM, new Vector3(xLeftPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidS, new Vector3(xLeftPos, yPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
       }
       else if (w == 5)
       {
           Instantiate(asteroidM, new Vector3(xRightPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidM, new Vector3(xRightPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
          
       }
       else if (w == 6)
       {
           Instantiate(asteroidM, new Vector3(xPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidM, new Vector3(xPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidS, new Vector3(xPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
       }

       else if (w == 7)
       {
           Instantiate(asteroidL, new Vector3(xRightPos, yPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
        
       }
       
       else if (w >= 8 && w <= 11)
       {
           Instantiate(asteroidL, new Vector3(xLeftPos, yPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidM, new Vector3(xLeftPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
       }
       else if (w > 11  && w <= 15)
       {
           Instantiate(asteroidL, new Vector3(xPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidL, new Vector3(xPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           
       }
       else if(w > 15 && w < 20)
       {
           Instantiate(asteroidL, new Vector3(xRightPos, yPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidM, new Vector3(xLeftPos, yPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidS, new Vector3(xPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           
       }
       else
       {
           Instantiate(asteroidL, new Vector3(xPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidL, new Vector3(xPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           Instantiate(asteroidL, new Vector3(xPos, yUpPos),
               Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
           
           
       }

    }
    
}
