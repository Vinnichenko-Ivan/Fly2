using UnityEngine;

public class DebrisPort : MonoBehaviour
{
    public DebrisPort connectedPort;
    public Debris debris;
    
    void Start()
    {
        debris = transform.parent.GetComponent<Debris>();
    }
}