using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	private Transform m_player;
	private Vector3 offset;
	public float speed = 20f;

    private void Start()
    {
        m_player = GameObject.FindWithTag(Tag.Player).transform;
        offset = transform.position - m_player.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, offset + m_player.position, speed * Time.deltaTime);
    }
}