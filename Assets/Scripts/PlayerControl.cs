using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	Animator animator;

	void Start () {
		animator = GetComponent<Animator>();	
	}
	
	void Update () {
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
		if (Input.GetKeyDown(KeyCode.Space)) {
			animator.SetTrigger("Jump");
		}
		else if (Input.GetKeyUp(KeyCode.Space)) {
			animator.ResetTrigger("Jump");
		}
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			v*=2;
		}
		animator.SetFloat("Speed", v);
		animator.SetFloat("Direction", h);
		animator.SetFloat("Angle", 180*h);

	}
}