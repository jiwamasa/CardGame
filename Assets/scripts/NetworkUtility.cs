using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// various utility functions for navigating network
public class NetworkUtility : NetworkBehaviour {
	NetworkManager network_manager;
	GameObject local_player;

	void Start() {
		network_manager = GameObject.Find ("NetworkManager").GetComponent<NetworkManager> ();
		local_player = network_manager.client.connection.playerControllers [0].gameObject;
	}

	// retrieve local player object
	public GameObject GetLocalPlayer() {
		return local_player;
	}
	
}
