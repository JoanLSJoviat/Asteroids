using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TMP_Text bestScore;
    private AudioSource audioSource;
    public AudioClip start;
    
    // Start is called before the first frame update
    void Start()
    {
        bestScore.text = "Best Score: " + PlayerPrefs.GetInt("bestScore");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(start);
     
        StartCoroutine(DelaySceneLoad());
        
        IEnumerator DelaySceneLoad()        //i s'esperen 1.3 segons abans de carregar la següent escena
        {
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene(1);
        }
    }
}
