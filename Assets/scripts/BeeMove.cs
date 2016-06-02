using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Linq;
[RequireComponent(typeof(AudioSource))]
public class BeeMove : MonoBehaviour
{
    //audio related
    public AudioMixerSnapshot aliveSnapshot;
    public AudioMixerSnapshot deathSnapshot;
    public AudioMixerSnapshot collectSnapshot;
    public AudioMixerSnapshot honeyGrabSnapshot;
    public AudioSource deathAudiosource;
    public AudioSource collectAudioSource;
    public AudioSource honeyGrabAudioSource;
    public AudioClip deathAudioClip;
    public AudioClip collectClip;
    public AudioClip honeyGrabClip;
    public SpriteRenderer spriteRenderer;
    public Sprite[] timerSprites;
    public float TotalRestartTimer = 3f;

    private Animator animator;
    private bool isDying = false;
    private bool iscollected = false;
    private float ScoreUpdateTime = 0.5f;
    private float remainingTime;
    private Rigidbody2D rb;
    private float leftBorder;
    private float rightBorder;
    private bool isGrabbedHoney;
    private float beeVelocity = 4f;
    private float rotationsPerMinute = 10f;
    private float collectedHoney;
    private float currentScore;
    private bool isGameOvershown = false;
    private UIManager uiManager;
    private Camera camera1;
    private float honeyGrabTimer = 1f;
    private float remainingTimeForHoneyGrab;
    private float screenWidth;
    private float screenHeight;
    private float currentTime;
    private float startTime;
    //	private BackgroundMove backgroundMove;
    private BackgroundScrolling backgroundMove;

    private float meters;
    private Hashtable honeyGrabObjects;
    private bool isPreviousScoreAdded;
    private float previousScore;
    private float showGameOverTimer = 3f;
    private float timerToSetSprite;
    private bool isScorePosted;
    void Awake()
    {

        //request for components
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.setHoneyCollectedText(PlayerPrefernces.getHoneyAvailable());
        camera1 = GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        honeyGrabObjects = new Hashtable();
        //		//initialize powers
        //		PlayerPrefernces.setInitialPowers ();
        //		PlayerPrefernces.setHoneyAvailable (20000);
        //		for (int i = 0; i < 100; i++) {
        //			PlayerPrefernces.increaseProtectionPower ();
        //			PlayerPrefernces.increaseMagnetsAvailable ();
        //			PlayerPrefernces.increaseHighSpeedPowerAvailable ();
        //		}
        //set initialze powers
        if (PlayerPrefernces.isFirstTime())
        {
            PlayerPrefernces.setFirstTime(true);
            PlayerPrefernces.setInitialPowers();
        }

        // get available honey
        collectedHoney = PlayerPrefernces.getHoneyAvailable();

        //local player stats
        PlayerCurrentStats localStats = new PlayerCurrentStats();
        localStats.currentScore = GlobalControl.Instance.playerCurrentStats.currentScore;


        //set default score
        previousScore = localStats.currentScore;
        uiManager.setCurrentScoreText(previousScore);
        //get screen width and hieght
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        //start time
        startTime = Time.time;
        //get scrollspeed
        backgroundMove = GameObject.Find("background").GetComponent<BackgroundScrolling>();

        //check if restoring  playing after death
        if (previousScore > 0)
        {
            uiManager.OnTutorialCompleted();

        }
        //unlock welcome achievement
        if (uiManager.IsUserAuthenticatedForPlayServices())
        {
            HandleAchievementsAndLeaderboard.Instance.UnlockAchievement(GPGSIDs.achievement_swagatham);
        }

    }
    // Use this for initialization
    void Start()
    {


        //disable screen lock
        Screen.sleepTimeout = SleepTimeout.NeverSleep;


        //set timer for score update
        remainingTime = ScoreUpdateTime;


        //set timer for grabbing honey
        remainingTimeForHoneyGrab = 0;
        //get left and right screen borders 

        float dist = (transform.position - Camera.main.transform.position).z;
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;


    }

