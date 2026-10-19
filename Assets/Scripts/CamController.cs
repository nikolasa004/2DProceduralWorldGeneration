using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float moveSpeed;
    [Range(0f, 1f)]
    public float smoothTime;
    public Transform playerTransform;
    [HideInInspector]
    public int worldSize;
    private float camsize;
    public void Spawn(Vector3 pos)
    {
        GetComponent<Transform>().position = pos;
        camsize = GetComponent<Camera>().orthographicSize;
    }
    private void FixedUpdate()
    {
        Vector3 pos=GetComponent<Transform>().position;
        pos.x =Mathf.Lerp(pos.x,playerTransform.position.x,smoothTime);
        pos.y = Mathf.Lerp(pos.y, playerTransform.position.y, smoothTime);
        pos.x=Mathf.Clamp(pos.x, 0 + camsize*2f, worldSize - camsize*2f);

        GetComponent<Transform>().position=pos;
    }
}
