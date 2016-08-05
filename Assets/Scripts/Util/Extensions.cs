using System.Linq;
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

	public static bool isOneOf<T>(this T str, T[] tags) {
		return tags.Contains(str);
	}
}