using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float defDistanceRay = 100f;

    public Transform laserFirePoint;
    private GameManager gm;
    public LineRenderer lr;

    private Transform t;

    private void Awake()
    {
        t = GetComponent<Transform>();
        lr = GetComponent<LineRenderer>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }

   public void ShootLaser()
    {
        if (Physics2D.Raycast(t.position, transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, transform.up);
            Draw2DRay(laserFirePoint.position, hit.point);
                
                if (hit.collider.CompareTag("AsteroidL"))
                {
                    hit.collider.gameObject.GetComponent<AsteroidBehavior>().InstantiateAsteroidsM();
                    Destroy(hit.collider.gameObject);
                    gm.AddScore();
                }
            
                else if (hit.collider.CompareTag("AsteroidM"))
                {
                    hit.collider.gameObject.GetComponent<AsteroidBehavior>().InstantiateAsteroidsS();
                    Destroy(hit.collider.gameObject);
                    gm.AddScore();
                }
                
                else if (hit.collider.CompareTag("AsteroidS"))
                {
                    Destroy(hit.collider.gameObject);
                    gm.AddScore();
                }
                
        }
       
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.up * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
    }
}
