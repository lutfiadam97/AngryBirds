using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Burung : MonoBehaviour
{
	public enum BirdState {Idle, Thrown, HitSomething }
	public GameObject Parent;
	public Rigidbody2D RigidBody;
	public CircleCollider2D Collider;

	public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<Burung> OnBirdShot = delegate { };

    public BirdState State { get { return state; } }

	private BirdState state;
	private float minVelocity = 0.05f;
	private bool flagDestroy = false;
    // Pada method Start, kita akan mematikan fungsi physics dan collider dari burung.
    void Start()
    {
        RigidBody.bodyType = RigidbodyType2D.Kinematic;
        Collider.enabled = false;
        state = BirdState.Idle;
    }

    // Method FixedUpdate merupakan method bawaan dari Monobehaviour, di mana method tersebut akan terus dipanggil 
    //pada setiap fixed frame Dalam method tersebut, kita akan mengubah state dari burung menjadi Thrown
    //jika kondisi saat ini adalah Idle dan kecepatannya berubah menjadi lebih dari 0.5. Jika kondisi burung pada saat ini 
    //adalah Thrown, dan kecepatan burung telah berada di bawah batas minimum (0.5), maka kita akan menghancurkan game object  
    //burung tersebut setelah 2 detik.
    void FixedUpdate()
    {
        if(state == BirdState.Idle && RigidBody.velocity.sqrMagnitude >= minVelocity)
        {
        	state = BirdState.Thrown;
        }

        if((state == BirdState.Thrown || state == BirdState.HitSomething) && RigidBody.velocity.sqrMagnitude < minVelocity && !flagDestroy)
        {
        	// hancurkan game object setelah 2 detik
        	// jika kecepatannya sudah berkurang dari batas minimum
        	flagDestroy = true;
        	StartCoroutine(DestroyAfter(2));
        }
    }

    void OnDestroy()
    {
        if(state == BirdState.Thrown || state == BirdState.HitSomething)
    	OnBirdDestroyed();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        state = BirdState.HitSomething;
    }

    private IEnumerator DestroyAfter(float second)
    {
    	yield return new WaitForSeconds(second);
    	Destroy(gameObject);
    }

    //Method MoveTo berfungsi untuk menginisiasi posisi dan mengubah parent dari game object burung.

    public void MoveTo(Vector2 target, GameObject parent)
    {
    	gameObject.transform.SetParent(parent.transform);
    	gameObject.transform.position = target;
    }

    //Method Shoot berfungsi untuk melemparkan burung dengan arah, jarak tali yang ditarik, dan kecepatan awal. 

    public void Shoot(Vector2 velocity, float distance, float speed)
    {
    	Collider.enabled = true;
    	RigidBody.bodyType = RigidbodyType2D.Dynamic;
    	RigidBody.velocity = velocity * speed * distance;
        OnBirdShot(this); // tanda bahwa trail sudah dapat di spawn
    }

    public virtual void OnTap()
    {
        //Do nothing
    }

}
