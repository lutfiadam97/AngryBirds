using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public Ketapel Ketapel;
	public TrailController TrailController;
	public List<Burung> Birds;
	public List<Lawan> Enemies;
	private Burung shotBird;
	public BoxCollider2D TapCollider;
	// Collider tersebut akan kita nyalakan, bila burung sudah dilontarkan, dan akan dimatikan bila ada burung baru 
	//yang ditempatkan pada ketapel dalam kondisi Idle.

	private bool isGameEnded = false;
	private UIControl uiControl;
    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < Birds.Count; i++)
        {
        	Birds[i].OnBirdDestroyed += ChargeBird;
        	Birds[i].OnBirdShot += AssignTrail;
        }

        for(int i = 0; i < Enemies.Count; i++)
        {
        	Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        Ketapel.InitiateBird(Birds[0]);
        shotBird = Birds[0];
        uiControl = GameObject.Find("Canvas").GetComponent<UIControl>();
    }

    
    public void ChargeBird()
    {
    	TapCollider.enabled = false;

    	if(isGameEnded)
    	{
    		return;
    	}

        Birds.RemoveAt(0);

        if(Birds.Count > 0)
        {
        	Ketapel.InitiateBird(Birds[0]);
        	shotBird = Birds[0];
        }
        
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
    	for(int i = 0; i < Enemies.Count; i++)
    	{
    		if(Enemies[i].gameObject == destroyedEnemy)
    		{
    			Enemies.RemoveAt(i);
    			break;
    		}
    	}

    	if(Enemies.Count == 0)
    	{
    		isGameEnded = true;
    		uiControl.endGame();
    	}
    }

    public void AssignTrail(Burung bird)
    {
    	TrailController.SetBird(bird);
    	StartCoroutine(TrailController.SpawnTrail());
    	TapCollider.enabled = true;
    }

    //Dalam fungsi OnMouseUp, kita akan memberitahukan burung yang saat ini 
    //dilontarkan bahwa player melakukan tap pada screen 
    //dengan memanggil fungsi OnTap dalam burung.

    void OnMouseUp()
    {
    	if(shotBird != null)
    	{
    		shotBird.OnTap();
    	}
    }
}
