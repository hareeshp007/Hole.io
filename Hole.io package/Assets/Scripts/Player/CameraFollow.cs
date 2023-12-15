using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float smoothness = 5f;
    [SerializeField]
    private Vector3 offsetPos;
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(
                player.position.x + offsetPos.x,
                transform.position.y, 
                player.position.z+ offsetPos.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        }
    }

    public void setPlayerTransform(Transform transform)
    {
        player = transform;
    }
}
