using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// defines context menu behaviour
// attach to object that needs a context menu
public class ContextMenu : MonoBehaviour {
	public UnityAction[] options; // list of option handles
	public GameObject menu_prefab; // base object for context menu background
	public GameObject option_prefab; // base object for a single option button
	public float option_width; // default width of menu
	public float option_height; // default height of a single option

	void Start () {
		// set defaults if none set
		if (option_width == 0) option_width = 80;
		if (option_height == 0) option_height = 20;
		// set right click event trigger
		SetRightClick();
	}

	// spawns context menu (should be called on right click)
	public void SpawnMenu() {
		if (options == null || options.Length == 0) return; // ignore empty lists
		// create menu background
		GameObject menu = Instantiate(menu_prefab, GameObject.Find("Canvas").transform); 
		// set position and size of menu
		RectTransform menu_rect = menu.GetComponent<RectTransform> ();
		menu_rect.anchoredPosition3D = GetMousePosition (); 
		menu_rect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, option_width);
		menu_rect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, option_height * options.Length);
		for (int option_ind = 0; option_ind < options.Length; option_ind++) {
			GameObject option = Instantiate (option_prefab, menu.transform);
			// set position and size of option
			RectTransform button_rect = option.GetComponent<RectTransform> ();
			Vector3 button_pos = new Vector3 (0, option_height * option_ind, 0);
			button_rect.anchoredPosition3D = button_pos;
			button_rect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, option_width);
			button_rect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, option_height);
			// set functionality of option
			Button option_button = option.GetComponent<Button> ();
			option_button.onClick.AddListener (options [option_ind]);
		}
	}
		
	// set event trigger to check if right mouse has been clicked
	// see EventTrigger documentation in Unity Scripting API
	void SetRightClick() {
		EventTrigger trigger = GetComponent<EventTrigger> ();
		EventTrigger.Entry entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener ((data) => {
			OnRightClick ((PointerEventData)data);
		});
		trigger.triggers.Add (entry);
	}

	// handle is called on right click
	public void OnRightClick(PointerEventData data) {
		if (data.button == PointerEventData.InputButton.Right) {
			SpawnMenu ();
		}
	}

	// returns mouse position in world
	Vector3 GetMousePosition() {
		// get distance of camera from world
		Vector3 camera_offset = new Vector3(0,0,GameObject.Find ("Main Camera").transform.position.z * -1);
		// convert screen pixel position to world position
		Vector3 mouse_position = Camera.main.ScreenToWorldPoint (Input.mousePosition) + camera_offset; 
		return mouse_position;
	}
}
