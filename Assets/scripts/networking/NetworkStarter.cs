using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkStarter : MonoBehaviour {
	NetworkManager network_manager;
	InputField address_input;
	string network_address;
	InputField port_input;
	int network_port;

	void Start() {
		network_manager = GameObject.Find ("NetworkManager").GetComponent<NetworkManager> ();
		address_input = GameObject.Find ("AddressInput").GetComponent<InputField> ();
		port_input = GameObject.Find ("PortInput").GetComponent<InputField> ();
	}

	public void StartHost() {
		network_manager.StartHost ();
		/* MyNetworkManager implementation
		network_manager.StartServer (network_port);
		network_manager.StartLocalClient ();
		*/
	}

	public void StartClient() {
		network_manager.StartClient ();
		/* MyNetworkManager implementation
		network_manager.StartClient (network_address, network_port);
		*/
	}

	public void SetAddress() {
		network_manager.networkAddress = address_input.text;
		/* MyNetworkManager implementation
		network_address = address_input.text;
		*/
	}

	public void SetPort() {
		int in_port;
		if (int.TryParse (port_input.text, out in_port))
			network_manager.networkPort = in_port;
		/* MyNetworkManager implementation
		int in_port;
		if (int.TryParse (port_input.text, out in_port))
			network_port = in_port;
		*/
	}
}
