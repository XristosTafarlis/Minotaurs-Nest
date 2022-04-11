using UnityEngine;

public class TargetFPS : MonoBehaviour{
	[SerializeField]
	private int FPS = 144;
    void Start(){
        QualitySettings.vSyncCount = 0;
    }
    void Update(){
    	if (FPS != Application.targetFrameRate){
    		Application.targetFrameRate = FPS;
    	}
    }
}
