using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    public Transform player;

    private Vector3 offset;


   
    void Start()
    {
        offset = transform.position - player.transform.position;

    }


    private void LateUpdate()
    {
        transform.position = new Vector3(0, player.position.y + offset.y, player.position.z + offset.z);
    }
}
