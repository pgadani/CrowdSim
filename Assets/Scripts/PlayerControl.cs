using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	Animator animator;
	CapsuleCollider capsule;
	public float capsuleHeight;
	public float jumpHeight;
	public float jumpDistance; // adjusted based on speed
	private static float walkSpeed = 1.556f;
	private static float runSpeed = 5.668f;

	void Start() {
		animator = GetComponent<Animator>();
		capsule = GetComponent<CapsuleCollider>();
	}
	
	void Update() {
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
		if (Input.GetKeyDown(KeyCode.Space)) {
			animator.SetTrigger("Jump");
		}
		else if (Input.GetKeyUp(KeyCode.Space)) {
			animator.ResetTrigger("Jump");
		}
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			v*=runSpeed;
		}
		else {
			v*=walkSpeed;
		}
		animator.SetFloat("Speed", v);
		animator.SetFloat("Direction", h);
	}

	void FixedUpdate() {
		AnimatorStateInfo currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		if (currentBaseState.IsName("Base.LocomotionJump")) {
			transform.Translate(Vector3.up*animator.GetFloat("JumpCurve")*jumpHeight);
			capsule.height = capsuleHeight + animator.GetFloat("CapsuleCurve") * 2.0f;
			transform.Translate(transform.forward*Time.deltaTime*jumpDistance*animator.GetFloat("Speed")*0.5f); //does this even do things
		}
		else if (currentBaseState.IsName("Base.IdleJump")) {
			transform.Translate(Vector3.up*animator.GetFloat("JumpCurve")*jumpHeight);
			capsule.height = capsuleHeight + animator.GetFloat("CapsuleCurve") * 0.7f;
		}

	}
}