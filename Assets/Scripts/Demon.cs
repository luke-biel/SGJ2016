using System.Collections;
using UnityEngine;

public class Demon : MonoBehaviour {
    public bool hit;
	public bool isPossesed;
    private GameObject smoke;
    [SerializeField, Disabled]
    private float power;
    private float ratio;

    public void Update() {
        if(hit) {
            onHit();
            hit = false;
        }
    }

    internal void posses()
    {
        power = 100.0f;
        ratio = Random.Range(0.4f, 1.1f);
        isPossesed = true;
        smoke = Instantiate(Resources.Load<GameObject>("WhiteSmoke"));
        float y = GetComponent<SpriteRenderer>().bounds.extents.y;
        smoke.transform.position = new Vector2(transform.position.x, transform.position.y - y);
        smoke.transform.localScale = new Vector3(
            GetComponent<SpriteRenderer>().bounds.extents.x / 6, 
            transform.localScale.y,
            transform.localScale.z);
    }

    internal void exorcise() {
        Destroy(smoke);
        isPossesed = false;
    }

    public void drainPower(float dt, CatController controler) {
        power -= dt * ratio * 10;
        if(power <= 0) {
            exorcise();
            controler.stopSleeping();
        }
    }

    public void onHit() {
        StartCoroutine(drop());
    }

    private IEnumerator drop() {
        yield return null;
        Destroy(this);
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}