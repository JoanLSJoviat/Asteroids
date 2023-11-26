using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidBehavior : MonoBehaviour
{
    private float thrust = 1000f;
    public GameObject asteroidM;
    public GameObject asteroidS;
    private Transform parent;
    private GameManager gm;
  
   
    void Start()
    {
      
        // Es dona força i velocitat (i rotació) als asteroides
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * thrust);
        
        GetComponent<Rigidbody2D>()
            .angularVelocity = Random.Range(-0.0f, 90.0f);
        
        parent = GameObject.FindGameObjectWithTag("AsteroidsParent").transform;
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (gm == null)
        {
            Debug.Log("GM NOT ATTACHED");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Shot"))      //quan els asteroides detectin la "bala" del jugador
        {
            if (tag.Equals("AsteroidL"))
            {
                gm.PlayAsteroidExplosionSound();
                InstantiateAsteroidsM();    //si és un asteroide L, s'instanciaran asteroides M
                Destroy(gameObject);
                gm.AddScore();
            }
            else if (tag.Equals("AsteroidM"))
            {
                gm.PlayAsteroidExplosionSound();
                InstantiateAsteroidsS();    //si és un asteroide M, s'instanciaran asteroides S
                Destroy(gameObject);
                gm.AddScore();
            }
            else if (tag.Equals("AsteroidS"))
            {
                gm.PlayAsteroidExplosionSound();
                Destroy(gameObject);
                gm.AddScore();
            }
        }

        else if (other.collider.CompareTag("Player"))
        {
            gm.PlayPlayerHit();     //si els asteroides xoquen amb el jugador, sonarà un efecte de so i es restarà 1 vida
            gm.TakeDamage();
        }
    }

    public void InstantiateAsteroidsM()
    {
        Instantiate(asteroidM, new Vector3(transform.position.x - 0.5f, transform.position.y),
            Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
        Instantiate(asteroidM, new Vector3(transform.position.x + 0.5f, transform.position.y),
            Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
    }
    public void InstantiateAsteroidsS()
    {
        Instantiate(asteroidS, new Vector3(transform.position.x - 0.5f, transform.position.y),
            Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
        Instantiate(asteroidS, new Vector3(transform.position.x + 0.5f, transform.position.y),
            Quaternion.Euler(0.0f, 0.0f, Random.Range(-0.0f, 359.0f)), parent.transform);
    }

}
