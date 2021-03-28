using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umpire : MonoBehaviour
{


    public Transform player;
    public int maxBadItems;
    public int waitTime;
    public float minimumDistance;
    private Item item;
    private Inventory inventory;
    private int collectedItems = 0;
    private NC_Tags tags;
    // Start is called before the first frame update
    void Start()
    {
        this.tags = new NC_Tags();
        inventory = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.HasKey())
            Debug.Log("HAS KEY");


        if (inventory.HasNewItem())
        {
            item = inventory.GetLastItem();
            if (item.Memory == tags.Bad)
                DoDoors();
            else if (item.Memory == tags.Good)
                Debug.Log("Do something good.");
            inventory.NewItemAcknowledge();
        }
    }


    private void GetFreaky()
    {

    }


    private void DoDoors()
    {
        GameObject[] doors;
            doors = GameObject.FindGameObjectsWithTag(this.tags.Door);


            foreach (GameObject door in doors)
            {
                float distance = Vector3.Distance(player.position, door.transform.position);
                if (distance <= minimumDistance)
                {
                    int id;
                    DoorController doorController = door.GetComponentInChildren<DoorController>();
                    id = doorController.GetID();
                    GameEvents.events.DoorwayTriggerEnter(id);


                    Action closeDoor = ()=> GameEvents.events.DoorwayTriggerExit(id);
                    StartCoroutine(Wait(waitTime, closeDoor));
                }
            }
    }


    private IEnumerator Wait(int time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
