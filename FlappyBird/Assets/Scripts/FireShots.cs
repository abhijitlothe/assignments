using UnityEngine;
using System.Collections;

public class FireShots : MonoBehaviour 
{
	public GameObject Projectile;
	private GameObject _projectileInstance;

	void OnTriggerEnter2D(Collider2D targetObject)
	{
		ShootProjectiles();
	}

	public void ShootProjectiles()
	{
		if(_projectileInstance != null)
			return;
		int sign = transform.position.y > 0.5f ? -1 : 1;
		float myHeight = GetComponent<SpriteRenderer>().bounds.size.y;
		_projectileInstance = Instantiate(Projectile) as GameObject;
		_projectileInstance.transform.parent = this.transform;
		_projectileInstance.transform.position = new Vector3(transform.position.x, transform.position.y + sign * myHeight/2.0f, transform.position.z);
	}
}