    // Update is called once per frame
    void Update()
    {

        int activePower = PlayerPrefernces.getCurrentActivePower();


        //updating score && audio
        if (uiManager.isTutorialCompleted())
        {

            //control physics if powers are activated
            if (activePower != 2 && !uiManager.isGamePaused())
            {

                rb.isKinematic = false;
                Time.timeScale = 1;
            }
            if (activePower == 2)
            {
                increaseGameSpeed();
            }
            else if (activePower == 1)
            {
                rb.isKinematic = false;
                //				isGrabbedHoney = true;
                grabHoney();
                //				remainingTimeForHoneyGrab = honeyGrabTimer;

            }

            if (!isDying && !iscollected)
            {
                //			
                aliveSnapshot.TransitionTo(0.25f);

                //remainingTime -= Time.deltaTime;
                float scrollSpeed = backgroundMove.getScrollSpeed();
                currentTime = Time.time;
                remainingTime -= Time.deltaTime;


                meters = previousScore + (currentTime - startTime) * scrollSpeed;

                //	uiManager.setCurrentScoreText (Mathf.Round (meters * 100f) / 100f);
                if (remainingTime < 0)
                {
                    currentScore = Mathf.Round(meters * 100f) / 100f;
                    uiManager.setCurrentScoreText(currentScore);
                    remainingTime = ScoreUpdateTime;
                }
                isPreviousScoreAdded = true;
            }

            //checking bee position
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPosition.x > Screen.width || screenPosition.x < 0)
            {
                isDying = true;

                deathSnapshot.TransitionTo(0.25f);
                StartCoroutine(playDeathAudio());
                die();
                //set timescale
                Time.timeScale = 1;

                //save current score data
                saveCurrentGameData();

                //save collected honey
                PlayerPrefernces.setHoneyAvailable(collectedHoney);
            }
            float temp = Mathf.Clamp(transform.position.x, leftBorder, rightBorder);
            transform.position = new Vector3(temp, transform.position.y, transform.position.z);


        }

    }

    void FixedUpdate()
    {

        //update bee position
        if (Input.touchCount > 0)
        {
            Touch touchInput = Input.GetTouch(0);
            if (touchInput.position.x < screenWidth / 2 && touchInput.phase == TouchPhase.Stationary)
            {

                moveBeeLeft();
                //rotateBeeLeft ();
            }
            else if (touchInput.position.x >= screenWidth / 2 && touchInput.phase == TouchPhase.Stationary)
            {
                moveBeeRight();
                //rotateBeeRight();
            }
        }

    }

    void LateUpdate()
    {


        if (isGameOvershown)
        {
            //save current score data
            saveCurrentGameData();
            //change animation
            animator.SetBool("isDying", true);

            //check if google play services enabled  
            if (uiManager.IsUserAuthenticatedForPlayServices() && !isScorePosted)
            {
                //post score
                postScoreToLeaderboard();

                //unlock achievement sipper
                if (collectedHoney >= 25 && collectedHoney < 50)
                {
                    HandleAchievementsAndLeaderboard.Instance.UnlockAchievement(GPGSIDs.achievement_sipper);

                }//unlock achievement collector 
                else if (collectedHoney >= 50 && collectedHoney < 100)
                {
                    HandleAchievementsAndLeaderboard.Instance.UnlockAchievement(GPGSIDs.achievement_collector);
                }//unlock acievement swiller 
                else if (collectedHoney > 100)
                {
                    HandleAchievementsAndLeaderboard.Instance.UnlockAchievement(GPGSIDs.achievement_swiller);
                }

                isScorePosted = true;

            }
            //set back game speed
            Time.timeScale = 1;
            showGameOverTimer -= Time.deltaTime;
            if (showGameOverTimer < 0)
            {

                SceneManager.LoadSceneAsync("gameoverscene");
                showGameOverTimer = ScoreUpdateTime;

            }


        }
    }
    //	void rotateBeeLeft(){
    //		Quaternion maxRotationAngle = Quaternion.Euler (0f, 0f, 10f);
    //		transform.rotation = Quaternion.Lerp (transform.rotation, maxRotationAngle, Time.deltaTime *5f);
    //	}
    //	void rotateBeeRight(){
    //		Quaternion maxRotationAngle = Quaternion.Euler (0f, 0f, -10f);
    //		transform.rotation = Quaternion.Lerp (transform.rotation, maxRotationAngle, Time.deltaTime *5f);
    //	}
    public void moveBeeLeft()
    {

        if (transform.position.x > leftBorder)
        {

            transform.Translate(Vector3.left * 8f * Time.deltaTime);

        }

    }
    public void moveBeeRight()
    {

        //
        if (transform.position.x < rightBorder)
        {
            transform.Translate(Vector3.right * 8f * Time.deltaTime);

        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("obstacle"))
        {
            activateCollision();
        }
        else if (col.CompareTag("honey"))
        {
            collectedHoney += 1;
            //set collected honey
            uiManager.setHoneyCollectedText(collectedHoney);

            iscollected = true;
            collectSnapshot.TransitionTo(0);
            collectAudioSource.clip = collectClip;
            collectAudioSource.Play();
            iscollected = false;
            Destroy(col.gameObject);
        }
    }

    void die()
    {
        GameObject beeObject = GameObject.FindGameObjectWithTag("Player");
        //		SpinObject (beeObject);
        isGameOvershown = true;
        StartCoroutine(SpinObject(beeObject));

    }

    IEnumerator SpinObject(GameObject go)
    {
        rb.gravityScale = 1.5f;
        rb.freezeRotation = false;
        float duration = 30f;
        float elapsed = 0f;
        float spinSpeed = 200f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            go.transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2);
    }
    IEnumerator playDeathAudio()
    {
        if (deathAudiosource != null)
        {
            deathAudiosource.clip = deathAudioClip;
            deathAudiosource.Play();
        }

        yield return new WaitForSeconds(5);
        Destroy(deathAudiosource);
        Destroy(gameObject);

    }
    public Vector3 getCurrentBeePosition()
    {
        return transform.position;
    }

    void increaseGameSpeed()
    {
        rb.isKinematic = true;
        Time.timeScale = 5;

    }

    bool hasChild(GameObject gameobject)
    {


        if (gameobject.transform.Find("honey_sprite").gameObject != null)
        {

            return true;
        }
        return false;

    }


    void saveCurrentGameData()
    {
        PlayerCurrentStats localPlayer = new PlayerCurrentStats();
        localPlayer.currentScore = currentScore;
        localPlayer.availableHoney = collectedHoney;
        localPlayer.currentSpeed = backgroundMove.getScrollSpeed();
        GlobalControl.Instance.playerCurrentStats = localPlayer;
    }

    void grabHoney()
    {
        //float step = pullForce * Time.deltaTime;
        GameObject[] honeyObjects = GameObject.FindGameObjectsWithTag("honey");
        if (honeyObjects.Length > 0)
        {

            Vector3 offset;
            float magnitudeSquared;
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 100))
            {

                if (collider.tag == "honey")
                {
                    if (!honeyGrabObjects.ContainsKey(collider.GetInstanceID()))
                    {
                        honeyGrabObjects.Add(collider.GetInstanceID(), false);

                    }
                    if (honeyGrabObjects.ContainsKey(collider.GetInstanceID()) && !(bool)honeyGrabObjects[collider.GetInstanceID()])
                    {

                        //collectedHoney += 1;
                        uiManager.setHoneyCollectedText(collectedHoney);
                        offset = transform.position - collider.transform.position;
                        offset.z = 0;
                        collider.GetComponent<Rigidbody2D>().AddForce((transform.position - collider.transform.position) * 5000f * Time.deltaTime);
                    }
                    honeyGrabObjects[collider.GetInstanceID()] = true;

                }

                //collider.attachedRigidbody.AddForce (20 * offset.normalized / magnitudeSquare,ForceMode2D.Force);

            }

            //				isGrabbedHoney = false;


        }
    }

    void playGrabHoneySound()
    {
        honeyGrabSnapshot.TransitionTo(0);
        honeyGrabAudioSource.clip = honeyGrabClip;
        honeyGrabAudioSource.Play();
    }

    void activateCollision()
    {


        int activePower = PlayerPrefernces.getCurrentActivePower();

        if (activePower == 0)
        {

            rb.isKinematic = true;

        }
        else if (activePower == 1)
        {

            rb.isKinematic = false;

            isDying = true;

            die();

            deathSnapshot.TransitionTo(0.25f);

            StartCoroutine(playDeathAudio());

            //save collected honey

            PlayerPrefernces.setHoneyAvailable(collectedHoney);

        }

        else if (activePower == -1)
        {

            rb.isKinematic = false;

            isDying = true;

            die();

            deathSnapshot.TransitionTo(0.25f);

            StartCoroutine(playDeathAudio());
            //save collected honey

            PlayerPrefernces.setHoneyAvailable(collectedHoney);

        }


    }

    //	public void SetRestartSprites(){
    //
    //		spriteRenderer.gameObject.SetActive (true);
    //		timerToSetSprite -= Time.deltaTime;
    //		if (timerToSetSprite <= 3 && timerToSetSprite > 2) {
    //			spriteRenderer.sprite = timerSprites [0];
    //		}
    //		if (timerToSetSprite <= 2 && timerToSetSprite > 1) {
    //			spriteRenderer.sprite = timerSprites [1];
    //		} else if (timerToSetSprite >= 1 && timerToSetSprite > 0) {
    //			spriteRenderer.sprite = timerSprites [2];
    //		} else {
    //			return;
    //		}
    //
    //	}
    void postScoreToLeaderboard()
    {

        float highScore = PlayerPrefernces.LoadHighscore();
        if (uiManager.IsUserAuthenticatedForPlayServices() && currentScore > highScore)
        {
            HandleAchievementsAndLeaderboard.Instance.PostScoreToLeaderboard(currentScore, GPGSIDs.leaderboard_distance_travelled);
        }
    }


}

