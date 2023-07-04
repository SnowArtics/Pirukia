using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelieverList : MonoBehaviour
{
    private BelieverManageList list;

    public void AddUpdateListener(GameObject manager)
    {
        list = manager.GetComponent<BelieverManageList>();
    }

    public void UpdateList()
    {
        if(list != null)
            list.UpdateList();
    }
}
