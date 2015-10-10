using UnityEngine;
using System.Collections;

public class Parralax : MonoBehaviour {

	public GameObject player;

	// Update is called once per frame
	void FixedUpdate  () {
		if (player) {

			Vector3 b = player.transform.position;
			b.y = 0;
			b.x = b.x / 8;

			Vector3 p = player.transform.position;
			p.y = -32 - p.y / 4;

			transform.position = p - b;
		}
	}

}
