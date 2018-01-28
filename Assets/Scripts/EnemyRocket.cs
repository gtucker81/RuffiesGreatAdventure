using UnityEngine;
using System.Collections;

public class EnemyRocket : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.

	public float lifetime = 2f;

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, lifetime);
	}


	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.gameObject.tag == "Player") {
			// Instantiate the explosion and destroy the rocket.
			col.GetComponent<DogHealth>().TakeDamage(transform);
			OnExplode();
			Destroy (gameObject);
		} else if (col.gameObject.tag == "Enemy") {
			// Ignore enemies
		} else {
			// Destroy itself if it hits anything else.
			OnExplode();
			Destroy (gameObject);
		}
	}
}
