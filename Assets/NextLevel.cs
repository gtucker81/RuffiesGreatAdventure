using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {
	[SerializeField] private string loadLevel;
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Player")
		{
			SceneManager.LoadScene(loadLevel);
		}
	}
}
