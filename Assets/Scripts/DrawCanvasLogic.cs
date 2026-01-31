using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCanvasLogic : MonoBehaviour {

	public float XOffset;
	public float YOffset;
	public int XPixels;
	public int YPixels;
	public int DrawSize;
	public Color DrawColor;

	private Texture2D CanvasDisplay;

	private GameObject inputlistener;
	private GameObject pointer;

	private bool ContinuousDraw;
	private int PrevX;
	private int PrevY;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(XOffset, YOffset, 0);
		DrawSize = 1;
		DrawColor = Color.black;
		ContinuousDraw = false;
		PrevX = 0;
		PrevY = 0;

		inputlistener = GameObject.Find("InputListener");
		pointer = GameObject.Find("PointerMark");

		CanvasDisplay = new Texture2D(XPixels, YPixels, TextureFormat.RGBA32, false);
		CanvasDisplay.filterMode = FilterMode.Point;

		ClearScreen();

		this.GetComponent<SpriteRenderer>().sprite = Sprite.Create(CanvasDisplay, new Rect(0f, 0f, CanvasDisplay.width, CanvasDisplay.height), new Vector2(0f, 0f), 48f);
	}
	
	// Update is called once per frame
	void Update () {
		int PX = (int) Mathf.Round((inputlistener.GetComponent<InputLogic>().PointerX - XOffset) * 48);
		int PY = (int) Mathf.Round((inputlistener.GetComponent<InputLogic>().PointerY - YOffset) * 48);

		if (PX >= 0 && PX < (XPixels + 1) && PY >= 0 && PY < (YPixels + 1))
        {
			pointer.GetComponent<PointerMarkLogic>().open = true;
			if (inputlistener.GetComponent<InputLogic>().BPressed == true)
            {
				if (ContinuousDraw == true && (PrevX != PX || PrevY != PY))
                {
					int Distance = (int) Mathf.Round(Mathf.Sqrt((PX - PrevX) * (PX - PrevX) + (PY - PrevY) * (PY - PrevY)));
					for (int i = 1; i <= Distance; i++)
                    {
						DrawPoint((i * PX + (Distance - i) * PrevX) / Distance, (i * PY + (Distance - i) * PrevY) / Distance);
                    }
                }
				else
                {
					DrawPoint(PX, PY);
                }
				ContinuousDraw = true;
				PrevX = PX;
				PrevY = PY;
				CanvasDisplay.Apply();
			}
			else
            {
				ContinuousDraw = false;
			}
		}
		else
        {
			pointer.GetComponent<PointerMarkLogic>().open = false;
			ContinuousDraw = false;
		}
	}

	void DrawPoint(int X, int Y)
    {
		int i = X - DrawSize + 1;
		int j = Y - DrawSize + 1;
		int iUp = X + DrawSize - 1;
		int jUp = Y + DrawSize - 1;

		if (i < 0)
        {
			i = 0;
        }
		if (j < 0)
        {
			j = 0;
        }
		if (iUp >= XPixels)
        {
			iUp = XPixels - 1;
        }
		if (jUp >= YPixels)
        {
			jUp = YPixels - 1;
        }

		for (int a = i; a <= iUp; a++)
        {
			for (int b = j; b <= jUp; b++)
            {
				if (((a - X) * (a - X) + (b - Y) * (b - Y)) <= DrawSize * DrawSize)
                {
					CanvasDisplay.SetPixel(a, b, DrawColor);
				}
            }
        }
    }

	public void ClearScreen()
    {
		for (int x = 0; x < XPixels; x++)
        {
			for (int y = 0; y < YPixels; y++)
            {
				CanvasDisplay.SetPixel(x, y, Color.white);
            }
        }

		CanvasDisplay.Apply();
	}
}
