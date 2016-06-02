using UnityEngine;
using System.Collections;
using System.Threading;
public class ObstacleGenerator : MonoBehaviour {

	public float spawnIncreaseRate;
	public float spawnTimer;
	private float remainingTime;
	private int obstacleNo;

	private Thread mThread;
	private float startTime;
	private float currentTime;
	private const float spawnIncreaseTime=10;
	private float intervalTimer;
	private float nextSpawn;
	private float resetTimer;

	private float leftBorder;
	private float rightBorder;
	private float previousObstaclePosY;
	void Awake(){
		intervalTimer = spawnIncreaseTime;
		resetTimer = 10f;

	}
	// Use this for initialization
	void Start () {
		transform.Rotate (0, 0, 0);
		remainingTime = 0;
		startTime = Time.time;
//		InvokeRepeating ("ResetObstacleToAvoidMemoryLeak", resetTimer, resetTimer);
		//StartCoroutine (spawnObstacles ());
		//		InvokeRepeating("spawnObstacles",0,spawnTimer);
		float dist = (transform.position - Camera.main.transform.position).z;
		leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
		//
		previousObstaclePosY=0;
		if (PlayerPrefernces.getIsMovementtutorialComplete ()) {
			InvokeRepeating ("InstantiateObstacle", 3, 1f);
		}

	}

	// Update is called once per frame
	void Update () {
		currentTime = Time.time;
		intervalTimer -= Time.deltaTime;
		if (intervalTimer < 0 && spawnTimer > 1.5f) {
			spawnTimer -= spawnIncreaseRate;
			intervalTimer = spawnIncreaseTime;
		} else if (spawnTimer < 1f) {
			spawnTimer = 1f;
		}

	}

	int getRandomInteger(int[] randomInteger){
		return randomInteger [Random.Range (0, randomInteger.Length)];
	}


//	void spawnObstacles(){
//
//		mThread = new Thread (InstantiateObstacle);
//		mThread.Start ();
//	
//	}

	void InstantiateObstacle(){

		PoolType type=PoolType.Obstacle;
		Vector3 obstaclePos=Vector3.zero;
		int[] randomInteger = { -2, 2 };
		if (Time.time > nextSpawn && PlayerPrefernces.getIsCollectHoneytutorialComplete()) {
			

			int randomNumber = getRandomInteger (randomInteger);

			if (randomNumber == -2) {

				if (transform.rotation.eulerAngles.y == 180) {

					transform.Rotate (180, 0, 180);
				}
			} else if (randomNumber == 2) {
				if (transform.rotation.eulerAngles.y != 180) {

					transform.Rotate (180, 0, 180);
				}
			}
			//			obstacleNo = Random.Range (0, obstacle.Length);
		

			float currentRandomYpos = Random.Range (2, 8);

			float diff = Mathf.Abs (previousObstaclePosY - currentRandomYpos);

			if (diff >= 1) {
				if (randomNumber == -2) {
					obstaclePos = new Vector3 (leftBorder, currentRandomYpos, 0);

				} else if (randomNumber == 2) {
					obstaclePos = new Vector3 (rightBorder, currentRandomYpos, 0);

				}
				string obstacleName = null;
				int RandomObstacleNo = Random.Range (1, 11);
				switch (RandomObstacleNo) {
				case 1:
					obstacleName = "Obstacle";
					type = PoolType.Obstacle;
					break;
				case 2:
					obstacleName = "Obstacle1";
					type = PoolType.Obstacle1;
					break;
				case 3:
					obstacleName = "Obstacle2";
					type = PoolType.Obstacle2;

					break;
				case 4:
					obstacleName = "Obstacle3";
					type = PoolType.Obstacle3;
					break;
				case 5:
					obstacleName = "Obstacle1";
					type = PoolType.Obstacle1;
					break;
				case 6:
					obstacleName = "Obstacle5";
					type = PoolType.Obstacle5;
					break;
				case 7:
					obstacleName = "Obstacle6";
					type = PoolType.Obstacle6;
					break;
				case 8:
					obstacleName = "Obstaclewithhoney";
					type = PoolType.ObstaclewithHoney;
					break;
					
				case 9:
					obstacleName = "Obstaclewithhoney1";
					type = PoolType.ObstaclewithHoney1;
					break;
				case 10:
					obstacleName = "Obstaclewithhoney2";
					type = PoolType.ObstaclewithHoney2;
					break;
				}


				ObjectPoolManager.Instance.Spawn (type, obstaclePos, transform.rotation);
				nextSpawn = Time.time + spawnTimer;

			} else {
			}
			previousObstaclePosY = currentRandomYpos;
		}
			
		
		
	}
	void ResetObstacleToAvoidMemoryLeak(){
		ObjectPoolManager.Instance.Reset ();
	}
}

