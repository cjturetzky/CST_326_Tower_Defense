using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavEnemyScript : MonoBehaviour
{
    public int hp = 3;
    public int maxHp = 3;
    public int coinReward = 2;

    public Transform target;

    public delegate void EnemyDied(NavEnemyScript deadEnemy);
    public event EnemyDied OnEnemyDied;

    //public Image healthImage;
    public Vector3 offset;

    bool enemyDied = false;
    // Start is called before the first frame update

    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 meshPosition = GetNavmeshPosition(target.position);
        agent.SetDestination(meshPosition);
        //moveImage();

        // Debug.Log(Vector3.Distance(target.position, transform.position));
        if(Vector3.Distance(target.position, transform.position) < 1){
            Debug.Log("Enemy made it to the end. Game over.");
            FindObjectOfType<RaycastScript>().Lose();
        }
        if(enemyDied){
            OnEnemyDied?.Invoke(this);
            Destroy(this.gameObject);
        }

    }

    Vector3 GetNavmeshPosition(Vector3 samplePosition){
        NavMesh.SamplePosition(samplePosition, out NavMeshHit hitInfo, 100f, -1);
        return hitInfo.position;
    }

    public void clicked(){
        hp--;
        Debug.Log("Ouch!");
        if(hp == 0){
            enemyDied = true;
        }
    }

    // void moveImage(){
    //     healthImage.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    //     healthImage.fillAmount = (float) hp/maxHp;
    // }

    void OnTriggerEnter(Collider col){
        Debug.Log(col.gameObject);
        clicked();
    }
}
