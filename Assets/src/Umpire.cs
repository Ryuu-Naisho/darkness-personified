using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class Umpire : MonoBehaviour
{


    public Transform player;
    public AudioClip shakeSound;
    public AudioClip smashDoorSound;
    public int maxBadItems;
    public int waitTime;
    public float minimumDistance;
    private Item item;
    private Inventory inventory;
    private NC_Tags tags;
    private wCalc wcalc;
    private GameObject apparition;
    private List<int> freakyActions = new List<int>(){1,2,3,4};
    private bool gameOver = false;
    private AudioSource audioSource;
    private bool audio_play;
    private bool audio_toggleChange;



    // Start is called before the first frame update
    void Start()
    {
        this.tags = new NC_Tags();
        this.wcalc = new wCalc();
        this.audioSource = GetComponent<AudioSource>();
        inventory = player.GetComponent<Inventory>();
        HideApparition();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.HasKey() && !gameOver)
            SceneManager.LoadScene("GameWon");


        if (inventory.HasNewItem() && !gameOver)
        {
            item = inventory.GetLastItem();
            if (item.Memory == this.tags.Bad)
                GetFreaky();
            else if (item.Memory == this.tags.Good)
                Debug.Log("Do something good.");
            inventory.NewItemAcknowledge();
        }


        
        if (gameOver)
            SceneManager.LoadScene("GameOver");
    }


    ///<summary>Randomly select weird things to happen.</summary>
    private void GetFreaky()
    {
        if (freakyActions.Count == 0)
        {
            gameOver = true;
            return;
        }
        int availableActions = freakyActions.Count - 1;
        int index = UnityEngine.Random.Range(0,availableActions);
        int seed = freakyActions[index];
        switch(seed)
        {
            case 1:
                DoDoors();
                break;
            case 2:
                DoLights();
                break;
            case 3:
                DoApparition();
                break;
            case 4:
                DoShakeHouse();
                break;
            default:
                DoShakeHouse();
                break;
        }


        freakyActions.Remove(seed);
    }


    ///<summary>Cause doors to open and close.</summary>
    private void DoDoors()
    {
        GameObject[] doors;
        doors = GameObject.FindGameObjectsWithTag(this.tags.Door);
        List<float> distances = new List<float>();
        GameObject closestDoor;

            foreach (GameObject door in doors)
            {
                float distance = Vector3.Distance(player.position, door.transform.position);
                distances.Add(distance);
            }


            closestDoor = wcalc.GetClosest(distances, doors);

            int id;
            DoorController doorController = closestDoor.GetComponentInChildren<DoorController>();
            id = doorController.GetID();
            GameEvents.events.DoorwayTriggerEnter(id);
            PlayClip(smashDoorSound);
            Action closeDoor = ()=> GameEvents.events.DoorwayTriggerExit(id);
            StartCoroutine(Wait(waitTime, closeDoor));
    }



    ///<summary>Cause lights to flicker.</summary>
    private void DoLights()
    {
        GameEvents.events.LightFlickerTrigger();
    }


    ///<summary>Summon the apparition for gameplay.</summary>
    private void DoApparition()
    {
        this.apparition.SetActive(true);
    }


    ///<summary>Deactivate apparition during gameplay.</summary>
    private void HideApparition()
    {

        GameObject[] shapeShifters;
        shapeShifters = GameObject.FindGameObjectsWithTag(this.tags.ShapeShifter);
        foreach (GameObject shapeShifter in shapeShifters)
        {
            if (shapeShifter.transform == shapeShifter.transform.root)
            {
            this.apparition = shapeShifter;
            shapeShifter.SetActive(false);
            }
        }
    }


    private void DoShakeHouse()
    {
            PlayClip(shakeSound);
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



    private IEnumerator Wait(int time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
