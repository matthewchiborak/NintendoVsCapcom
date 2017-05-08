using UnityEngine;
using System.Collections;

public class WindEffect : MonoBehaviour {

    //Repeat rate of the circle. 1 is one full circle. <1 Isn't a complete circle
    public int frequency = 1;

    //Amount of keys on the created curve
    public float resolution = 20.0f;

    //Height of the max/min value of the curve
    public float amplitude = 1.0f;

    //Speed value
    public float zValue = 0f;

    void createCircle()
    {
        ParticleSystem circle = GetComponent<ParticleSystem>();
        var vel = circle.limitVelocityOverLifetime;
        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.Local;
        circle.startSpeed = 0;
        //vel.z = new ParticleSystem.MinMaxCurve(10.0f, zValue);

        AnimationCurve curveX = new AnimationCurve();
        for(int i = 0; i < resolution; i++)
        {
            float newTime = (i / (resolution - 1));
        }
    }

	// Use this for initialization
	void Start ()
    {
        createCircle();
	}
}
