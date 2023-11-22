using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IInteractable
{
    public GameObject explosion;
    public void OnAbortInteract()
    {
        throw new System.NotImplementedException();
    }

    public void OnEndInteract()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InteractorClosest interactor)
    {
        explosion.SetActive(true);
        Invoke("Disappear", 3f);
    }

    public void OnReadyInteract()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Disappear()
    {
        explosion.SetActive(false);
    }
}
