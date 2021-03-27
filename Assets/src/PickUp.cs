using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{


    public Transform player;
    public int minDistance;
    public string defaultHint;
    public string hint;
    public bool interactable;
    public bool displayHint;
    public int ID;
    private NC_GUI gui;
    private Inventory inventory;
    private bool showingHint = false;

    // Start is called before the first frame update
    void Start()
    {
        hint = defaultHint;
        gui = player.GetComponent<NC_GUI>();
        inventory = player.GetComponent<Inventory>();
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
                            //If player pressed the E key, do something
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
        gui.ClearHint();
        interactable = false;
        inventory.Add(ID);
        inventory.Show();
        gameObject.SetActive(false);
    }
}
