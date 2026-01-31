using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMarkLogic : MonoBehaviour {

	private GameObject inputlistener;
	private GameObject outline;
	private GameObject selection;
	private GameObject canvasstatusleft;
	private GameObject canvasstatusright;
	private GameObject tooltip;
	private List<GameObject> options = new List<GameObject>();
	private int modecounter;
	private string firstnumber;
	private char sign;
	private string secondnumber;

	public GameObject newoption;
	public GameObject newbgchoice;
	public GameObject newcanvas;
	public GameObject newpicture;
	public GameObject newdigit;
	public GameObject newswatch;
	public Sprite[] points;
	public Sprite[] tooltips;
	public bool open;

	GameObject addedswatch;
	GameObject addeddigit;

	void MakeOption(float XP, float YP, float XS, float YS, int INUM, int ID, bool MOPTION, int TOOLTIP, float TTVSCALE)
    {
		GameObject addedoption;
		addedoption = Instantiate(newoption, new Vector3(XP, YP, 0), Quaternion.identity);
		addedoption.transform.localScale = new Vector3(XS, YS, 1);
		addedoption.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().icons[INUM];
		addedoption.GetComponent<OptionLogic>().OptionID = ID;
		addedoption.GetComponent<OptionLogic>().MakeOptionsWhenSelect = MOPTION;
		addedoption.GetComponent<OptionLogic>().TooltipID = TOOLTIP;
		addedoption.GetComponent<OptionLogic>().TooltipYScale = TTVSCALE;

		options.Add(addedoption);
	}

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().sprite = points[0];
		open = false;
		inputlistener = GameObject.Find("InputListener");
		outline = GameObject.Find("Outline");
		selection = GameObject.Find("Selection");
		canvasstatusleft = GameObject.Find("CanvasStatusLeft");
		canvasstatusright = GameObject.Find("CanvasStatusRight");
		tooltip = GameObject.Find("Tooltip");
		firstnumber = "";
		sign = '\0';
		secondnumber = "";
		
		SwitchMenu(4);
	}
	
	// Update is called once per frame
	void Update () {
		if (open == true)
        {
			this.GetComponent<SpriteRenderer>().sprite = points[1];
		}
		else
        {
			this.GetComponent<SpriteRenderer>().sprite = points[0];
		}

		transform.position = new Vector3(inputlistener.GetComponent<InputLogic>().PointerX, inputlistener.GetComponent<InputLogic>().PointerY, 0);

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
				tooltip.transform.localScale = new Vector3(1, options[Current].GetComponent<OptionLogic>().TooltipYScale, 1);
				tooltip.GetComponent<SpriteRenderer>().sprite = tooltips[options[Current].GetComponent<OptionLogic>().TooltipID];
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
				tooltip.transform.localScale = new Vector3(1, 1, 1);
				tooltip.GetComponent<SpriteRenderer>().sprite = tooltips[0];
			}
			Current++;
		}

		tooltip.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + 0.6f) - (0.3f * (1 - tooltip.transform.localScale.y)), 0);

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

		tooltip.GetComponent<SpriteRenderer>().sprite = tooltips[0];

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

		GameObject addedpicture = newpicture;
		addedswatch = newswatch;
		addeddigit = newdigit;

		switch (CASEID)
        {
			case 0: //Desktop Background Menu
				GameObject.Find("Background").GetComponent<BackgroundLogic>().SetBackground(4);
				MakeOption(0, 4, 7, 1.4f, 4, 0, false, 0, 1);
				MakeOption(0, 2, 7, 1.4f, 4, 1, false, 0, 1);
				MakeOption(0, 0, 7, 1.4f, 4, 2, false, 0, 1);
				MakeOption(0, -2, 7, 1.4f, 4, 3, false, 0, 1);

				GameObject addedbgchoice;

				addedbgchoice = Instantiate(newbgchoice, new Vector3(0, 4, 0), Quaternion.identity);
				addedbgchoice.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Background").GetComponent<BackgroundLogic>().backgrounds[0];

				addedbgchoice = Instantiate(newbgchoice, new Vector3(0, 2, 0), Quaternion.identity);
				addedbgchoice.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Background").GetComponent<BackgroundLogic>().backgrounds[1];

				addedbgchoice = Instantiate(newbgchoice, new Vector3(0, 0, 0), Quaternion.identity);
				addedbgchoice.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Background").GetComponent<BackgroundLogic>().backgrounds[2];

				addedbgchoice = Instantiate(newbgchoice, new Vector3(0, -2, 0), Quaternion.identity);
				addedbgchoice.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Background").GetComponent<BackgroundLogic>().backgrounds[3];

				selection.transform.position = new Vector3(0, (4 - (GameObject.Find("Background").GetComponent<BackgroundLogic>().desktop * 2)), 0);
				selection.transform.localScale = new Vector3(5.8f, 0.98f, 1);

				MakeOption(0, -3.5f, 1.8f, 0.8f, 5, 4, true, 5, 1);
				break;
			case 1: //Photo Album Menu
				GameObject.Find("Background").GetComponent<BackgroundLogic>().SetBackground(5);

				MakeOption(-3.9f, 2.05f, 2.4f, 2.4f, 6, 5, true, 6, 1);
				MakeOption(0, 2.05f, 2.4f, 2.4f, 6, 6, true, 7, 1);
				MakeOption(3.9f, 2.05f, 2.4f, 2.4f, 6, 7, true, 8, 1);

				MakeOption(-3.9f, -1.3f, 2.4f, 2.4f, 6, 8, true, 9, 1);
				MakeOption(0, -1.3f, 2.4f, 2.4f, 6, 9, true, 10, 1);
				MakeOption(3.9f, -1.3f, 2.4f, 2.4f, 6, 10, true, 11, 1);

				DeleteTaggedObjects();
				inputlistener.GetComponent<InputLogic>().picture.text = "";

				addedpicture = Instantiate(newpicture, new Vector3(-3.9f, 2.05f, 0), Quaternion.identity);
				addedpicture.transform.localScale = new Vector3(0.45f, 0.45f, 1);
				addedpicture.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().pictures[0];

				addedpicture = Instantiate(newpicture, new Vector3(0, 2.05f, 0), Quaternion.identity);
				addedpicture.transform.localScale = new Vector3(0.45f, 0.45f, 1);
				addedpicture.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().pictures[1];

				addedpicture = Instantiate(newpicture, new Vector3(3.9f, 2.05f, 0), Quaternion.identity);
				addedpicture.transform.localScale = new Vector3(0.45f, 0.45f, 1);
				addedpicture.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().pictures[2];

				addedpicture = Instantiate(newpicture, new Vector3(-3.9f, -1.3f, 0), Quaternion.identity);
				addedpicture.transform.localScale = new Vector3(0.45f, 0.45f, 1);
				addedpicture.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().pictures[3];

				addedpicture = Instantiate(newpicture, new Vector3(0, -1.3f, 0), Quaternion.identity);
				addedpicture.transform.localScale = new Vector3(0.45f, 0.45f, 1);
				addedpicture.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().pictures[4];

				addedpicture = Instantiate(newpicture, new Vector3(3.9f, -1.3f, 0), Quaternion.identity);
				addedpicture.transform.localScale = new Vector3(0.45f, 0.45f, 1);
				addedpicture.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().pictures[5];

				MakeOption(0, -3.5f, 1.8f, 0.8f, 5, 4, true, 5, 1);
				break;
			case 2: //Art Canvas Menu
				GameObject.Find("Background").GetComponent<BackgroundLogic>().SetBackground(6);

				MakeOption(-7.1f, 4.3f, 0.55f, 0.55f, 10, 6, false, 15, 0.6f);
				MakeOption(-6.3f, 4.3f, 0.55f, 0.55f, 10, 7, false, 16, 0.6f);
				MakeOption(-5.5f, 4.3f, 0.55f, 0.55f, 10, 8, false, 17, 0.6f);
				MakeOption(-4.7f, 4.3f, 0.55f, 0.55f, 10, 9, false, 18, 0.6f);

				MakeOption(-7.5f, 3.5f, 0.55f, 0.55f, 10, 10, false, 19, 0.6f);
				MakeOption(-6.7f, 3.5f, 0.55f, 0.55f, 10, 11, false, 20, 0.6f);
				MakeOption(-5.9f, 3.5f, 0.55f, 0.55f, 10, 12, false, 21, 0.6f);
				MakeOption(-5.1f, 3.5f, 0.55f, 0.55f, 10, 13, false, 22, 0.6f);
				MakeOption(-4.3f, 3.5f, 0.55f, 0.55f, 10, 14, false, 23, 0.6f);

				MakeOption(4.3f, 4.3f, 0.55f, 0.55f, 10, 15, false, 24, 0.6f);
				MakeOption(5.1f, 4.3f, 0.55f, 0.55f, 10, 16, false, 25, 0.6f);
				MakeOption(5.9f, 4.3f, 0.55f, 0.55f, 10, 17, false, 26, 0.6f);
				MakeOption(6.7f, 4.3f, 0.55f, 0.55f, 10, 18, false, 27, 0.6f);
				MakeOption(7.5f, 4.3f, 0.55f, 0.55f, 10, 19, false, 28, 0.6f);

				MakeOption(4.3f, 3.5f, 0.55f, 0.55f, 10, 20, false, 29, 0.6f);
				MakeOption(5.1f, 3.5f, 0.55f, 0.55f, 10, 21, false, 30, 0.6f);
				MakeOption(5.9f, 3.5f, 0.55f, 0.55f, 10, 22, false, 31, 0.6f);
				MakeOption(6.7f, 3.5f, 0.55f, 0.55f, 10, 23, false, 32, 0.6f);
				MakeOption(7.5f, 3.5f, 0.55f, 0.55f, 10, 24, false, 33, 0.6f);

				MakeOption(0, 4.3f, 1.2f, 0.55f, 11, 25, false, 0, 1);

				addedswatch = Instantiate(newswatch, new Vector3(-7.1f, 4.3f, 0), Quaternion.identity);
				addedswatch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1, 1, TextureFormat.RGBA32, false), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
				addedswatch.transform.localScale = new Vector3(0.48f, 0.48f, 1);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.black);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addedswatch = Instantiate(newswatch, new Vector3(-6.3f, 4.3f, 0), Quaternion.identity);
				addedswatch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1, 1, TextureFormat.RGBA32, false), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
				addedswatch.transform.localScale = new Vector3(0.48f, 0.48f, 1);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.gray);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addedswatch = Instantiate(newswatch, new Vector3(-5.5f, 4.3f, 0), Quaternion.identity);
				addedswatch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1, 1, TextureFormat.RGBA32, false), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
				addedswatch.transform.localScale = new Vector3(0.48f, 0.48f, 1);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.white);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addedswatch = Instantiate(newswatch, new Vector3(-4.7f, 4.3f, 0), Quaternion.identity);
				addedswatch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1, 1, TextureFormat.RGBA32, false), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
				addedswatch.transform.localScale = new Vector3(0.48f, 0.48f, 1);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.red);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addedswatch = Instantiate(newswatch, new Vector3(-7.5f, 3.5f, 0), Quaternion.identity);
				addedswatch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1, 1, TextureFormat.RGBA32, false), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
				addedswatch.transform.localScale = new Vector3(0.48f, 0.48f, 1);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.yellow);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addedswatch = Instantiate(newswatch, new Vector3(-6.7f, 3.5f, 0), Quaternion.identity);
				addedswatch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1, 1, TextureFormat.RGBA32, false), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
				addedswatch.transform.localScale = new Vector3(0.48f, 0.48f, 1);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.green);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addedswatch = Instantiate(newswatch, new Vector3(-5.9f, 3.5f, 0), Quaternion.identity);
				addedswatch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1, 1, TextureFormat.RGBA32, false), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
				addedswatch.transform.localScale = new Vector3(0.48f, 0.48f, 1);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.cyan);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addedswatch = Instantiate(newswatch, new Vector3(-5.1f, 3.5f, 0), Quaternion.identity);
				addedswatch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1, 1, TextureFormat.RGBA32, false), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
				addedswatch.transform.localScale = new Vector3(0.48f, 0.48f, 1);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.blue);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addedswatch = Instantiate(newswatch, new Vector3(-4.3f, 3.5f, 0), Quaternion.identity);
				addedswatch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1, 1, TextureFormat.RGBA32, false), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
				addedswatch.transform.localScale = new Vector3(0.48f, 0.48f, 1);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.magenta);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addedswatch = Instantiate(newswatch, new Vector3(-0.45f, 3.454f, 0), Quaternion.identity);
				addedswatch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1, 1, TextureFormat.RGBA32, false), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
				addedswatch.transform.localScale = new Vector3(0.5f, 0.5f, 1);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.black);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addeddigit = Instantiate(newdigit, new Vector3(4.17f, 4.3f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[1];

				addeddigit = Instantiate(newdigit, new Vector3(4.97f, 4.3f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[2];

				addeddigit = Instantiate(newdigit, new Vector3(5.77f, 4.3f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[3];

				addeddigit = Instantiate(newdigit, new Vector3(6.57f, 4.3f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[4];

				addeddigit = Instantiate(newdigit, new Vector3(7.37f, 4.3f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[5];

				addeddigit = Instantiate(newdigit, new Vector3(4.17f, 3.5f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[6];

				addeddigit = Instantiate(newdigit, new Vector3(4.97f, 3.5f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[7];

				addeddigit = Instantiate(newdigit, new Vector3(5.77f, 3.5f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[8];

				addeddigit = Instantiate(newdigit, new Vector3(6.57f, 3.5f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[9];

				addeddigit = Instantiate(newdigit, new Vector3(7.37f, 3.5f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[10];

				addeddigit = Instantiate(newdigit, new Vector3(0.19f, 3.44f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[1];

				Instantiate(newcanvas, Vector3.zero, Quaternion.identity);

				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawColor);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();

				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize];
				addeddigit.transform.localScale = new Vector3(1.3f, 1.3f, 1);

				canvasstatusleft.transform.position = new Vector3(-0.45f, 3.454f, 0);
				canvasstatusright.transform.position = new Vector3(0.45f, 3.454f, 0);

				MakeOption(0, -3.7f, 1.8f, 0.56f, 5, 4, true, 5, 1);
				break;
			case 3: //Calculator Menu
				GameObject.Find("Background").GetComponent<BackgroundLogic>().SetBackground(7);
				modecounter = 0;
				firstnumber = "";
				sign = '\0';
				secondnumber = "";

				MakeOption(-4.68f, 0.735f, 2.95f, 0.6f, 12, 26, false, 0, 1);
				MakeOption(-0.815f, 0.735f, 2.95f, 0.6f, 12, 27, false, 0, 1);
				MakeOption(3.05f, 0.735f, 2.95f, 0.6f, 12, 28, false, 0, 1);

				MakeOption(-4.68f, -0.115f, 2.95f, 0.6f, 12, 29, false, 0, 1);
				MakeOption(-0.815f, -0.115f, 2.95f, 0.6f, 12, 30, false, 0, 1);
				MakeOption(3.05f, -0.115f, 2.95f, 0.6f, 12, 31, false, 0, 1);

				MakeOption(-4.68f, -0.965f, 2.95f, 0.6f, 12, 32, false, 0, 1);
				MakeOption(-0.815f, -0.965f, 2.95f, 0.6f, 12, 33, false, 0, 1);
				MakeOption(3.05f, -0.965f, 2.95f, 0.6f, 12, 34, false, 0, 1);

				MakeOption(-4.68f, -1.815f, 2.95f, 0.6f, 12, 35, false, 0, 1);
				MakeOption(-0.815f, -1.815f, 2.95f, 0.6f, 12, 36, false, 0, 1);
				MakeOption(3.05f, -1.815f, 2.95f, 0.6f, 12, 37, false, 0, 1);

				MakeOption(6, 1.16f, 0.9f, 0.6f, 13, 38, false, 0, 1);
				MakeOption(6, 0.31f, 0.9f, 0.6f, 13, 39, false, 0, 1);
				MakeOption(6, -0.54f, 0.9f, 0.6f, 13, 40, false, 0, 1);
				MakeOption(6, -1.39f, 0.9f, 0.6f, 13, 41, false, 0, 1);
				MakeOption(6, -2.24f, 0.9f, 0.6f, 13, 42, false, 0, 1);

				addeddigit = Instantiate(newdigit, new Vector3(-4.68f, 0.735f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[11];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(-0.815f, 0.735f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[0];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(3.05f, 0.735f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[12];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(-4.68f, -0.115f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[1];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(-0.815f, -0.115f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[2];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(3.05f, -0.115f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[3];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(-4.68f, -0.965f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[4];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(-0.815f, -0.965f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[5];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(3.05f, -0.965f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[6];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(-4.68f, -1.815f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[7];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(-0.815f, -1.815f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[8];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(3.05f, -1.815f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[9];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(6, 1.16f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[13];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(6, 0.31f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[14];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(6, -0.54f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[15];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(6, -1.39f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[16];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				addeddigit = Instantiate(newdigit, new Vector3(6, -2.24f, 0), Quaternion.identity);
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[17];
				addeddigit.transform.localScale = new Vector3(1.1f, 1.1f, 1);

				MakeOption(0, -3.52f, 1.8f, 0.8f, 5, 4, true, 5, 1);
				break;
			case 4: //Desktop Menu
				DeleteTaggedObjects();
				inputlistener.GetComponent<InputLogic>().calculator.text = "";

				selection.transform.position = new Vector3(0, -7, 0);
				selection.transform.localScale = new Vector3(0.98f, 0.98f, 1);

				canvasstatusleft.transform.position = new Vector3(-0.45f, 7, 0);
				canvasstatusright.transform.position = new Vector3(0.45f, 7, 0);

				GameObject.Find("Background").GetComponent<BackgroundLogic>().SetBackground(GameObject.Find("Background").GetComponent<BackgroundLogic>().desktop);
				MakeOption(-7.5f, 3.5f, 1, 1, 0, 0, true, 1, 1);
				MakeOption(-5.5f, 3.5f, 1, 1, 1, 1, true, 2, 1);
				MakeOption(-3.5f, 3.5f, 1, 1, 2, 2, true, 3, 1);
				MakeOption(-1.5f, 3.5f, 1, 1, 3, 3, true, 4, 1);
				break;
			case 5: //Flowers Picture Menu
				ShowPictureMenu(0, addedpicture);
				break;
			case 6: //Mantis Shrimp Picture Menu
				ShowPictureMenu(1, addedpicture);
				break;
            case 7: //Glacier Picture Menu
				ShowPictureMenu(2, addedpicture);
				break;
			case 8: //Dragon Fruit Picture Menu
				ShowPictureMenu(3, addedpicture);
				break;
			case 9: //Sunrise Picture Menu
				ShowPictureMenu(4, addedpicture);
				break;
			case 10: //Mushrooms Picture Menu
				ShowPictureMenu(5, addedpicture);
				break;
        }
    }

	void FunctionMenu(int CASEID)
    {
		switch (CASEID)
        {
			case 0: //Switch Desktop Background Background One
				GameObject.Find("Background").GetComponent<BackgroundLogic>().desktop = 0;
				selection.transform.position = new Vector3(0, (4 - (GameObject.Find("Background").GetComponent<BackgroundLogic>().desktop * 2)), 0);
				break;
			case 1: //Switch Desktop Background Background Two
				GameObject.Find("Background").GetComponent<BackgroundLogic>().desktop = 1;
				selection.transform.position = new Vector3(0, (4 - (GameObject.Find("Background").GetComponent<BackgroundLogic>().desktop * 2)), 0);
				break;
			case 2: //Switch Desktop Background Background Three
				GameObject.Find("Background").GetComponent<BackgroundLogic>().desktop = 2;
				selection.transform.position = new Vector3(0, (4 - (GameObject.Find("Background").GetComponent<BackgroundLogic>().desktop * 2)), 0);
				break;
			case 3: //Switch Desktop Background Background Four
				GameObject.Find("Background").GetComponent<BackgroundLogic>().desktop = 3;
				selection.transform.position = new Vector3(0, (4 - (GameObject.Find("Background").GetComponent<BackgroundLogic>().desktop * 2)), 0);
				break;
			case 4: //Previous Picture
				modecounter--;

				if (modecounter < 0)
                {
					modecounter = inputlistener.GetComponent<InputLogic>().pictures.Length - 1;
				}

				inputlistener.GetComponent<InputLogic>().SetPictureText(modecounter + 1);

				GameObject.Find("Picture(Clone)").GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().pictures[modecounter];
				break;
			case 5: //Next Picture
				modecounter++;

				if (modecounter > (inputlistener.GetComponent<InputLogic>().pictures.Length - 1))
				{
					modecounter = 0;
				}

				inputlistener.GetComponent<InputLogic>().SetPictureText(modecounter + 1);

				GameObject.Find("Picture(Clone)").GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().pictures[modecounter];
				break;
			case 6: //Set Canvas Draw Color Black
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawColor = Color.black;
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.black);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();
				break;
			case 7: //Set Canvas Draw Color Gray
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawColor = Color.gray;
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.gray);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();
				break;
			case 8: //Set Canvas Draw Color White
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawColor = Color.white;
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.white);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();
				break;
			case 9: //Set Canvas Draw Color Red
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawColor = Color.red;
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.red);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();
				break;
			case 10: //Set Canvas Draw Color Yellow
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawColor = Color.yellow;
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.yellow);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();
				break;
			case 11: //Set Canvas Draw Color Green
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawColor = Color.green;
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.green);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();
				break;
			case 12: //Set Canvas Draw Color Cyan
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawColor = Color.cyan;
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.cyan);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();
				break;
			case 13: //Set Canvas Draw Color Blue
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawColor = Color.blue;
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.blue);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();
				break;
			case 14: //Set Canvas Draw Color Magenta
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawColor = Color.magenta;
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.SetPixel(0, 0, Color.magenta);
				addedswatch.GetComponent<SpriteRenderer>().sprite.texture.Apply();
				break;
			case 15: //Set Canvas Draw Size One Pixel
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize = 1;
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[1];
				break;
			case 16: //Set Canvas Draw Size Two Pixels
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize = 2;
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[2];
				break;
			case 17: //Set Canvas Draw Size Three Pixels
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize = 3;
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[3];
				break;
			case 18: //Set Canvas Draw Size Four Pixels
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize = 4;
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[4];
				break;
			case 19: //Set Canvas Draw Size Five Pixels
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize = 5;
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[5];
				break;
			case 20: //Set Canvas Draw Size Six Pixels
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize = 6;
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[6];
				break;
			case 21: //Set Canvas Draw Size Seven Pixels
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize = 7;
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[7];
				break;
			case 22: //Set Canvas Draw Size Eight Pixels
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize = 8;
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[8];
				break;
			case 23: //Set Canvas Draw Size Nine Pixels
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize = 9;
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[9];
				break;
			case 24: //Set Canvas Draw Size Ten Pixels
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().DrawSize = 10;
				addeddigit.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().digits[10];
				break;
			case 25: //Set Canvas Screen Fully Erased
				GameObject.Find("DrawCanvas(Clone)").GetComponent<DrawCanvasLogic>().ClearScreen();
				break;
			case 26: //Calculator Input Delete
				if (firstnumber.Length > 0 && modecounter == 0)
                {
					firstnumber = firstnumber.Substring(0, firstnumber.Length - 1);
					inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber;
				}
				else if (modecounter == 1)
                {
					sign = '\0';
					inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber;
					modecounter = 0;
				}
				else if (modecounter == 2)
                {
					secondnumber = secondnumber.Substring(0, secondnumber.Length - 1);
					if (secondnumber.Length == 0)
                    {
						inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber + sign;
						modecounter = 1;
					}
					else
                    {
						inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber + sign + secondnumber;
					}
				}
				else
                {
					modecounter = 0;
					firstnumber = "";
					sign = '\0';
					secondnumber = "";
					inputlistener.GetComponent<InputLogic>().calculator.text = "";
				}
				break;
			case 27: //Calculator Input Zero
				if (modecounter == 3)
                {
					modecounter = 0;
					firstnumber = "";
					sign = '\0';
					secondnumber = "";
				}
				if (modecounter == 0 && firstnumber.Length < 12)
                {
					if (firstnumber.Length == 0 || (firstnumber.Length != 0 && firstnumber[0] != '0'))
                    {
						firstnumber = firstnumber + "0";
						inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber;
					}
                }
				else if (modecounter > 0 && modecounter < 3 && secondnumber.Length < 12)
                {
					if (secondnumber.Length == 0 || (secondnumber.Length != 0 && secondnumber[0] != '0'))
					{
						secondnumber = secondnumber + "0";
						inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber + sign + secondnumber;
						modecounter = 2;
					}
				}
				break;
			case 28: //Calculator Input Clear
				modecounter = 0;
				firstnumber = "";
				sign = '\0';
				secondnumber = "";
				inputlistener.GetComponent<InputLogic>().calculator.text = "";
				break;
			case 29: //Calculator Input One
				AddNumber("1");
				break;
			case 30: //Calculator Input Two
				AddNumber("2");
				break;
			case 31: //Calculator Input Three
				AddNumber("3");
				break;
			case 32: //Calculator Input Four
				AddNumber("4");
				break;
			case 33: //Calculator Input Five
				AddNumber("5");
				break;
			case 34: //Calculator Input Six
				AddNumber("6");
				break;
			case 35: //Calculator Input Seven
				AddNumber("7");
				break;
			case 36: //Calculator Input Eight
				AddNumber("8");
				break;
			case 37: //Calculator Input Nine
				AddNumber("9");
				break;
			case 38: //Calculator Input Divide
				AddSign('/');
				break;
			case 39: //Calculator Input Multiply
				AddSign('*');
				break;
			case 40: //Calculator Input Subtract
				AddSign('-');
				break;
			case 41: //Calculator Input Add
				AddSign('+');
				break;
			case 42: //Calculator Input Equal
				if (modecounter == 2)
                {
					long one = long.Parse(firstnumber);
					long two = long.Parse(secondnumber);
					if (sign == '/')
                    {
						if (one == 0 && two == 0)
                        {
							inputlistener.GetComponent<InputLogic>().calculator.text = "Indeterminate";
						}
						else if (two == 0)
                        {
							inputlistener.GetComponent<InputLogic>().calculator.text = "Undefined";
						}
						else
                        {
							double answer = two;
							answer = one / answer;
							inputlistener.GetComponent<InputLogic>().calculator.text = "" + answer;
						}
					}
					else if (sign == '*')
                    {
						double answer = one * two;
						inputlistener.GetComponent<InputLogic>().calculator.text = "" + answer;
					}
					else if (sign == '-')
                    {
						double answer = one - two;
						inputlistener.GetComponent<InputLogic>().calculator.text = "" + answer;
					}
					else if (sign == '+')
                    {
						double answer = one + two;
						inputlistener.GetComponent<InputLogic>().calculator.text = "" + answer;
					}
					modecounter = 3;
                }
				break;
		}
	}

	void DeleteTaggedObjects()
    {
		GameObject[] deletes = GameObject.FindGameObjectsWithTag("DELETEONQUIT");

		for (int i = 0; i < deletes.Length; i++)
		{
			Destroy(deletes[i]);
		}
	}

	void ShowPictureMenu(int num, GameObject pic)
    {
		DeleteTaggedObjects();
		modecounter = num;

		pic = Instantiate(newpicture, new Vector3(0, 0.6f, 0), Quaternion.identity);
		pic.GetComponent<SpriteRenderer>().sprite = inputlistener.GetComponent<InputLogic>().pictures[num];

		inputlistener.GetComponent<InputLogic>().SetPictureText(modecounter + 1);

		MakeOption(-2.4f, -3.5f, 0.8f, 0.8f, 7, 4, false, 12, 1);
		MakeOption(0, -3.5f, 1.8f, 0.8f, 8, 1, true, 13, 1);
		MakeOption(2.4f, -3.5f, 0.8f, 0.8f, 9, 5, false, 14, 1);
	}

	void AddNumber(string num)
    {
		if (modecounter == 3)
		{
			modecounter = 0;
			firstnumber = "";
			sign = '\0';
			secondnumber = "";
		}
		if (modecounter == 0 && firstnumber.Length < 12)
		{
			if (firstnumber.Length > 0)
			{
				if (firstnumber[0] == '0')
				{
					firstnumber = num;
				}
				else
				{
					firstnumber = firstnumber + num;
				}
				inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber;
			}
			else
			{
				firstnumber = firstnumber + num;
				inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber;
			}
		}
		else if (modecounter > 0 && modecounter < 3 && secondnumber.Length < 12)
		{
			if (secondnumber.Length > 0)
			{
				if (secondnumber[0] == '0')
				{
					secondnumber = num;
				}
				else
				{
					secondnumber = secondnumber + num;
				}
				inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber + sign + secondnumber;
			}
			else
			{
				secondnumber = secondnumber + num;
				inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber + sign + secondnumber;
			}
			modecounter = 2;
		}
	}

	void AddSign(char c)
    {
		if (firstnumber.Length > 0 && modecounter < 2)
		{
			sign = c;
			inputlistener.GetComponent<InputLogic>().calculator.text = firstnumber + sign;
			if (modecounter == 0)
			{
				modecounter = 1;
			}
		}
	}
}
