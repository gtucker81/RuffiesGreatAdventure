using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.


	private Transform player;		// Reference to the PlayerControl script.
	//private Animator anim;					// Reference to the Animator component.
	public Enemy owner;

	public float shotCooldown = 1f;
	public float nextShotTime = 1f;

	void Awake()
	{
		// Setting up the references.
		//anim = transform.root.gameObject.GetComponent<Animator>();
		player = player = GameObject.FindGameObjectWithTag("Player").transform;
		owner = GetComponent<Enemy> ();
	}


	void Update ()
	{
		if (owner.dead == false) {
			// If the fire button is pressed...
			if (Time.time > nextShotTime) {
				// ... instantiate the rocket facing the player. 
				Vector3 direction = Vector3.Normalize (player.position - transform.position);
				direction.z = 0;
				Rigidbody2D bulletInstance = Instantiate (rocket, transform.position, Quaternion.FromToRotation (Vector3.zero, direction)) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (direction.x, direction.y).normalized * speed;

				Debug.Log ("Pew");

				nextShotTime = Time.time + shotCooldown;
			}
		}
	}
}
