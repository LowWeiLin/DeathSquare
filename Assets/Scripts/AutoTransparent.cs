using UnityEngine;
using System.Collections;

public class AutoTransparent : MonoBehaviour {

	private Shader m_OldShader = null;
	private Color m_OldColor = Color.black;
	private float m_Transparency = 0.3f;
	private const float m_TargetTransparancy = 0.3f;
	private const float m_FallOff = 0.1f; // returns to 100% in 0.1 sec

	new private Renderer renderer = null;

	bool shouldBeTransparent = false;

	public void BeTransparent()
	{
		shouldBeTransparent = true;
	}

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer> ();

		if (renderer.material)
		{   
			// Save the current shader
			m_OldShader = renderer.material.shader;
			renderer.material.shader = Shader.Find("Transparent/Diffuse");
			if (renderer.material.HasProperty("_Color") )
			{
				m_OldColor  = renderer.material.color;
				m_Transparency = m_OldColor.a;
			} else
			{
				m_OldColor = Color.white;
				m_Transparency = 1.0f;
			}
		} else
		{
			m_Transparency = 1.0f;
		}
	}

	void OnDestroy() {
		if (!m_OldShader) return;
		// Reset the shader
		renderer.material.shader = m_OldShader;
		renderer.material.color = m_OldColor;
	}
	
	// Update is called once per frame
	void Update () {
		//Shoud AutoTransparent component be removed?
		if (!shouldBeTransparent && m_Transparency >= 1.0f)
		{
			Destroy(this);
		}
		//Are we fading in our out?
		if (shouldBeTransparent)
		{
			//Fading out
			if (m_Transparency > m_TargetTransparancy)
				m_Transparency -= ( (1.0f - m_TargetTransparancy) * Time.deltaTime) / m_FallOff;
		} else
		{
			//Fading in
			m_Transparency += ( (1.0f - m_TargetTransparancy) * Time.deltaTime) / m_FallOff;
		}

		Color C = renderer.material.color;
		C.a = m_Transparency;
		renderer.material.color = C;

		//The object will start to become visible again if BeTransparent() is not called
		shouldBeTransparent = false;
	}
}
