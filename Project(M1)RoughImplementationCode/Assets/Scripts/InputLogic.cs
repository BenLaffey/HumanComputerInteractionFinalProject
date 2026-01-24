using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class InputLogic : MonoBehaviour {
	
	public Text infoA;
	public Text infoB;

	public float PointerX;
	public float PointerY;
	public bool BPressed;

	public Sprite[] icons;

	WiiU.Remote Remote1;

	Camera camera;

	// Use this for initialization
	void Start()
	{
		Remote1 = WiiU.Remote.Access(0);
		camera = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		WiiU.RemoteState CurrentRemote1State = Remote1.state;
		
		Vector3 mousepos = Input.mousePosition;
		Vector3 mouseworldpos = camera.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, camera.nearClipPlane));
		PointerX = mouseworldpos.x;
		PointerY = mouseworldpos.y;

		//float XPoint = CurrentRemote1State.pos.x * 1920f / 3f + 1920f/2f;
		//float YPoint = -CurrentRemote1State.pos.y * 1080f / 3f + 1080f/2f;
		//bool BPress = false;

		//PointerX = -8.9f + (8.9f * ((800f + (CurrentRemote1State.pos.x * 1920f / 3f + 1920f / 2f)) / 1700f));
		//PointerX = (8.9f / 2.4f) * CurrentRemote1State.pos.x;

		//PointerY = 5f - (5f * ((200f + (-CurrentRemote1State.pos.y * 1080f / 3f + 1080f / 2f)) / 600f));
		//PointerY = (-5f / 1.45f) * ((CurrentRemote1State.pos.y + 1f) - 1.45f);

		BPressed = false;

		if (CurrentRemote1State.IsPressed(WiiU.RemoteButton.B))
		{
			BPressed = true;
		}

		if (Input.GetMouseButton(0))
		{
			BPressed = true;
		}

		//infoA.text = "Remote Aiming Mode Enabled: " + Remote1.aimingModeEnabled; //True
		//infoB.text = "Remote Sensor Height: " + Remote1.sensorHeight; //0.2
		//infoA.text = "Pointer X: " + XPoint + " Pointer Y: " + YPoint;

		infoA.text = "Pointer X: " + PointerX + " Pointer Y: " + PointerY;
		//infoA.text = "Pointer X: " + CurrentRemote1State.pos.x + " Pointer Y: " + CurrentRemote1State.pos.y;
		infoB.text = "B Pressed: " + BPressed;
	}
}
