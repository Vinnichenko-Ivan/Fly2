using System.Collections.Generic;
using UnityEngine;

public class Rocket
{ 
    public List<Debris> debrises = new List<Debris>();
    public float angle = 0;
    public float power = 0;
    
    public List<Engine> engines = new List<Engine>();

    public void updateEngine()
    {
        Engine engine = engines[0];
        engine.angle = angle * engine.angleReg;
        engine.power = power * engine.maxPower / 100f;
        
    }
}