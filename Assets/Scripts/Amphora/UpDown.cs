using UnityEngine;
 
public class UpDown : MonoBehaviour{
	
	[SerializeField] private AnimationCurve myCurve;
	[SerializeField] private float Offset = 1;
	[SerializeField] private int yspeed = 50;
	
	void Start() {
		Offset += Random.Range (0.05f, 0.1f);
		yspeed += Random.Range (5, 10);
	}
	
	void Update(){
		transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)) + Offset, transform.position.z);
		transform.Rotate( 0, yspeed * Time.deltaTime, 0);
	}
}