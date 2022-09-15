using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesMovement : MonoBehaviour
{
	private Rigidbody rb;

	/// <summary>
	/// Velocity of the boss
	/// </summary>
	public float velocity = 1f;

	/// <summary>
	/// playerTransform
	/// </summary>
	public Transform playerTransform;

	public float damping;

	private bool isMoving = true;

	private bool isAtacking = false;

	public GameObject door;

	private Animator animator;

	private BoxCollider closeCol;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		closeCol = GetComponent<BoxCollider>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (isMoving)
		{
			var rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
			rotation.x = 0;
			rotation.z = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
			Vector3 vectorToPlayer = playerTransform.position - transform.position;
			rb.velocity = vectorToPlayer.normalized * velocity;
		}
		else
		{
			StartCoroutine(Attacking());
		}


	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "MainCamera" && door.activeInHierarchy == true)
		{
			isMoving = false;
			isAtacking = true;
		}
	}



	IEnumerator Attacking()
	{
		animator.Play("Attack");
		yield return new WaitForSeconds(2.5f);
		animator.Play("Idle01");
		isMoving = true;
		isAtacking = false;
	}

}
