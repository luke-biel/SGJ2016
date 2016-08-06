using UnityEngine;

public class Demon : MonoBehaviour {
	public bool isPossesed;
    private GameObject smoke;

    internal void posses()
    {
        isPossesed = true;
        smoke = Instantiate(Resources.Load<GameObject>("WhiteSmoke"));
        float y = GetComponent<SpriteRenderer>().bounds.extents.y;
        smoke.transform.position = new Vector2(transform.position.x, transform.position.y - y);
    }

    internal void exorcise() {
        Destroy(smoke);
        isPossesed = false;
    }
}