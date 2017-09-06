using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// sets network spawned objects to the canvas
public class SetToCanvas : NetworkBehaviour {

	public override void OnStartClient ()
	{
		Debug.Log ("Start Local Player");
		transform.SetParent (GameObject.Find("Canvas").transform);
		transform.localScale = Vector3.one;
	}
}
