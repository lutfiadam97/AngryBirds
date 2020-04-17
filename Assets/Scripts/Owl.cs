using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Burung
{
	private bool explode = false;
	[SerializeField] private GameObject explosion;

	public void Boom()
	{
		if(State == BirdState.Thrown && !explode)
		{
			// show effect
			//get nearby objects
				//add force
				//damage
			//remove object
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
			explode = true;

		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Boom();
	}
}
