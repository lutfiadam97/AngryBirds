﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
	public GameObject Trail;
	public Burung TargetBird;

	private List<GameObject> trails;
    // atribut trail diinisiasi
    void Start()
    {
        trails = new List<GameObject>();
    }

    
    public void SetBird(Burung bird)
    {
    	TargetBird = bird;
    	for(int i = 0; i < trails.Count; i++)
    	{
    		Destroy(trails[i].gameObject);
    	}

    	trails.Clear();
    }

    public IEnumerator SpawnTrail()
    {
    	trails.Add(Instantiate(Trail, TargetBird.transform.position, Quaternion.identity));
    	yield return new WaitForSeconds(0.1f);

    	if(TargetBird != null && TargetBird.State != Burung.BirdState.HitSomething)
    	{
    		StartCoroutine(SpawnTrail());
    	}
    }
}
