using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCharge : MonoBehaviour
{
    // CONST
    private const float Ke = 8.988f;
    private const float MaxAbsForceMagnitude = 200;

    // PUBLIC FIELDS
    public float Q = 10;

    // LIFE CYCLE

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag is "Player")
        {
            var player = collision.gameObject.GetComponent<Player.PlayerController>();

            float distance = Vector2.Distance(transform.position, player.transform.position);
            float chargesProduct = Q * player.Q;

            float forceMagnitude = Mathf.Clamp(Ke * chargesProduct / Mathf.Pow(distance, 2), -MaxAbsForceMagnitude, MaxAbsForceMagnitude);
            Vector2 force = forceMagnitude * (player.transform.position - transform.position).normalized;

            player.AddForce(force);
        }
    }
}
