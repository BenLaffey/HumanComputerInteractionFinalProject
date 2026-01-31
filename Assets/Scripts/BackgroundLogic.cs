using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLogic : MonoBehaviour {

	public Sprite[] backgrounds;
	public int desktop;

	// Use this for initialization
	void Start () {
		desktop = 0;
		this.GetComponent<SpriteRenderer>().sprite = backgrounds[desktop];
	}

	public void SetBackground(int NUM)
    {
		this.GetComponent<SpriteRenderer>().sprite = backgrounds[NUM];
	}
}
