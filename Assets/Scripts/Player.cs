using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject prefabBullet;
    public float speedThrusting;
    public float speedTurn = 1.0f;
    public float turnDirection = 0.0f;
    private bool thrusting = false;
    public Rigidbody2D rb;
    public GameObject shootingPoint;
    public Vector2 playerPos;
    public bool justCrossed = false;
    public GameManager gm;
    public AsteroidSpawnController asc;
    public GameObject laserGun;
    public bool laserActive = false;
    public float laserTimer = 0;
    private float laserDuration = 7f;
    public AudioClip shoot;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f;
        }
        else
        {
            turnDirection = 0.0f;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !laserActive)
        {
            Shot();
        }

        
        // controlar el teleport del player en base als marges de l'àrea de joc
        if (transform.position.x > 9)
        {
            transform.position = new Vector2( -8.9f, transform.position.y);
        }
        else if (transform.position.x < -9)
        {
            transform.position  = new Vector2( 8.9f, transform.position.y);
        }

        else if (transform.position.y > 5)
        {
            transform.position  = new Vector2( transform.position.x, -4.9f);
        }
        else if (transform.position.y < -5)
        {
            transform.position  = new Vector2( transform.position.x, 4.9f);
        }

        // preparar comptador del làser
        laserTimer += Time.deltaTime;
        
        if (laserActive && laserTimer > laserDuration)
        {
            laserActive = false;    //passat el temps establert, el làser es destrueix
            Destroy(gameObject.transform.GetChild(1).gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rb.AddForce(transform.up*speedThrusting);
        }

        if (turnDirection != 0)
        {
            rb.AddTorque(turnDirection*speedTurn);    
        }
    }

    private void Shot()
    {
        audioSource.PlayOneShot(shoot);
        GameObject o = Instantiate(prefabBullet, shootingPoint.transform.position, transform.rotation, transform);
        Bullet b = o.GetComponent<Bullet>();
        b.Shot(transform.up);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // condicions per a la recol·lecció de power-ups/objectes
        
        if (other.CompareTag("Life"))
        {
            gm.PlayItemCollected();
            Debug.Log("Life detected");     
            gm.AddLives();
            Destroy(other.gameObject);
            gm.AddScore();
        }

        if (other.CompareTag("SpawnNerf"))
        {
            gm.PlayItemCollected();
            Debug.Log("SpawnNerf detected");
            asc.spawnTime += 0.3f;
            Destroy(other.gameObject);
            gm.AddScore();
        }

        if (other.CompareTag("atkBuff"))
        {
            Destroy(other.gameObject);
            Instantiate(laserGun, shootingPoint.transform.position, transform.rotation, transform);
            laserActive = true;
            laserTimer = 0;
        }
    }
    
}