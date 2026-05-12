using System;
using UnityEngine;

[RequireComponent(typeof(ArticulationBody))]
public class PropertiesArticulateBody : MonoBehaviour
{
    private ArticulationBody articulationBody;
    [SerializeField] private float stiffness;
    [SerializeField] private float damping ;
    [SerializeField] private float limit;
    [SerializeField] private float upperLimit;
    [SerializeField] private float lowerLimit;
    [SerializeField] private float maxVelocityLimit;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        articulationBody = GetComponent<ArticulationBody>();
        ApplySettings();
        Invoke(nameof(ApplySettings), 0.1f);
    }

    public void ApplySettings()
    {
        if (articulationBody == null) return;
        Drive(stiffness, damping, limit, lowerLimit, upperLimit, maxVelocityLimit);


    }
    public void Drive(float stifness, float damping, float forceLimit, float lowerLimit, float upperLimit, float maxVelocityLimit)
    {
        ArticulationDrive articulationDrive = articulationBody.xDrive;
        articulationDrive.upperLimit = upperLimit;
        articulationDrive.lowerLimit = lowerLimit;
        articulationDrive.stiffness = stifness;
        articulationDrive.damping = damping;
        articulationDrive.forceLimit = forceLimit;
        articulationBody.maxAngularVelocity = maxVelocityLimit;
        articulationDrive.driveType = ArticulationDriveType.Force;
        
        articulationBody.xDrive = articulationDrive;


    }

}
