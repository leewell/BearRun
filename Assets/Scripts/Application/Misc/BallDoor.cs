using System;
using UnityEngine;

public class BallDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Ball)
        {
            other.transform.parent.parent.SendMessage("HitBallDoor",SendMessageOptions.RequireReceiver);
            gameObject.transform.parent.parent.SendMessage("ShootAGoal", (int)other.transform.position.x);
        }
    }
}