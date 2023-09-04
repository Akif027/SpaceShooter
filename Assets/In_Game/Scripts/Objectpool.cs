using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Objectpool : MonoBehaviour
{

    public List<GameObject> PooledObject_list;//define
    public static Objectpool poolSharedInstance;
    public GameObject ObjectToPool;
    public int AmountToPool;


    private void Awake()
    {
        poolSharedInstance = this;
    }

    private void Start()
    {
        PooledObject_list = new List<GameObject>();//create
        GameObject temp;
        for (int i = 0; i < AmountToPool; i++)
        {
            temp = Instantiate(ObjectToPool,transform);//instantiate Gameobject
            temp.SetActive(false);
            PooledObject_list.Add(temp);//add in list
        }
    }


    public GameObject GetpoolObject() //returning Gameobject
    {
        for (int i = 0; i < AmountToPool; i++)
        {
            if (!PooledObject_list[i].activeInHierarchy)//cheack if it's active 
            {
                PooledObject_list[i].SetActive(true);
                return PooledObject_list[i]; //if not return 
            }
     

        }
        return null;
    }



}