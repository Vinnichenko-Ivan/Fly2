using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Debris : MonoBehaviour
{
    public List<DebrisPart> debrisParts;
    public List<DebrisPort> debrisPorts;
    public bool created = false;
    public Quaternion defaultRotation;

    void Start()
    {
        created = true;
        // debrisParts = new List<DebrisPart>();
        // debrisParts.Add(GetComponent<DebrisPart>());
        // foreach (var debris in transform.GetComponentsInChildren<DebrisPart>().ToList())
        // {
        //     debrisParts.Add(debris);
        // }
        // debrisPorts = new List<DebrisPort>();
        // foreach (var debris in transform.GetComponentsInChildren<DebrisPort>().ToList())
        // {
        //     debrisPorts.Add(debris);
        // }
    }
}