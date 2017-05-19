using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public AudioClip crack;
	private int maxHits;
	private int timesHit;
	
	public static int breakableCount = 0;
	private bool isBreakable;

	public Sprite[] hitSprites;

	private LevelManager levelManager;
	
	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		timesHit = 0;
		maxHits = hitSprites.Length +1;
		
		//Keep trace of breakable bricks
		isBreakable = (this.tag == "Breakable");
		if (isBreakable){
			breakableCount++;
		}
	}
	
	void OnCollisionEnter2D(){
		AudioSource.PlayClipAtPoint(crack, transform.position);
		if(isBreakable){
			HandleHits();
		}
	}
	
	void HandleHits(){
		timesHit++;
		
		if(timesHit >= maxHits){
			breakableCount--;
			levelManager.BrickDestroyed();
			Destroy(gameObject);
		} else {
			LoadSprites();
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void LoadSprites(){
		int spriteIndex = timesHit - 1 ;
		if(hitSprites[spriteIndex]){
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		
	}
	
	// TODO Remove this method once we can actually win!
	void SimulateWin() {
		levelManager.LoadNextLevel();
	}
}
