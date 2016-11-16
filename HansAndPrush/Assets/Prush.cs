using UnityEngine;
using System.Collections;

public class Prush : MonoBehaviour {
	Revolver rev;
	CentralNervousSystem hans;
	Transform pursueTarget;
	Vector2 subDest;
	public int minEnemyDistance;
	public int minPointOfInterestDistance;
	public int minAttackDistance;
	public int xOffset;
	public int yOffset;
	public int xBobAmnt;
	public int yBobAmnt;
	public int timeToDestination;
	public bool pursueRunning = false;
	public Transform closestEnemy;
	// Use this for initialization
	void Start () {
		rev = GameObject.FindGameObjectWithTag ("Revolver").GetComponent<Revolver> ();
		hans = GameObject.FindGameObjectWithTag ("Player").GetComponent<CentralNervousSystem> ();
		StartCoroutine ("GetClosestEnemy");
		StartCoroutine ("Prioritize");
		StartCoroutine ("Bob");
		StartCoroutine ("ProcessInput");
	}
	public IEnumerator ProcessInput(){
		while(true){
			if(Input.GetAxis("Action1") > 0){
				if(closestEnemy != null){
					if((closestEnemy.position - transform.position).sqrMagnitude < minAttackDistance * minAttackDistance){
						rev.Attack (closestEnemy);
					}
				}
			}
			yield return null;
		}
	}

	public IEnumerator GetClosestEnemy(){
		while (true) {
			Vector2 posA = hans.transform.position;
			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			if (enemies.Length == 1) {
				closestEnemy = enemies [0].transform;
			} else if (enemies.Length == 0) {
				closestEnemy = null;
			} else {
				closestEnemy = enemies [0].transform;
				float smallestDistance = ((Vector2) closestEnemy.position - (Vector2) posA).sqrMagnitude;
				for (int i = 1; i < enemies.Length; i++) {
					float newDist = ((Vector2) enemies [i].transform.position - (Vector2) posA).sqrMagnitude;
					if (newDist < smallestDistance) {
						closestEnemy = enemies [i].transform;
						smallestDistance = newDist;
					}
					yield return null;
				}
			}
			yield return null;
		}
	}
	public IEnumerator Prioritize(){
		Transform oldTarget;
		while (true) {
			oldTarget = pursueTarget;
			if (closestEnemy == null) {
				pursueTarget = hans.transform;
			} else {
				pursueTarget = closestEnemy;
			}
			if (pursueTarget != oldTarget) {
				if (pursueRunning) {
					StopCoroutine (Pursue ());
				}
				StartCoroutine (Pursue ());
				oldTarget = pursueTarget;
			}
			yield return null;
		}
	}
	public IEnumerator Pursue(){
			pursueRunning = true;
			if (pursueTarget != null) {
			Vector2 location = transform.position;
			float timer = 0.0f;
			bool escape = true;
			Vector2 destination = new Vector2 (pursueTarget.position.x + xOffset, pursueTarget.position.y + yOffset);
			while ((Vector2)transform.position != destination && escape){
				try{
				destination = new Vector2 (pursueTarget.position.x + xOffset, pursueTarget.position.y + yOffset);
				timer += Time.deltaTime;
				float ratio = timer / timeToDestination;
				subDest = new Vector2 (Mathf.SmoothStep(location.x, destination.x, ratio), Mathf.SmoothStep(location.y, destination.y, ratio));
				}catch(MissingReferenceException){
					escape = false;
				}
			yield return new WaitForFixedUpdate();
		}
		}
		pursueRunning = false;
	}
	public IEnumerator Bob(){
		while(true){
		float amount = Mathf.Sin (Time.realtimeSinceStartup);
			Vector2 newPos = new Vector2 (subDest.x + amount, subDest.y + amount);
			transform.position = newPos;
			yield return new WaitForFixedUpdate ();
	}
}
}
