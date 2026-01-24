using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionLogic : MonoBehaviour {

	private GameObject inputlistener;

	public int OptionID;
	//public float XPos;
	//public float YPos;
	//public float XScale;
	//public float YScale;
	public int SelectionState;
	public bool MakeOptionsWhenSelect;

	// Use this for initialization
	void Start () {
		inputlistener = GameObject.Find("InputListener");
	}
	
	// Update is called once per frame
	void Update () {
		if (SelectionState == 2 && inputlistener.GetComponent<InputLogic>().BPressed == false)
        {
			if (MakeOptionsWhenSelect == true)
            {
				SelectionState = 3;
            }
			else
            {
				SelectionState = 4;
            }
        }
		if (SelectionState < 3)
        {
			SelectionState = 0;
			if ((inputlistener.GetComponent<InputLogic>().PointerX < (transform.position.x + (0.64f * transform.localScale.x))) && (inputlistener.GetComponent<InputLogic>().PointerX > (transform.position.x - (0.64f * transform.localScale.x))) && (inputlistener.GetComponent<InputLogic>().PointerY < (transform.position.y + (0.64f * transform.localScale.y))) && (inputlistener.GetComponent<InputLogic>().PointerY > (transform.position.y - (0.64f * transform.localScale.y))))
            {
				SelectionState++;
				if (inputlistener.GetComponent<InputLogic>().BPressed == true)
				{
					SelectionState++;
				}
			}
        }
	}
}
