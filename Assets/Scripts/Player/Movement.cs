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
	public float movementSmoothing = 0.5f;
	private Collider2D plyrCol;
	private Rigidbody2D plyrRb;
	private Collider2D jumpOffCollider;
	private float jumpTimer = -1f;
	private Vector2 curMovementVelocity = new Vector2(0, 0);
	private Animator animator;
	private bool isMovingCheck = false;

	// Start is called before the first frame update
	private void Awake() {
		plyrCol = GetComponent<Collider2D>();
		plyrRb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	public Collider2D PlayerOnGround(){ // Returns the collider, upon which the player is standing
		Vector2 plyrFeetPos = new Vector3(transform.position.x, transform.position.y - plyrCol.bounds.extents.y);
		Vector2 boxSize = new Vector2(plyrCol.bounds.extents.x, groundCheckDist);
		RaycastHit2D hit = Physics2D.BoxCast(plyrFeetPos, boxSize, 0, -transform.up, groundCheckDist, ~LayerMask.GetMask("NonJumpable"));

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

		animator.SetTrigger("Jump");
	}

	// Update is called once per frame
	void Update()
	{
		float horizInput = Input.GetAxis("Horizontal");

		if(horizInput != 0 && !pauseMovement){
			Vector2 vel = new Vector2();
			Vector2 targetVelocity = new Vector2(speed * horizInput, plyrRb.velocity.y);
			plyrRb.velocity = Vector2.SmoothDamp(plyrRb.velocity, targetVelocity, ref vel, movementSmoothing);

			if(!isMovingCheck){
				isMovingCheck = true;

				// animator.SetBool("MoveRight", horizInput > 0);
				// animator.SetBool("IsMoving", true);
			}
		}
		else if(isMovingCheck){
			// animator.SetBool("IsMoving", false);
			isMovingCheck = false;
		}

		if(Input.GetButton("Jump") && jumpTimer == -1 && !pauseMovement){
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
