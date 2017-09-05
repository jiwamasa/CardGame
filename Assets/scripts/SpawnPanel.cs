using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

// spawn panel allows for spawning new cards/tokens/etc
public class SpawnPanel : NetworkBehaviour {
	public List<GameObject> prefab_list; // list of spawnable prefabs
	Dropdown dropdown; // dropdown menu

	void Start() {
		dropdown = GetComponentInChildren<Dropdown> (); // get dropdown component
		List<string> dropdown_names = new List<string>(); // list of names for dropdown options 
		foreach (GameObject prefab in prefab_list) // get names from prefab list
			dropdown_names.Add(prefab.name);
		dropdown.AddOptions (dropdown_names); // set names in dropdown options
	}

	// spawn currently selected item in dropdown
	public void SpawnItem() {
		CmdSpawnItem (prefab_list [dropdown.value]); // spawn instance on server
	}

	// sends a command to server to spawn the item
	[Command]
	void CmdSpawnItem(GameObject curr_prefab) {
		GameObject new_item = Instantiate (curr_prefab);
		new_item.transform.SetParent (GameObject.Find("Canvas").transform);
		new_item.transform.position = Vector3.zero;
		new_item.transform.localScale = Vector3.one;
		NetworkServer.Spawn (new_item);
	}

}
