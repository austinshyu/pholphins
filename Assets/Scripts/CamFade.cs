using UnityEngine;
using System.Collections;

public class CamFade : MonoBehaviour {

	public Texture2D fadeTexture;
	float fadeSpeed = 0.2f;
	int drawDepth = -1000;
	
	private float alpha = 1.0f; 
	public float fadeDir = -1f;
	
	void OnGUI(){
		
		alpha += fadeDir * fadeSpeed * Time.deltaTime;  
		alpha = Mathf.Clamp01(alpha);   

		Color c = GUI.color;
		c.a = alpha;
		GUI.color = c;
		
		GUI.depth = drawDepth;
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
	}
}
