using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Vector3 pos1, pos2;
    public float speed = 1.0f;

    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}