using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Health))]
public class HealthBar : MonoBehaviour
{
	public Rect m_Rectangle = new Rect(0, 0, 40, 4);
	public Vector2 m_Offset = new Vector2(-20, -40);
	
	Texture2D m_Background;//Red background
	Texture2D m_Foreground;//Green bar foreground
	
	Health m_Health;
	
	// Use this for initialization
	void Start ()
	{
		// Use 1x1 pixels to draw the health bars
		m_Background = new Texture2D(1,1);
		m_Background.SetPixel(0,0, Color.red);
		m_Background.Apply();
		m_Foreground = new Texture2D(1,1);
		m_Foreground.SetPixel(0,0, Color.green);
		m_Foreground.Apply();
		
		m_Health = GetComponent<Health>();
	}
	
	void OnGUI ()
	{
		//Determine positions and draw bars
		//Red background
		m_Rectangle.x = Camera.main.WorldToScreenPoint(transform.position).x + m_Offset.x;
		m_Rectangle.y = Screen.height - Camera.main.WorldToScreenPoint(transform.position).y + m_Offset.y;
		
		GUI.DrawTexture(m_Rectangle, m_Background);
		
		//Green bar foreground
		Rect partialRect = m_Rectangle;
		partialRect.width = m_Rectangle.width * ((float)m_Health.hp/(float)m_Health.maxHp);
		partialRect.x = m_Rectangle.x;
		
		GUI.DrawTexture(partialRect, m_Foreground);
	}
}