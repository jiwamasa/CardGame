using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// allows object to be dragged
public class Draggable : NetworkBehaviour {
	Dragger dragger; // local player's dragger component (for sending commands to server)
	Vector3 camera_offset; // distance between camera and board
	Vector3 mouse_offset; // distance between mouse and position of object

	void Start() {
		// find the local player's dragger component
		dragger = GetComponent<NetworkUtility>().GetLocalPlayer().GetComponent<Dragger>();
		// find distance between camera and board
		camera_offset = new Vector3(0,0,GameObject.Find ("Main Camera").transform.position.z * -1);
	}

	// calculate distance vector bewteen mouse and position of object (called when dragging starts)
	public void SetMouseOffset() {
		// get world position of mouse
		Vector3 mouse_position = Camera.main.ScreenToWorldPoint (Input.mousePosition) + camera_offset; 
		mouse_offset = transform.position - mouse_position; // calculate offset
	}

	// allow local player to drag object
	public void FollowMouse() {
		// get world position of mouse
		Vector3 mouse_position = Camera.main.ScreenToWorldPoint (Input.mousePosition) + camera_offset;
		// send command to server to drag object
		dragger.CmdDragObject(gameObject, mouse_position + mouse_offset);
	}

}
