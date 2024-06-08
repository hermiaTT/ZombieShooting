using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{

    //health bar follow palyer
    [SerializeField]
    private GameObject healthBarFollow;
    private Transform targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        targetPosition = healthBarFollow.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
     
        transform.position = healthBarFollow.transform.position;
        
    }
}
