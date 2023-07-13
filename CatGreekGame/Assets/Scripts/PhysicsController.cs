using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    public void SimulateVeolicty(Rigidbody2D rb, float slowDown, float cutoff) {
        if (Mathf.Abs(rb.velocity.x) > cutoff) rb.velocity = new Vector2(rb.velocity.x * slowDown, rb.velocity.y);
        if (Mathf.Abs(rb.velocity.y) > cutoff) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * slowDown);
    }
}
