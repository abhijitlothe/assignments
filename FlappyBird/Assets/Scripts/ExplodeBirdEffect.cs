using UnityEngine;
using System.Collections;

public class ExplodeBirdEffect : MonoBehaviour 
{
	public GameObject KillEffect;

	void Start()
	{

	}

	public void Apply(GameObject targetObject)
	{
		GameObject killEffectInstance = Instantiate<GameObject>(KillEffect);
		killEffectInstance.transform.position = targetObject.transform.position;
		StartCoroutine(AnimateAlpha(killEffectInstance));
		targetObject.SetActive(false);
	}


	IEnumerator AnimateAlpha(GameObject gameObject)
	{
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		WaitForSeconds delay = new WaitForSeconds(0.1f);
		if(renderer != null)
		{
			Color c;
			while(renderer.color.a > 0.0f)
			{
				c = renderer.color;
				c.a -= 0.05f;
				renderer.color = c;
				yield return delay;
			}
		}

		yield break;
	}
}
