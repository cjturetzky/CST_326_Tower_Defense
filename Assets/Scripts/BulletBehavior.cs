using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public GameObject target;
    public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            Destroy(this.gameObject);
        }
        else{
            Vector3 targetPosition = target.transform.position;
            Vector3 movementDir = (targetPosition - transform.position).normalized;

            Vector3 newPosition = transform.position;
            newPosition += movementDir * speed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.tag != "Building"){
            Destroy(this.gameObject);
        }
        
    }
}
