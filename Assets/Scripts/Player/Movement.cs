using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float speed = 5f;
	public float jump = 5f;
	public float jumpDelay = 0.5f;
	public float groundCheckDist = 0.1f;
	public float fallMult = 2f;
	public float lowJumpMult = 2f;
	public bool pauseMovement = false;
	private Collider2D plyrCol;
	private Rigidbody2D plyrRb;
	private Collider2D jumpOffCollider;
	private float jumpTimer = -1f;

	// Start is called before the first frame update
	private void Awake() {
		plyrCol = GetComponent<Collider2D>();
		plyrRb = GetComponent<Rigidbody2D>();
	}

	public Collider2D PlayerOnGround(){ // Returns the collider, upon which the player is standing
		Vector3 plyrFeetPos = new Vector3(transform.position.x, transform.position.y - plyrCol.bounds.extents.y, transform.position.z);
		RaycastHit2D hit = Physics2D.Linecast(plyrFeetPos, plyrFeetPos - new Vector3(0, groundCheckDist, 0), ~LayerMask.GetMask("NonJumpable"));

		if(hit.collider == jumpOffCollider) return null;
		if(hit) return hit.collider;
		
		return null;
	}

	void Jump(){
		Collider2D gndCol = PlayerOnGround();

		if(gndCol == null) return;

		plyrRb.velocity += new Vector2(0, jump);
		jumpOffCollider = gndCol;
		jumpTimer = jumpDelay;
	}

	// Update is called once per frame
	void Update()
	{
		if(pauseMovement) return;

		float horizInput = Input.GetAxis("Horizontal");

		if(horizInput != 0){
			transform.position += new Vector3(horizInput * speed * Time.deltaTime, 0, 0);
		}

		if(Input.GetButton("Jump") && jumpTimer == -1){
			Jump();
		}

		if(plyrRb.velocity.y < 0){
			plyrRb.velocity += Vector2.up * Physics2D.gravity.y * (fallMult - 1) * Time.deltaTime;
		}
		else if(plyrRb.velocity.y > 0 && !Input.GetButton("Jump")){
			plyrRb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
		}

		if(jumpTimer > 0){
			jumpTimer -= Time.deltaTime;
		}
		else if(jumpTimer <= 0){
			jumpTimer = -1;
			jumpOffCollider = null;
		}
	}
}
