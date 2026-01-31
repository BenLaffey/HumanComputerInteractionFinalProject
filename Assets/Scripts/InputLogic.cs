using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class InputLogic : MonoBehaviour {
	
	public Text clock;
	public Text picture;
	public Text calculator;

	public float PointerX;
	public float PointerY;
	public bool BPressed;

	public Sprite[] icons;
	public Sprite[] pictures;
	public Sprite[] digits;

	WiiU.Remote Remote1;

	//Camera camera;

	// Use this for initialization
	void Start()
	{
		//camera = Camera.main;
		Remote1 = WiiU.Remote.Access(0);
		picture.text = "";
		calculator.text = "";
	}

	// Update is called once per frame
	void Update()
	{
		WiiU.RemoteState CurrentRemote1State = Remote1.state;
		
		//Vector3 mousepos = Input.mousePosition;
		//Vector3 mouseworldpos = camera.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, camera.nearClipPlane));
		//PointerX = mouseworldpos.x;
		//PointerY = mouseworldpos.y;

		PointerX = (8.9f / 2.4f) * CurrentRemote1State.pos.x;
		PointerY = (-5f / 1.45f) * ((CurrentRemote1State.pos.y + 1f) - 1.45f);

		BPressed = false;

		//if (Input.GetMouseButton(0))
		//{
		//	BPressed = true;
		//}

		if (CurrentRemote1State.IsPressed(WiiU.RemoteButton.B))
		{
			BPressed = true;
		}

		clock.text = System.DateTime.UtcNow.ToLocalTime().ToString("h:mm tt  M/d/yyyy");
	}

	public void SetPictureText(int num)
    {
		picture.text = num + " / " + pictures.Length;
	}
}
