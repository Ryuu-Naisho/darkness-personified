using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class DoorController : MonoBehaviour
{
    public AudioClip[] doorCreakSound;
    public AudioClip doorClosedSound;
    private Vector3 initialPosition;
    private int ID;
    private Animator animator;
    private AudioSource audioSource;
    private bool audio_play;
    private bool audio_toggleChange;



    // Start is called before the first frame update
    void Start()
    {
        ID = gameObject.GetInstanceID();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GameEvents.events.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.events.onDoorwayTriggerExit += OnDoorwayClose;
    }

    // Update is called once per frame
    void Update()
    {
    }


    ///<summary>Set door to open position.</summary>
    ///<param name="id">int id, instance id of the gameObject.</param>
    private void OnDoorwayOpen(int id)
    {
        if (id == ID)
        {
            int index = UnityEngine.Random.Range(0, doorCreakSound.Length);
            PlayClip(doorCreakSound[index]);
            animator.SetTrigger("Open");
        }
    }


    ///<summary>Set door to closed position.</summary>
    ///<param name="id">int id, instance id of the gameObject.</param>
    private void OnDoorwayClose(int id)
    {
        if (id == ID)
        {
            PlayClip(doorClosedSound);
            animator.SetTrigger("Close");
        }
    }


    ///<summary>Get the ID of the parent object.</summary>
    public int GetID()
    {
        return this.ID;
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
}
