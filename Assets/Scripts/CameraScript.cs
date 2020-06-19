using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Player Player;
    private Vector3 offset;
    void Start()
    {
        Player = FindObjectOfType<Player>();
        offset = transform.position - Player.transform.position;
    }

    void LateUpdate()
    {
        //transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + offset.y, Player.transform.position.z);
        transform.position = Player.transform.position + offset;
    }
}
