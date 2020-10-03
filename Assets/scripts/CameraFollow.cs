using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 target;

	public float smoothSpeed = 0.125f;
	
	void FixedUpdate()
	{
        Vector3 desiredPosition = target;
		Vector3 smoothedPostition = Vector3.Lerp (transform.position, desiredPosition,smoothSpeed);
		transform.position = smoothedPostition;
	}
}
