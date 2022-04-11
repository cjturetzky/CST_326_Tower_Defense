using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaycastScript : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public Transform spawnPoint;
    public Transform target;
    public GameObject healthBar;
    public GameObject enemy;
    public GameObject startButton;
    public GameObject restartButton;
    private AudioSource audio;
    private int coins = 1;
    private bool started = false;

    void Start(){
        coinText.SetText($"Coins: {coins}");
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            if (Physics.Raycast(ray, out RaycastHit hit)) {    
                Debug.Log($"You have clicked {hit.transform.name}");
                if(hit.transform.tag == "NavEnemy"){
                    hit.transform.GetComponent<NavEnemyScript>().clicked();
                }
                if(hit.transform.gameObject.tag == "Building"){
                    if(coins > 0){
                        changeCoins(-1);
                        hit.transform.GetComponent<BuildingBehavior>().Build();
                    }
                }
            }  
        }
        if(FindObjectsOfType<NavEnemyScript>().Length == 0 && started){
            restartButton.SetActive(true);
        }
    }

    private void EnemyDied(NavEnemyScript deadEnemy){
        changeCoins(deadEnemy.coinReward);
        audio.Play();
    }

    private void changeCoins(int amount){ 
        coins += amount;
        coinText.SetText($"Coins: {coins}");
    }

    public void startLevel(){
        StartCoroutine(spawner(5));
        startButton.SetActive(false);
        started = true;
    }

    IEnumerator spawner(int count){
        for(int i = 0; i < count; i++){
            GameObject e = Instantiate(enemy);
            e.transform.position = spawnPoint.position;
            //GameObject eHealth = Instantiate(healthBar);
            e.GetComponent<NavEnemyScript>().target = target;
            e.GetComponent<NavEnemyScript>().OnEnemyDied += EnemyDied;
            //e.GetComponent<NavEnemyScript>().healthImage = eHealth.GetComponent<Image>();
            yield return new WaitForSeconds(2);
        }
    }

    public void restartLevel(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Lose(){
        restartButton.SetActive(true);
    }
}
