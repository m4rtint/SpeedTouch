using UnityEngine;
using System.Collections;

public class ScalingCamera : MonoBehaviour {

	public float orthographicSize = 10;
	public float aspect = 1.33333f;
	void Start()
	{
		Camera.main.projectionMatrix = Matrix4x4.Ortho(
			-orthographicSize * aspect, orthographicSize * aspect,
			-orthographicSize, orthographicSize,
			this.GetComponent<Camera>().nearClipPlane, this.GetComponent<Camera>().farClipPlane);
	}
}
