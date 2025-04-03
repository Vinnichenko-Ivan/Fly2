using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketConfigurator:MonoBehaviour
{
    public WorldScript worldScript;
    public GameObject spawnPoint;
    
    public List<GameObject> debrisPrefabs;
    public Dictionary<String, GameObject> debrisPrefabsNamed;
    public int createStage = 0;
    private Debris capsule;
    private Debris fuel;
    private Debris engine;
    
    private List<Debris> debrisPool = new List<Debris>();
    private List<Engine> engines = new List<Engine>();

    public RocketConfig rocketConfig;
    
    public void Start()
    {
        debrisPool = new List<Debris>(); 
        debrisPrefabsNamed = new Dictionary<String, GameObject>();
        debrisPrefabsNamed.Add("CapsuleA", debrisPrefabs[0]);
        debrisPrefabsNamed.Add("FuelA", debrisPrefabs[1]);
        debrisPrefabsNamed.Add("EngineA", debrisPrefabs[2]);
        debrisPrefabsNamed.Add("AdapterAB", debrisPrefabs[3]);
        debrisPrefabsNamed.Add("FuelB", debrisPrefabs[4]);
        debrisPrefabsNamed.Add("EngineB", debrisPrefabs[5]);
        
        
        rocketConfig = new RocketConfig();
        // PartConfig capsuleConfig = new PartConfig("CapsuleA");
        // PartConfig fuelConfig = new PartConfig("FuelA");
        // PartConfig engineConfig = new PartConfig("EngineA");
        // PortConfig portConfigA = new PortConfig(0, 0, 1, 0);
        // PortConfig portConfigB = new PortConfig(1, 1, 2, 0);
        // rocketConfig.parts.Add(capsuleConfig);
        // rocketConfig.parts.Add(fuelConfig);
        // rocketConfig.parts.Add(engineConfig);
        // rocketConfig.ports.Add(portConfigA);
        // rocketConfig.ports.Add(portConfigB);
        
        PartConfig capsuleConfig = new PartConfig("CapsuleA");
        PartConfig fuelConfig = new PartConfig("FuelA");
        PartConfig fuelConfig2 = new PartConfig("FuelA");
        PartConfig engineConfig = new PartConfig("EngineA");
        PortConfig portConfigA = new PortConfig(0, 0, 1, 0);
        PortConfig portConfigB = new PortConfig(1, 1, 2, 0);
        PortConfig portConfigC = new PortConfig(2, 1, 3, 0);
        rocketConfig.parts.Add(capsuleConfig);
        rocketConfig.parts.Add(fuelConfig);
        rocketConfig.parts.Add(fuelConfig2);
        rocketConfig.parts.Add(engineConfig);
        rocketConfig.ports.Add(portConfigA);
        rocketConfig.ports.Add(portConfigB);
        rocketConfig.ports.Add(portConfigC);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            createStage = 1;
        }

        if (createStage == 1)
        {
            int i = 0;
            foreach (PartConfig partConfig in rocketConfig.parts)
            {
                i++;
                GameObject prefab = Instantiate(debrisPrefabsNamed[partConfig.partName], new Vector3(100000, i * 1000, 0), debrisPrefabsNamed[partConfig.partName].transform.rotation);
                debrisPool.Add(prefab.GetComponent<Debris>());
                Engine engine = prefab.GetComponent<Engine>();
                if (engine)
                {
                    engines.Add(engine);
                }
            }
            createStage = 2;
        }

        if (createStage == 2)
        {
            bool all = true;
            foreach (Debris debris in debrisPool)
            {
                if (!debris.created)
                {
                    all = false;
                }
            }

            if (all)
            {
                createStage = 3;
            }
        }

        if (createStage == 3)
        {
            debrisPool[0].transform.position = spawnPoint.transform.position;
            foreach (PortConfig portConfig in rocketConfig.ports)
            {
                connect(debrisPool[portConfig.part1].debrisPorts[portConfig.port1], debrisPool[portConfig.part2].debrisPorts[portConfig.port2]);
            }
            createStage = 4;
        }
        
        if (createStage == 4)
        {
            Rocket rocket = new Rocket();
            foreach (Debris debris in debrisPool)
            {
                rocket.debrises.Add(debris);
            }
                
            foreach (Engine engine in engines)
            {
                rocket.engines.Add(engine);
            }
            worldScript.rockets.Add(rocket);
            debrisPool.Clear();
            createStage = 5;
        }
    }

    private void connect(DebrisPort a, DebrisPort b)
    {
        Debris debrisA = a.debris;
        Debris debrisB = b.debris;
        
        Vector3 posToMove = a.transform.position - b.transform.position;
        debrisB.transform.position += posToMove - Vector3.up * 0.1f;
        a.transform.rotation = a.debris.defaultRotation;
        b.transform.rotation = b.debris.defaultRotation;
        
        
        FixedJoint2D fixedJoint2D = debrisA.AddComponent<FixedJoint2D>();
        fixedJoint2D.connectedBody = debrisB.debrisParts[0].rigidBody;
        fixedJoint2D.breakForce = 100f;
    }
    
    
}