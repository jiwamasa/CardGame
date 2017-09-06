using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// defines context menu behaviour
// attach to object that needs a context menu
public class ContextMenu : MonoBehaviour {
	public GameObject menu_prefab; // base object for context menu background
	public GameObject option_prefab; // base object for a single option button
	public float option_width; // width of menu
	public float option_height; // height of a single option
	public int option_text_size; // size of option text
	public UnityEvent[] options; // list of option function handles
	public string[] option_names; // list of the option names
	GameObject curr_menu; // currently spawned menu

	void Start () {
		// set defaults if none set
		if (option_width == 0) option_width = 80;
		if (option_height == 0) option_height = 20;
		if (option_text_size == 0) option_text_size = 16;
		// set right click event trigger
		SetRightClick();
	}

	// spawns context menu (should be called on right click)
	public void SpawnMenu() {
		if (options == null || options.Length == 0) return; // ignore empty lists
		Transform canvas_transform = GameObject.Find("Canvas").transform; // transform of the canvas
		// create menu background
		GameObject menu = Instantiate(menu_prefab, canvas_transform); 
		// set position and size of menu
		RectTransform menu_rect = menu.GetComponent<RectTransform> ();
		menu_rect.anchoredPosition3D = canvas_transform.transform.InverseTransformPoint(GetMousePosition ()); 
		menu_rect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, option_width);
		menu_rect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, option_height * options.Length);
		for (int option_ind = 0; option_ind < options.Length; option_ind++) {
			// add a new option to the menu
			SpawnOption (menu.transform, new Vector3 (0, -1 * option_height * option_ind, 0), 
				option_names [option_ind], options [option_ind].Invoke);
		}
		curr_menu = menu; // set current menu
	}

	// spawns an option as the child of given menu with given parameters
	public void SpawnOption(Transform menu, Vector3 new_pos, string new_text, UnityAction new_action) {
		// create a new option button
		GameObject option = Instantiate (option_prefab, menu.transform);
		// set position and size of option button
		RectTransform button_rect = option.GetComponent<RectTransform> ();
		button_rect.anchoredPosition3D = new_pos;
		button_rect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, option_width);
		button_rect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, option_height);
		// set text and text size
		Text button_text = option.GetComponentInChildren<Text>();
		button_text.fontSize = option_text_size;
		button_text.text = new_text;
		// set functionality of option
		Button option_button = option.GetComponent<Button> ();
		option_button.onClick.AddListener (new_action.Invoke);
		option_button.onClick.AddListener (DestroyOldMenu);
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

	// this handle is called on right click
	// menu is spawned on right click
	public void OnRightClick(PointerEventData data) {
		if (data.button == PointerEventData.InputButton.Right) { // check for right click
			DestroyOldMenu();
			SpawnMenu ();
		}
	}

	// destroy old menu
	public void DestroyOldMenu() {
		if (curr_menu) GameObject.Destroy (curr_menu);
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
