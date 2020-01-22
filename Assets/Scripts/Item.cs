using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string Name;
    public string Description;
    public float Weight; // in kgs
    public Sprite Sprite;
    public List<string> Actions;

    public void PickItem()
    {
        // pick the item
    }

    public void DropItem()
    {
        //drop the item
    }

    
    class Food : Item
    {

    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
