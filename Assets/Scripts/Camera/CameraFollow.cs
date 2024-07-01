using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    private Vector3 followPsition = Vector3.zero;
    public float followSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void LateUpdate()
    {
        followPsition = player.gameObject.transform.position;
        followPsition.z = -10;

        //Æ½»¬¸úËæ
        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, followPsition, Time.deltaTime * followSpeed);
    }
 
}
