using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ketapel : MonoBehaviour
{
	public CircleCollider2D Collider;
	private Vector2 startPos;
	public LineRenderer Trajectory;

	[SerializeField] private float radius = 0.75f;
	[SerializeField] private float throwSpeed = 30f;

	private Burung _bird;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    void OnMouseUp()
    {
    	Collider.enabled = false;
    	Vector2 velocity = startPos - (Vector2)transform.position;
    	float distance = Vector2.Distance(startPos, transform.position);

    	_bird.Shoot(velocity, distance, throwSpeed);

    	// kembalikan ketapel ke posisi awal
    	gameObject.transform.position = startPos;
    	Trajectory.enabled = false;
    }

    public void InitiateBird(Burung bird)
    {
    	_bird = bird;
    	_bird.MoveTo(gameObject.transform.position, gameObject);
    	Collider.enabled = true;
    }

    void OnMouseDrag()
    {
    	// mengubah posisi mouse ke world position
    	Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    	// Hitung supaya 'karet' ketapel berada dalam radius yang ditentukan
    	Vector2 dir = p - startPos;
    	if (dir.sqrMagnitude > radius)
    		dir = dir.normalized * radius;
    		transform.position = startPos + dir;

    		float distance = Vector2.Distance(startPos, transform.position);

    		if (!Trajectory.enabled)
    		{
    			Trajectory.enabled = true;
    		}

    		DisplayTrajectory(distance);
    }

    
    void DisplayTrajectory(float distance)
    {
    	if(_bird == null)
    	{
    		return;
    	}

    	Vector2 velocity = startPos - (Vector2)transform.position;
    	int segmentCount = 5;
    	Vector2[] segments = new Vector2[segmentCount];

    	// posisi awal trajectory merupakan posisi mouse dari player saat ini
    	segments[0] = transform.position;

    	// velocity awal
    	Vector2 segVelocity = velocity * throwSpeed * distance;

    	for(int i = 1; i < segmentCount; i++)
    	{
    		float elapsedTime = i * Time.fixedDeltaTime * 5;
    		segments[i] = segments[0] + segVelocity * elapsedTime + 0.5f * Physics2D.gravity * Mathf.Pow(elapsedTime, 2);
    	}

    	Trajectory.positionCount = segmentCount;
    	for(int i = 0; i < segmentCount; i++)
    	{
    		Trajectory.SetPosition(i, segments[i]);
    	}
    }
}
