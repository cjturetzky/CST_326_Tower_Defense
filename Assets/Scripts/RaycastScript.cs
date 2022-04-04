using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastScript : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coins = 1;

    void Start(){
        foreach (NavEnemyScript e in FindObjectsOfType<NavEnemyScript>()){
            e.OnEnemyDied += EnemyDied;
        }
        coinText.SetText($"Coins: {coins}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            if (Physics.Raycast(ray, out RaycastHit hit)) {    
                Debug.Log($"You have clicked {hit.transform.name}");
                if(hit.transform.name == "NavEnemy"){
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
    }

    private void EnemyDied(NavEnemyScript deadEnemy){
        changeCoins(deadEnemy.coinReward);
    }

    private void changeCoins(int amount){
        coins += amount;
        coinText.SetText($"Coins: {coins}");
    }
}
