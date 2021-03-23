using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{


    private ItemContainer itemContainer;
    private Item item;
    private List<Item> inventory = new List<Item>();



    // Start is called before the first frame update
    void Start()
    {
        itemContainer = ItemContainer.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    ///<summary>Add Item _item to inventory.</summary
    ///<param name="id">Id of Item.</param>
    public void Add(int id)
    {
        Item item = new Item();
        foreach (Item _item in itemContainer.Items)
        {
            if (_item.ID == id)
                item = _item;
        }


        if (item != null)
            inventory.Add(item);
    }


    ///<summary>Log items in the inventory in the console.</summary>
    public void Show()
    {
        foreach (Item item in inventory)
        {
            Debug.Log(item.Name +" Memory type: " + item.Memory);            
        }
    }
}
