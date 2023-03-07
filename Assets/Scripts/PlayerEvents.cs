using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    private static PlayerEvents instance;

    public static PlayerEvents Instance { get { return instance; } }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }


    public UnityAction<GameObject> OnObjectClicked;

    public void CallOnObjectClicked(GameObject objectCLicked)
    {
        if (OnObjectClicked != null)
        {
            OnObjectClicked.Invoke(objectCLicked);
        }
    }

}
