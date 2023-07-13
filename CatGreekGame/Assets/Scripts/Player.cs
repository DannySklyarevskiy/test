using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration, dashCD;

    private bool facingLeft = true;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    public float dashDurationTimer, dashCDTimer;

    private float xInput, yInput;
    private bool isMoving;

    private Rigidbody2D rb;
    private PhysicsController pc;
    private Vector2 moveVector;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pc = FindObjectOfType<PhysicsController>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.D)) {
            facingLeft = false;
            Turn();
        }
        if (Input.GetKey(KeyCode.A)) {
            facingLeft = true;
            Turn();
        }
        if (Input.GetKey(KeyCode.Space) && canDash) {
            isDashing = true;
            dashDurationTimer = dashDuration;
            dashCDTimer = dashCD;
        }

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        isMoving = (xInput != 0 || yInput != 0) && !isDashing;
    }

    void FixedUpdate(){
        if (isMoving) {
            Move();
        }

        if (isDashing) {
            Dash();
        }

        if (dashCDTimer > 0) {
            dashCDTimer -= Time.deltaTime;
            canDash = false;
        }
        else canDash = true;

        pc.SimulateVeolicty(rb, 0, 0);
    }

    private void Move() {
         moveVector = new Vector2(xInput, yInput).normalized;
         rb.MovePosition(new Vector2((transform.position.x + moveVector.x * speed * Time.deltaTime),
         transform.position.y + moveVector.y * speed * Time.deltaTime));
    }

    private void Dash() {
        rb.AddForce(moveVector * dashSpeed * 10000);
        if (dashDurationTimer > 0) {
            dashDurationTimer -= Time.deltaTime;
        }
        else isDashing = false;
    }

    private void Turn() {
        float playerDir = gameObject.transform.localScale.x;
        if (facingLeft && playerDir < 0 || !facingLeft && playerDir > 0) gameObject.transform.localScale = new Vector3(-playerDir, gameObject.transform.lossyScale.y, gameObject.transform.lossyScale.z);
    }
}
