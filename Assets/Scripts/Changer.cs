using UnityEngine;

public delegate void ChangeMethod(float a);

public class Changer : MonoBehaviour {

	private float StartTime, DeltaTime, StartV, EndV;
	private ChangeMethod ChangeMethod;
	
	public void Change(float startV, float endV, float time, ChangeMethod changeMethod) {
		EndV = endV;
		StartV = startV;
		DeltaTime = time;
		StartTime = Time.time;
		ChangeMethod = changeMethod;
	}
	

	void Update() {
		if (StartTime != 0) {

			if (Time.time > DeltaTime + StartTime) {
				Destroy(this);
			}
			float percent = (Time.time - StartTime) / DeltaTime;
			float deltaA = (EndV * percent);
			float step = Mathf.SmoothStep(0.0f, 1.0f, percent);
			float finalA = Mathf.Lerp(StartV, deltaA, step);

			ChangeMethod(finalA);
		}
	}

}
