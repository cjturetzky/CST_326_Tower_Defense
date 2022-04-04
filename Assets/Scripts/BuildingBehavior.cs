using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehavior : MonoBehaviour
{
    public GameObject towerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Build(){
        Debug.Log("Building!");
        GameObject tower = Instantiate(towerPrefab);
        tower.transform.position = transform.position;
        tower.transform.rotation = transform.rotation;
        Destroy(this.gameObject);
    }
}
