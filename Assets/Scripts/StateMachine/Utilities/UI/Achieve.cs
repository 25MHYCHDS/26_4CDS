using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achieve : MonoBehaviour
{
    public GameObject Book;
    public GameObject[] AchieveO;
    public static int[] Achieves = {0,0,0,0,0,0,0};

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Achieves.Length; i++)
        {
            if (Achieves[i] == 1)
            {
                AchieveO[i].SetActive(true);
            }
            if(i == Achieves.Length)
            {
                i = 0;
            }
        }
    }
    public void ChooseBook()
    {
        Book.SetActive(false);
    }
}
