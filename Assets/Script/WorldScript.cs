using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class WorldScript : MonoBehaviour
{
    public GameObject camera;
    public GameObject planetPrefab;
    
    public List<Rocket> rockets;
    public List<Planet> planets;

    public float G = 9.81f;
    public float T = 0.1f;

    [FormerlySerializedAs("numDebris")] public int numRocket = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rockets = new List<Rocket>();
        planets = new List<Planet>();
        
        
        // sun.size = 10f;
        // sun.radius = 50f;
        // sun.mass = 100f;
        //
        //
        // earth.size = 10f;
        // earth.radius = 50f;
        // earth.mass = 100f;
        //
        // sun.children.Add(earth);
    }

    public int stage = 0;
    // Update is called once per frame
    void Update()
    {
        if (stage == 0)
        {
            Planet sun = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity).GetComponent<Planet>();
            planets.Add(sun);
            Planet earth = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity).GetComponent<Planet>();
            planets.Add(earth);
            stage++;
        }

        if (stage == 1)
        {
            bool flag = true;
            foreach (Planet planet in planets)
            {
                if (!planet)
                { 
                    flag = false;
                }
            }

            if (flag)
            {
                stage++;
            }
        }

        if (stage == 2)
        {
            planets[0].size = 100f;
            planets[0].radius = 50f;
            planets[0].mass = 100f;
            planets[0].speed = 0.01f;
            
            planets[1].size = 10f;
            planets[1].radius = 50f;
            planets[1].mass = 10f;
            planets[1].speed = 0.01f;
            
            planets[0].children.Add(planets[1]);
            foreach (Planet planet in planets)
            {
                planet.init();
            }
            stage++;
        }
        
        
        
        if (Input.GetKeyDown("["))
        {
            numRocket -= 1;
            
        }
        if (Input.GetKeyDown("]"))
        {
            numRocket += 1;
        }

        if (numRocket <= 0)
        {
            numRocket = 0;
        }

        if (numRocket >= rockets.Count && rockets.Count != 0)
        {
            numRocket = rockets.Count - 1;
        }

        if (numRocket >= 0 && numRocket < rockets.Count)
        {
            Rocket rocket = rockets[numRocket];
            Transform debrisTransform = rocket.debrises[0].transform;
            camera.transform.position = new Vector3(debrisTransform.position.x, debrisTransform.position.y, camera.transform.position.z);
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rocket.angle = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rocket.angle = 1;
            }
            else
            {
                rocket.angle = 0;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                rocket.power += 0.1f;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                rocket.power -= 0.1f;
            }
            
            if (Input.GetKey(KeyCode.UpArrow) || rocket.power >= 100)
            {
                rocket.power = 100;
            }
            if (Input.GetKey(KeyCode.DownArrow)|| rocket.power <= 0)
            {
                rocket.power = 0;
            }

            rocket.updateEngine();
        }


        float wheel_speed = 100f; // скорость зума
        float mw = Input.GetAxis("Mouse ScrollWheel");
        
        float targetOrtho = Camera.main.orthographicSize;
        float scroll = Input.GetAxis ("Mouse ScrollWheel");
        if (scroll != 0.0f) {
            targetOrtho -= scroll * 10f;
            targetOrtho = Mathf.Clamp (targetOrtho, 1, 1000);
        }
        Camera.main.orthographicSize = targetOrtho;
    }

    void FixedUpdate()
    {
        foreach (var planet in planets)
        {
            foreach (Rocket rocket in rockets)
            {
                foreach (Debris debris in rocket.debrises)
                {
                    foreach (DebrisPart debrisPart in debris.debrisParts)
                    {
                        Vector2 posP = planet.transform.position;
                        Vector2 posR = debrisPart.transform.position;
            
                        Vector2 force = forceCalculate(posP, posR, planet.mass, debrisPart.rigidBody.mass);
                        debrisPart.rigidBody.AddForce(force);
                        Debug.DrawRay(posR, force, Color.blue);
                    }
                }
            }
        }

        planets[0].rotateChildren();
    }


    private Vector2 forceCalculate(Vector2 positionR, Vector2 positionP, float massA, float massB)
    {
        float R = Vector2.Distance(positionR, positionP);
        float force = massA * massB * G / R / R * T;
        return (positionR - positionP) * force;
    }
    
    
}
