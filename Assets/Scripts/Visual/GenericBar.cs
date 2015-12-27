using UnityEngine;
using System.Collections;

public abstract class GenericBar : MonoBehaviour {
	
	protected Rect defaultSize = new Rect(0, 0, 40, 2);
	protected Vector2 defaultOffset = new Vector2(-20, -40);
	protected Color defaultForegroundColour = Color.green;
	protected Color defaultBackgroundColour = Color.red;
	
	Texture2D bgTex, fgTex;
	
	protected abstract float Value();
	protected abstract float MaxValue();

	protected virtual Rect Size() {
		return defaultSize;
	}

	protected virtual Vector2 Offset() {
		return defaultOffset;
	}

	protected virtual Color ForegroundColour() {
		return defaultForegroundColour;
	}

	protected virtual Color BackgroundColour() {
		return defaultBackgroundColour;
	}

	protected void Init() {
		// Use 1x1 pixels to draw the health bars
		bgTex = new Texture2D(1,1);
		bgTex.SetPixel(0, 0, BackgroundColour());
		bgTex.Apply();

		fgTex = new Texture2D(1,1);
		fgTex.SetPixel(0, 0, ForegroundColour());
		fgTex.Apply();
	}
		
	void OnGUI() {

		if (bgTex == null || fgTex == null) {
			return;
		}

		// Background
		Rect computedSize = Size();
		computedSize.x = Camera.main.WorldToScreenPoint(transform.position).x + Offset().x;
		computedSize.y = Screen.height - Camera.main.WorldToScreenPoint(transform.position).y + Offset().y;
		
		GUI.DrawTexture(computedSize, bgTex);
		
		// Foregrounds
		Rect partialRect = computedSize;
		partialRect.width = defaultSize.width * ((float) Value() / (float) MaxValue());
		partialRect.x = computedSize.x;
		
		GUI.DrawTexture(partialRect, fgTex);
	}
}