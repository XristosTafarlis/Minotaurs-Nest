using UnityEngine;
 
public class UpDown : MonoBehaviour{
	
	public AnimationCurve myCurve;
	public float Offset = 1;
	public int yspeed = 50;
   
	void Update(){
		transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)) + Offset, transform.position.z);
		transform.Rotate( 0, yspeed * Time.deltaTime, 0);
	}
}