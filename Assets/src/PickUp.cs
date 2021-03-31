using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PickUp : MonoBehaviour
{


    public Transform player;
    public int minDistance;
    public string defaultHint;
    public string hint;
    public bool interactable;
    public bool displayHint;
    public int ID;
    public AudioClip[] clips;
    private NC_GUI gui;
    private Inventory inventory;
    private bool showingHint = false;
    private AudioSource audioSource;
    private bool audio_play;
    private bool audio_toggleChange;



    // Start is called before the first frame update
    void Start()
    {
        hint = defaultHint;
        gui = player.GetComponent<NC_GUI>();
        inventory = player.GetComponent<Inventory>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


        if (displayHint)
        {



            RaycastHit _hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, minDistance))
            {
                if (_hit.transform.tag == tag)
                {
                    int hitID = _hit.transform.gameObject.GetComponent<PickUp>().ID;
                    if (hitID == this.ID)
                    {
                        if (!showingHint)
                        {
                            gui.DisplayHint(hint);
                            showingHint = true;
                        }


                        if (interactable)
                        {
                            if (Input.GetKeyDown("e"))
                                Interact();
                        }
                    }
                }
            }

            else
            {
                //Clear any hints.
                if(showingHint)
                {
                    gui.ClearHint();
                    showingHint = false;
                }
            }
        }   
    }


    public void Interact()
    {
        int index = UnityEngine.Random.Range(0, clips.Length);
        PlayClip(clips[index]);
        gui.ClearHint();
        interactable = false;
        inventory.Add(ID);
        transform.position += Vector3.down * 100;
        Action hide = () => gameObject.SetActive(false);
        StartCoroutine(Wait(1, hide));
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
