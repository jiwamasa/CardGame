using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// defines card behaviour
// card text is stored as child game objects
public class Card : MonoBehaviour {
	bool face_up; // is card face up?
	GameObject owner; // owner of the card (null if no owner)

	// flip the card (toggles face_up)
	public void Flip() {
		if (face_up) {
			// FLIP FACE DOWN	
		} else {
			// FLIP FACE UP
		}
	}

	// claims ownership of card
	public void Claim() {

	}

}
