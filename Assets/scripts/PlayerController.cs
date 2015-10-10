﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	public bool canJump = false;

	public bool attack = false;	

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 100f;			// Amount of force added when the player jumps.

	public GameObject checkobject;

	public GameObject arm;
	public GameObject body;

	private bool dead;

	void OnDead() {
		body.GetComponent<Animator>().SetBool("dead", true);
		arm.GetComponent<Animator>().SetBool("dead", true);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
		dead = true;
	}

	void Update() {
		if (dead) {
			return;
		}

		// If the jump button is pressed and the player is grounded then the player should jump.
		if (Input.GetButtonDown ("Jump")) {
			jump = true;
		}

		Collider2D col = Physics2D.OverlapCircle (checkobject.transform.position, 0.01f);

		canJump = col != null;
		if (!canJump) {
			jump = false;
		}

		if (!canJump) {
			body.GetComponent<Animator> ().SetBool ("inair", true);
		} else {
			body.GetComponent<Animator> ().SetBool ("inair", false);
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			arm.GetComponent<Animator> ().SetBool ("attack", true);
		}
		
		if (Input.GetKeyUp (KeyCode.D)) {
			arm.GetComponent<Animator> ().SetBool ("attack", false);
		}	
	}
	void FixedUpdate () {
		if (dead) {
			return;
		}

		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		GetComponent<Rigidbody2D> ().velocity = new Vector2(5 * h, GetComponent<Rigidbody2D> ().velocity.y);

		body.GetComponent<Animator> ().SetBool ("running", h != 0);

		if (h > 0 && !facingRight) {
			Flip ();
		} else if (h < 0 && facingRight) {
			Flip();
		}


		if(jump && canJump) {
			// Add a vertical force to the player.
			GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, 0);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}

	}

	void Flip () {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}