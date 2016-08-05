using UnityEngine;

public static class Extensions {
	public static Vector3 setX(this Vector3 vector, float x) {
		return new Vector3(x, vector.y, vector.z);
	}

	public static Vector3 setY(this Vector3 vector, float y) {
		return new Vector3(vector.x, y, vector.z);
	}

	public static Vector3 setZ(this Vector3 vector, float z) {
		return new Vector3(vector.x, vector.y, z);
	}

	public static bool isInBOunds(this Vector3 vector, Rect bounds) {
		return vector.x >= bounds.xMin && vector.x <= bounds.xMax && vector.y >= bounds.yMin && vector.y <= bounds.yMax;
	}
}