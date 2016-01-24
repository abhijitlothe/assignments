using UnityEngine;
using System.Collections;

public class DangerZone : MonoBehaviour 
{
	public System.Action<Collider2D> OnTriggerEnter;


	public void SetColliderRadius(float radius)
	{
		this.GetComponent<CircleCollider2D>().radius = radius;
	}

	void OnTriggerEnter2D(Collider2D targetObject)
	{
		if(targetObject.CompareTag("Bird") && OnTriggerEnter != null)
			OnTriggerEnter(targetObject);
	}
}
