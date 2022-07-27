using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour{
	[SerializeField] Text text;
	float pollingTime = 0.1f;
	float time;
	int frameCoutn;
	void Update(){
		time += Time.deltaTime;
		frameCoutn ++;
		if (time >= pollingTime){
			int framerate = Mathf.RoundToInt(frameCoutn / time);
			text.text = "FPS : " + framerate.ToString();
			
			time -= pollingTime;
			frameCoutn = 0;
		}
	}
}