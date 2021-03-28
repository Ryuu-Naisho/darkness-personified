using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{


    private ItemContainer itemContainer;
    private Item lastItem;
    private List<Item> inventory = new List<Item>();
    private bool hasNewItem = false;



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


        if (item.ID != 0){
            inventory.Add(item);
            lastItem = item;
            ReportNewItem();
        }
    }


    ///<summary>Log items in the inventory in the console. Inteded for debug only.</summary>
    public void Show()
    {
        foreach (Item item in inventory)
        {
            Debug.Log(item.Name +" Memory type: " + item.Memory);            
        }
    }


    ///<summary>Check if inventory has an item called key.</summary>
    public bool HasKey()
    {
        int keyId = 7;
        bool hasKey = false;
        Item key = GetItem(keyId);
        if (key.ID == keyId)
            hasKey = true;
        else
        {
            hasKey = false;
        }
        return hasKey;
    }


    ///<summary>Get the item by given id.</summary>
    ///<param name="id">The id of an item.</param>
    private Item GetItem(int id)
    {
        Item item = new Item();
        foreach (Item _item in itemContainer.Items)
        {
            if (_item.ID == id)
                item = _item;
        }


        return item;
    }


    ///<summary>Report there is a new item.</summary>
    private void ReportNewItem()
    {
        this.hasNewItem = true;
    }


    ///<summary>Clear the new item reported.</summary>
    public void NewItemAcknowledge()
    {
        this.hasNewItem = false;
    }


    ///<summary>Return true if there is a new unacknowledge item.</summary>
    public bool HasNewItem()
    {
        return this.hasNewItem;
    }


    ///<summary>Set the last item added to the inventory.</summary>
    ///<param name="item">Item of the Item class.</param>
    private void SetLastItem(Item item)
    {
        this.lastItem = item;
    }



    ///<summary>Get the last item added to the inventory.</summary>
    public Item GetLastItem()
    {
        return lastItem;
    }
}
