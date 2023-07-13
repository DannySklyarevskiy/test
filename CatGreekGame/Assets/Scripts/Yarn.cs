using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yarn : MonoBehaviour
{
    [SerializeField] private float friction;
    [SerializeField] private float cutoff;
    [SerializeField] private PhysicsMaterial2D mat;

    private PhysicsController pc;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pc = FindObjectOfType<PhysicsController>();
    }

    private void FixedUpdate() {
        pc.SimulateVeolicty(rb, friction, cutoff);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            rb.sharedMaterial = null;
            print("hi");
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        rb.sharedMaterial = mat;
    }
}
