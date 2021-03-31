using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShapeShifter : MonoBehaviour
{


    public Mesh shiftTo;
    public float shiftTime;
    public float distanceFromPlayer;
    public Transform player;
    [SerializeField] public AudioClip breathingSound;
    private MeshFilter meshFilter;
    private int parentID;
    private AudioSource audioSource;
    private bool audio_play;
    private bool audio_toggleChange;
    private bool breathingSoundPlayed = false;



    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        parentID = transform.parent.gameObject.GetInstanceID();
        GameEvents.events.OnShapeShiftTrigger += OnShapeShift;
        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.clip = breathingSound;
        PlayClip(breathingSound);

    }

    // Update is called once per frame
    void Update()
    {
    }


    public void OnEnable()
    {
        GetInPosition();
    }


    ///<summary>Set the position of the shape shifter.</summay>
    public void GetInPosition()
    {
        Vector3 startPosition = player.position - player.forward * distanceFromPlayer;
        transform.parent.position = startPosition;
        transform.parent.LookAt(player);
    }



    ///<summary>Begin shape shift event.</summary>
    private void OnShapeShift(int id)
    {
        if (id == parentID)
            Shift();
    }


    private void Shift()
    {
        GameEvents.events.SinkTrigger();
        //Mesh mesh = Instantiate(shiftTo);
        //Action doShift = () => meshFilter.sharedMesh = mesh;
        Action doShift = () => Destroy(transform.parent.gameObject);
        StartCoroutine(Wait(shiftTime, doShift));
    }


        private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
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


        breathingSoundPlayed = true;
    }
}
