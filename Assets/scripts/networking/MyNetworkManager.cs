using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// manages starting servers/clients accross the network
// hides Unity.Networking.NetworkManager
public class MyNetworkManager : MonoBehaviour {
	NetworkClient my_client; // local client

	// start server and listen for connections
	public void StartServer(int port) {
		NetworkServer.Listen (port);
	}

	// start a new client and connect to ip/port
	public void StartClient(string ip, int port) {
		my_client = new NetworkClient ();
		my_client.RegisterHandler(MsgType.Connect, OnConnected);     
		my_client.Connect("127.0.0.1", 4444);
	}

	// Create a local client and connect to the local server
	public void StartLocalClient()
	{
		my_client = ClientScene.ConnectLocalServer();
		my_client.RegisterHandler(MsgType.Connect, OnConnected);
	}

/*---------------------------------------------------------------------*/
	// custom handlers

	// invoked on connection
	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
	}

/*---------------------------------------------------------------------*/
	// getters/setters

	// get local client
	public NetworkClient GetClient() {
		return my_client;
	}

}
