using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float MINX = -12f;
    [SerializeField] private float MAXX = 19f;
    [SerializeField] private float MINY = -14f;
    [SerializeField] private float MAXY = 21f;
    [SerializeField] public GameObject player;
    [SerializeField] [Range(-10, 10)] public float camerax;
    [SerializeField] [Range(-10, 10)] public float cameray;
    [SerializeField] [Range(-10, 10)] public float cameraz;

    bool isInWidthScenario()
    {
        return player.transform.position.x > MINX && player.transform.position.x < MAXX;
    }

    bool isInHeightScenario()
    {
        return player.transform.position.y < MAXY && player.transform.position.y > MINY;
    }
    void LateUpdate()
    {
        Vector3 pos = player.transform.position;
        pos.x += camerax;
        pos.y += cameray;
        pos.z += cameraz;
        if (isInWidthScenario() && isInHeightScenario())
        {
            transform.position = pos;
        }
        else if (!isInWidthScenario() && !isInHeightScenario())
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, pos.z);
        }
        else if (!isInWidthScenario())
        {
            transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);
        }
        else if (!isInHeightScenario())
        {
            transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
        }
    }
}