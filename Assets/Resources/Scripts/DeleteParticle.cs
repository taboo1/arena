﻿using UnityEngine;
using System.Collections;

public class DeleteParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(Delete());
	}
	
	IEnumerator Delete()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
