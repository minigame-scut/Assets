using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMap : MonoBehaviour
{
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void PlayOpenMap()
    {
        ani.SetBool("openmap", true);
    }
    public void StopOpenMap()
    {
        ani.SetBool("openmap", false);
    }
    public void PlayCloseMap()
    {
        ani.SetBool("closemap", true);
    }
    public void StopCloseMap()
    {
        ani.SetBool("closemap", false);
    }
}
