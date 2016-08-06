using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions {
	const float NEAR_ZERO = 0.01f;

	public static Vector3 setX(this Vector3 vector, float x) {
		return new Vector3(x, vector.y, vector.z);
	}

	public static Vector3 setY(this Vector3 vector, float y) {
		return new Vector3(vector.x, y, vector.z);
	}

	public static Vector3 setZ(this Vector3 vector, float z) {
		return new Vector3(vector.x, vector.y, z);
	}

	public static bool isOneOf<T>(this T str, IEnumerable<T> tags) {
		return tags.Contains(str);
	}

	public static float toTri(this float f) {
		if(Mathf.Abs(f) <= NEAR_ZERO) {
			return 0;
		}
		return Mathf.Sign(f);
	}
}