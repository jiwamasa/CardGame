using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

// allows players (namely remote players) to drag objects
public class Dragger : NetworkBehaviour {
	// send command to server to drag object to new_pos
	[Command]
	public void CmdDragObject(GameObject obj, Vector3 new_pos) {
		obj.transform.position = new_pos;
	}
			
}
