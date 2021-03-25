using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class NC_CharacterController : MonoBehaviour
{
    public float speed;
    public float runSpeed;
    public float stepDelay;
    public float jumpSpeed;
    public float gravity;
    public Camera playerCamera;
    public float lookSpeed;
    public float lookXLimit;
    public AudioClip[] WalkOnWoodSound;
    public AudioClip[] RunOnWoodSound;
    private CharacterController characterController;
    private AudioSource audioSource;
    private HeadBob headBob;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 rotation = Vector2.zero;
    private float initialSpeed;
    private float moveSpeed;
    private bool canMove = true;
    private bool audio_play;
    private bool audio_toggleChange;
    private bool stepping = false;
    private bool isRunning = false;
    private Vector3 currentVelocity;
    private Vector3 previousVelocity;
    private float stepOffset;
    private float walkingMoveSpeed;
    private float RunningMoveSpeed;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        headBob = playerCamera.GetComponent<HeadBob>();
        rotation.y = transform.eulerAngles.y;
        //Keep the mouse inside the game screen
        Cursor.lockState = CursorLockMode.Locked;
        currentVelocity = characterController.velocity;
        previousVelocity = currentVelocity;
        stepOffset = characterController.stepOffset;
        initialSpeed = speed;
        walkingMoveSpeed = ((1/speed)+stepOffset * speed);
        RunningMoveSpeed = walkingMoveSpeed * 2;
        moveSpeed = walkingMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            //Recalculate direction
            try
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 right = transform.TransformDirection(Vector3.right);
                float curSpeedX = canMove ? moveSpeed * Input.GetAxis("Vertical") : 0;
                float curSpeedY = canMove ? moveSpeed * Input.GetAxis("Horizontal") : 0;
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }


            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }


            if (Input.GetKeyDown("left shift"))
            {
                speed = runSpeed;
                moveSpeed = RunningMoveSpeed;
                isRunning = true;
            }
            if (Input.GetKeyUp("left shift"))
            {
                speed = initialSpeed;
                moveSpeed = walkingMoveSpeed;
                isRunning = false;
            }
        }


        //Apply gravity. Gravity is multiplied by deltaTime twice.
        moveDirection.y -= gravity * Time.deltaTime;



        //Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        currentVelocity = characterController.velocity;

        if (currentVelocity != previousVelocity && canMove)
        {
            Step();
        }
        else
        {
            headBob.Idle(speed);
        }


        //Player and Camera rotation
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, rotation.y);


        //TODO other player keys should be added below.


        previousVelocity = characterController.velocity;
    }


    ///<summary>Do step sounds and head bobbing movement.</summary>
    private void Step(){
        if (!stepping)
        {
            int stepSound = 0;
            if (!isRunning)
                stepSound = UnityEngine.Random.Range(0, WalkOnWoodSound.Length);
            else
            {
                stepSound = UnityEngine.Random.Range(0, RunOnWoodSound.Length);
            }
            float stepSpeed = (1/speed) + stepOffset + stepDelay;
            stepping = true;
            PlayClip(WalkOnWoodSound[stepSound]);
            Action stepOff = ()=> stepping = false;
            StartCoroutine(Wait(stepSpeed, stepOff));
            headBob.Bounce(speed);
        }
    }


    ///<summary>Play audio clip once.</summary>
    ///<param name="clip">AudioClip to play.</param>
    private void PlayClip(AudioClip clip)
    {
        audio_play = true;
        audio_toggleChange = true;
        //Check if you just set the toggle to positive.
        if (audio_play == true && audio_toggleChange == true)
        {
            audioSource.clip = clip;
            audioSource.Play();
            audio_toggleChange = false;
        }
        //Check if you just set the toggle to false
        if (audio_play == false && audio_toggleChange == true)
        {
            //Stop the audio
            audioSource.Stop();
            //Ensure audio doesn't play more than once
            audio_toggleChange = false;
        }
    }


    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
