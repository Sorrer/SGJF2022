using System.Collections;
using System.Collections.Generic;
using Game.Common.ScriptableData.Values;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RatKingLoadingSystem : MonoBehaviour
{

    public IntScriptableData RatKingCount;
    public float spinSpeed;
    public GameObject RatSpritePrefab;

    private List<GameObject> currentRats = new List<GameObject>();

    public bool Running = false;
    public float currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        Activate();
    }

    [MenuItem("Activate")]
    public void Activate()
    {
        foreach (var rat in currentRats)
        {
            Destroy(rat);
        }

        for (int i = 0; i < RatKingCount.value; i++)
        {
            var rat = Instantiate(RatSpritePrefab);
            rat.transform.position = this.transform.position;
            
            rat.transform.rotation = Quaternion.Euler(0, 0,  i * (360 / RatKingCount.value));
            rat.transform.parent = this.transform;
            currentRats.Add(rat);
        }

        Running = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Running)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
            currentAngle += spinSpeed * Time.deltaTime;

            if (currentAngle > 320) currentAngle -= 360;
        }
    }
}
