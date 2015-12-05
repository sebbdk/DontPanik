﻿using UnityEngine;
using System.Collections;

public class ShadowMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().shadowCastingMode =  UnityEngine.Rendering.ShadowCastingMode.On;
		GetComponent<Renderer>().receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
