using UnityEngine;
using System.Collections;

public class Tickable : MonoBehaviour {
	int addAmount;
	public Sprite icon;
	public Sprite tappedOut;
	public Transform indicator;
	public SpriteRenderer indicatorRenderer;
	public GameMaster gm;
	GameMaster.RESOURCES resource;
	public int floatTime = 2;
	public int limitZeroForInf;
	public int count = 0;

	void Start () {
		gm = GameMaster.gameMaster;
		indicatorRenderer = indicator.GetComponent<SpriteRenderer> ();
		indicatorRenderer.sprite = icon;
		indicatorRenderer.color = new Color (0, 0, 0, 0);
		//add to List
		GameMaster.gameMaster.AddToList(this);
		City.city.UpdateConsumptions ();
		gm.AddResource (resource, 0);
	}
	public void Tick(){
		if (limitZeroForInf == 0) {
			gm.AddResource (resource, addAmount);
		} else {
			count++;
			if (count <= limitZeroForInf) {
				gm.AddResource (resource, addAmount);
			} else if (count == limitZeroForInf + 1) {
				icon = tappedOut;
				gm.productions [(int)resource] -= addAmount;
				indicatorRenderer.sprite = tappedOut;
				City.city.UpdateConsumptions ();
				gm.AddResource (resource, 0);
			}
		}
		DisplayIndicator ();
	}
	public void DisplayIndicator(){
		StopAllCoroutines ();
		StartCoroutine (FloatIcon());
	}
	public IEnumerator FloatIcon(){
		float xPos = transform.position.x;
		float startY = transform.position.y;
		float endY = transform.position.y + 2;
		float currY = startY;
		float time = 0.0f;
		float dTime;
		float currAlpha = 1.0f;
		Color c;
		while (currY < endY) {
			time += Time.deltaTime;
			dTime = time / floatTime;
			currY = Mathf.Lerp (startY, endY, dTime);
			currAlpha = Mathf.Lerp (1, 0, dTime);
			c = new Color (255, 255, 255, currAlpha);
			indicatorRenderer.color = c;
			indicator.position = new Vector2 (xPos, currY);
			yield return null;
		}
	}

	public void SetAmount(int a){
		addAmount = a;
	}
	public void SetLimit(int a){
		limitZeroForInf = a;
	}
	public void SetResource(GameMaster.RESOURCES r){
		resource = r;
	}
}
