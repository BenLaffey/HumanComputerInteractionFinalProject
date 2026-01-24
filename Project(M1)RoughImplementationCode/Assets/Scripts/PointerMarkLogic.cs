using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMarkLogic : MonoBehaviour {

	private GameObject inputlistener;
	private GameObject outline;
	private List<GameObject> options = new List<GameObject>(); //each option game object has its own visual id (same as option id), position, scale, selection scale (maybe not), and current input status data
								  //pointer will have each case for option object when it is clicked

	public GameObject newoption;

	//private int Current;

	void MakeOption(float XP, float YP, float XS, float YS, int INUM, int ID, bool MOPTION)
    {
		GameObject addedoption;
		addedoption = Instantiate(newoption, new Vector3(XP, YP, 0), Quaternion.identity);
		addedoption.transform.localScale = new Vector3(XS, YS, 1);
		addedoption.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().icons[INUM];
		addedoption.GetComponent<OptionLogic>().OptionID = ID;
		addedoption.GetComponent<OptionLogic>().MakeOptionsWhenSelect = MOPTION;

		options.Add(addedoption);
	}

	// Use this for initialization
	void Start () {
		inputlistener = GameObject.Find("InputListener");
		outline = GameObject.Find("Outline");

		MakeOption(-5, 0, 1, 1, 1, 1, true);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(inputlistener.GetComponent<InputLogic>().PointerX, inputlistener.GetComponent<InputLogic>().PointerY, 0);
		//transform.position.x;// = inputlistener.GetComponent<TestScript>().PointerX;
		//inputlistener.GetComponent<TestScript>().PointerX;
		if (inputlistener.GetComponent<InputLogic>().BPressed == true)
        {
			transform.localScale = new Vector3(0.7f, 0.7f, 1);
		}
		else
        {
			transform.localScale = new Vector3(1, 1, 1);
		}
		bool OptionActive = false;
		int Current = 0;
		
		while (Current < options.Count)
        {
			if (options[Current].GetComponent<OptionLogic>().SelectionState > 0)
            {
				outline.transform.position = new Vector3(options[Current].transform.position.x, options[Current].transform.position.y, 0);
				outline.transform.localScale = new Vector3((0.98f * options[Current].transform.localScale.x), (0.98f * options[Current].transform.localScale.y), 1);
				if (options[Current].GetComponent<OptionLogic>().SelectionState > 2)
                {
					OptionActive = true;
                }
				break;
			}
			else
            {
				outline.transform.position = new Vector3(0, -7, 0);
				outline.transform.localScale = new Vector3(0.98f, 0.98f, 1);
            }
			Current++;
		}

		if (OptionActive == true)
        {
			if (options[Current].GetComponent<OptionLogic>().SelectionState == 3)
            {
				SwitchMenu(options[Current].GetComponent<OptionLogic>().OptionID);
            }
			else
            {
				FunctionMenu(options[Current].GetComponent<OptionLogic>().OptionID);
				options[Current].GetComponent<OptionLogic>().SelectionState = 0;
			}
        }
	}

	void SwitchMenu(int CASEID)
    {
		outline.transform.position = new Vector3(0, -7, 0);
		outline.transform.localScale = new Vector3(0.98f, 0.98f, 1);

		while (true)
        {
			if (options.Count > 0)
            {
				Destroy(options[options.Count - 1]);
				options.RemoveAt(options.Count - 1);
            }
			else
            {
				break;
            }
        }

		switch (CASEID)
        {
			case 0:
				MakeOption(-5, 0, 1, 1, 1, 1, true);
				break;
			case 1:
				MakeOption(5, 1, 1, 1, 0, 0, true);
				MakeOption(5, 3, 1, 1, 1, 1, true);
				break;
        }
    }

	void FunctionMenu(int CASEID)
    {
		//
    }
}
