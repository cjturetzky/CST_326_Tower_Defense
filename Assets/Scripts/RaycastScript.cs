using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastScript : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coins = 0;

    void Start(){
        foreach (EnemyDemo e in FindObjectsOfType<EnemyDemo>()){
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
                if(hit.transform.name == "Enemy"){
                    hit.transform.GetComponent<EnemyDemo>().clicked();
                }
            }  
        } 
    }

    private void EnemyDied(EnemyDemo deadEnemy){
        coins++;
        coinText.SetText($"Coins: {coins}");
    }
}
