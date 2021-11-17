using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Agents
{
    public List<Vector3> positions;
}

public class AgentController : MonoBehaviour
{
    [SerializeField] string url;
    [SerializeField] string testEP;
    [SerializeField] string configEP;
    [SerializeField] string updateEP;
    [SerializeField] int numAgents;
    [SerializeField] GameObject carPrefab;
    [SerializeField] float updateDelay;

    // Object to receive the data from the server
    // Using the same name fields and types
    Agents agents;
    // Array of cars
    GameObject[] cars;
    // Time since last update
    float updateTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        cars = new GameObject[numAgents];
        // Generate the objects
        for (int i=0; i<numAgents; i++) {
            cars[i] = Instantiate(carPrefab, Vector3.zero, Quaternion.identity);
        }

        StartCoroutine(SendConfiguration());
    }

    // Update is called once per frame
    void Update()
    {
        if (updateTime > updateDelay) {
            StartCoroutine(UpdatePositions());
            updateTime = 0;
        }
        updateTime += Time.deltaTime;
    }

    IEnumerator TestAPI()
    {
        // Prepare the request, indicating the full url endpoint
        UnityWebRequest www = UnityWebRequest.Get(url + testEP);
        // Wait for the response to arrive
        yield return www.SendWebRequest();

        // Validate the results
        if (www.result == UnityWebRequest.Result.Success) {
            Debug.Log(www.downloadHandler.text);
        } else {
            Debug.Log(www.error);
        }
    }

    IEnumerator UpdatePositions()
    {
        // Prepare the request, indicating the full url endpoint
        UnityWebRequest www = UnityWebRequest.Get(url + updateEP);
        // Wait for the response to arrive
        yield return www.SendWebRequest();

        // Validate the results
        if (www.result == UnityWebRequest.Result.Success) {
            Debug.Log(www.downloadHandler.text);
            // Convert json into an object
            agents = JsonUtility.FromJson<Agents>(www.downloadHandler.text);
            MoveCars();
        } else {
            Debug.Log(www.error);
        }
    }

    IEnumerator SendConfiguration()
    {
        WWWForm form = new WWWForm();
        form.AddField("numAgents", numAgents.ToString());

        // Prepare the request, indicating the full url endpoint
        UnityWebRequest www = UnityWebRequest.Post(url + configEP, form);
        // Wait for the response to arrive
        yield return www.SendWebRequest();

        // Validate the results
        if (www.result == UnityWebRequest.Result.Success) {
            Debug.Log(www.downloadHandler.text);
        } else {
            Debug.Log(www.error);
        }
    }

    void MoveCars()
    {
        for (int i=0; i<numAgents; i++) {
            cars[i].transform.position = agents.positions[i];
        }
    }
}
