using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject player;
    [SerializeField]
    float verticle,
        zoom;
    [SerializeField]
    public float rotateSpeed = 5;
    Vector3 offset;
    
    [SerializeField]
    Vector3 verticleOffset,
        horizontalOffset;
    
    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = player.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        verticle += Input.GetAxis("Mouse Y");
        zoom += Input.GetAxis("Mouse ScrollWheel");
        player.transform.Rotate(0, horizontal, 0);
        
        horizontalOffset.z = zoom;
        //Debug.Log(verticle);
        verticleOffset.y = verticle * 0.4f;
        float desiredAngle = player.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = player.transform.position - (rotation * offset);

        transform.LookAt(player.transform.position  + verticleOffset);
        transform.position = player.transform.position - (rotation * offset) + player.transform.right * 0.7f;
	}
}
