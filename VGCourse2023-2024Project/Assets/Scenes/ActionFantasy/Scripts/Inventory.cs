using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> MyInventory = new List<GameObject>();
    public Transform backpackPlace;
    // Start is called before the first frame update
    void Start()
    {
        MyInventory = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
            AddMyItem(other.gameObject);
    }

    void AddMyItem(GameObject newItem) {
        if (!MyInventory.Contains(newItem))
        {
            MyInventory.Add(newItem);

            newItem.transform.parent = backpackPlace;
            newItem.transform.localPosition = Vector3.zero;
            newItem.transform.localRotation = Quaternion.Euler(0,0,0);
            newItem.tag = "Untagged";
            Collider col = newItem.GetComponent<Collider>();
            Destroy(col);
        }
    }
}

