using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastScript : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coins = 0;

    void Start(){
        foreach (NavEnemyScript e in FindObjectsOfType<NavEnemyScript>()){
            e.OnEnemyDied += EnemyDied;
        }
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
                if(hit.transform.name == "Building"){
                    hit.transform.GetComponent<BuildingBehavior>().Build();
                }
            }  
        } 
    }

    private void EnemyDied(NavEnemyScript deadEnemy){
        coins += deadEnemy.coinReward;
        coinText.SetText($"Coins: {coins}");
    }
}
