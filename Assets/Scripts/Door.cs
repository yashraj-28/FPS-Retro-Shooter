using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorModel;
    public GameObject colliderObject;
    public float openSpeed;
    private bool shouldOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldOpen && doorModel.position.z != 1f)
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, 
                        new Vector3(doorModel.position.x, doorModel.position.y, 1f), openSpeed * Time.deltaTime);
            if(doorModel.position.z == 1f)
            {
                colliderObject.SetActive(false);
            }
        }
        else if( !shouldOpen && doorModel.position.z != 0f)
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, 
                        new Vector3(doorModel.position.x, doorModel.position.y, 0f), openSpeed * Time.deltaTime);
            if(doorModel.position.z == 0f)
            {
                colliderObject.SetActive(true);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            shouldOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player")
        {
            shouldOpen = false;
        }
    }
}
