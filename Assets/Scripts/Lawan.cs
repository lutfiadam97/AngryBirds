using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lawan : MonoBehaviour
{
	public float Health = 50f;
	public UnityAction<GameObject> OnEnemyDestroyed = delegate { };
	private bool isHit = false;

    
    void OnDestroy()
    {
        if(isHit)
        {
        	OnEnemyDestroyed(gameObject);
        }
    }

    //ketika ada collider lain di luar game object Enemy, bersentuhan dengan collider 
    //dari game object Enemy.
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<Rigidbody2D>() == null)
        return;

        if(col.gameObject.tag == "Burung")
        {
        	isHit = true;
        	Destroy(gameObject);
        }

        else if(col.gameObject.tag == "Obstacle")
        {
        	// hitung damage yang diperoleh
        	float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
        	Health -= damage;

        	if (Health <= 0)
        	{
        		isHit = true;
        		Destroy(gameObject);
        	}
        }
    }
}
