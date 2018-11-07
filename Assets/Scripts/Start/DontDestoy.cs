using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoy : MonoBehaviour {

	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
}
