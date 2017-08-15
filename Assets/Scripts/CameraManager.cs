using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [SerializeField] private Camera camera;
    [SerializeField] private float horizontalSize;

	// Use this for initialization
	void Start () {
        camera.orthographicSize = horizontalSize / camera.aspect;
    }

    private void OnDrawGizmosSelected() {
        
        Gizmos.DrawWireCube(transform.position, new Vector2(horizontalSize, horizontalSize / camera.aspect)*2);
    }
}
