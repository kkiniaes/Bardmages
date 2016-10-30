using UnityEngine;
using System.Collections;

public class CameraRaceMovement : MonoBehaviour {

    public Transform[] targets;
    public Transform[] goals;
    public float cameraSpeed = 2f;

    public int currentGoal = 1; //public so you can set the starting goal, min should be 1

    private Vector3 offset;
    private Vector3 lookPosition;
    private float initialFOV;

    private float maxz;

    // Use this for initialization
    void Start() {
        offset = transform.position;

        initialFOV = GetComponent<Camera>().fieldOfView;
        lookPosition = Vector3.zero;
        maxz = goals[0].transform.position.z;

    }

    // Update is called once per frame
    void Update() {
        Vector3 leadPosition = GetCurrentViewTarget();

        GetComponent<Camera>().fieldOfView = LevelManager.instance.BeatValue(0f) / 2f + initialFOV;
        transform.GetChild(0).GetComponent<Camera>().fieldOfView = LevelManager.instance.BeatValue(0f) / 2f + initialFOV;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, leadPosition + offset, Time.deltaTime * Vector3.Distance(transform.localPosition, leadPosition + offset) * cameraSpeed);
        //lookPosition = Vector3.MoveTowards(lookPosition, leadPosition, Time.deltaTime * Vector3.Distance(lookPosition, leadPosition));
        //transform.LookAt(lookPosition);

    }


    Vector3 GetCurrentViewTarget() {
        //update the farthest distance travelled
        foreach (Transform target in targets)
            if (target.position.z > maxz)
                maxz = target.position.z;
        //check if the updated z position is farther than the current goal and update it if necessary
        if (maxz > goals[currentGoal].position.z)
            currentGoal++;
        //interpolate the target position for the camera between goals
        float normalizedDistance = (maxz - goals[currentGoal - 1].position.z) / (goals[currentGoal].position.z - goals[currentGoal - 1].position.z);
        Vector3 direction = (goals[currentGoal].position - goals[currentGoal - 1].position); //not normalized
        return (goals[currentGoal - 1].position + direction * normalizedDistance);
    }

}

    