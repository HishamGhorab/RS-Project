using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float smoothTime = 0.3f;
    
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
 
    void Start () 
    {
        offset = transform.position - player.transform.position;
    }
    
    void LateUpdate () 
    {
        transform.position = Vector3.SmoothDamp (transform.position, player.transform.position + offset, ref velocity, smoothTime);
    }
}
