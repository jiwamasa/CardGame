using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for debugging
public class DebugUtility : MonoBehaviour {
	public void Ping() {
		Debug.Log ("ping!");
	}

	public void Echo(string echo_string) {
		Debug.Log (echo_string);
	}
}
