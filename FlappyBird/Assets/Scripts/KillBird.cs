using UnityEngine;
using System.Collections;

public class KillBird : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D targetObject)
	{
		targetObject.gameObject.SetActive(false);
	}

}
