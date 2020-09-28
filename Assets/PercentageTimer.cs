using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentageTimer : MonoBehaviour
{
    float startTime = 0f;
    public float MaxMemorizationTime = 20f;
    private int percentageValue = 0;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        percentageValue = Mathf.RoundToInt((Time.time - startTime) * (100f / MaxMemorizationTime));
       transform.Find("PercentageText").GetComponent<Text>().text = percentageValue + "%";
        if (percentageValue > 99 && count == 0)
        {
            if (gameObject.name == "Practice2")
            {
            transform.parent.GetComponent<OLM_control>().FromPractice2ToPractice3();
            count++;
            }
            else
            {
                transform.parent.GetComponent<OLM_control>().FromFormal2ToFormal3();
                count++;
            }
            
        }
    }
    private void OnEnable()
    {
        startTime = Time.time;
        percentageValue = 0;
        count = 0;

    }
  
}
