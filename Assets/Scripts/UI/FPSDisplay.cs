using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour{
	[SerializeField] Text text;
	float pollingTime = 0.2f;
	float time;
	int frameCoutn;
	int framerate;

	void Update(){
		time += Time.deltaTime;
		frameCoutn ++;
		if (time >= pollingTime){
			framerate = Mathf.RoundToInt(frameCoutn / time);
			ColorPicker();
			text.text = "FPS : " + framerate.ToString();

			time -= pollingTime;
			frameCoutn = 0;
		}
	}

	void ColorPicker(){
		if(framerate > 60){
			text.color = Color.green;
		}else if (framerate > 30 && framerate <= 60){
			text.color = new Color(1f, 0.55f, 0f);				//Orange
		}else if (framerate <= 30){
			text.color = Color.red;
		}
	}
}