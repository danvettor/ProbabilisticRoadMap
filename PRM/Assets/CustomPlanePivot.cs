using UnityEngine;
using System.Collections;

public class CustomPlanePivot : MonoBehaviour {

	private Vector3 adjustedInitialPosition;
	private float realScale;

	void Start () 
	{
		realScale = (transform.localScale.x * 10.0f)/2;
		transform.position = new Vector3 (adjustedInitialPosition.x + realScale, 0.0f, adjustedInitialPosition.z + realScale);
	}

}
