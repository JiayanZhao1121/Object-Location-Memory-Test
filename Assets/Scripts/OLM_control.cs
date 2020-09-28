using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class OLM_control : MonoBehaviour
{

    public List<int> PracticeResponseList = new List<int>();
    public List<int> PracticeCorrectAnswerList = new List<int>();

    public List<int> FormalResponseList = new List<int>();
    public List<int> FormalCorrectAnswerList = new List<int>();

    private int FinishedFormalTrialNumber = 0;

    public int[] HitArray = new int[5];
    public int[] CorrectRejectionArray  = new int[5];
    public int[] FalseAlarmArray = new int[5];
    public int[] MissArray = new int[5];
    public float[] ADIArray = new float[5];
    public float[] TimeArray = new float[5];

    private string StartTime;
    private string FinishTime;

    private float StartTrialTime = 0f;
    private float endTrialTime = 0f;
    public Text ParticipantText;
    private int isInFullScreen = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        StartTime = System.DateTime.UtcNow.ToString("HH:mm:ss MMMM dd, yyyy");

        for (int i = 0; i < transform.Find("Practice4").Find("Colliders").childCount; i++)
        {
            PracticeResponseList.Add(0);
        }
        for (int i = 0; i < transform.Find("Practice4").Find("Colliders").childCount; i++)
        {
            PracticeCorrectAnswerList.Add(0);
        }
        PracticeCorrectAnswerList[2] = 1;
        PracticeCorrectAnswerList[3] = 1;
        PracticeCorrectAnswerList[5] = 1;
        PracticeCorrectAnswerList[7] = 1;
        PracticeCorrectAnswerList[8] = 1;
        PracticeCorrectAnswerList[9] = 1;


        for (int i = 0; i < transform.Find("Formal4").Find("Colliders").childCount; i++)
        {
            FormalResponseList.Add(0);
        }
        for (int i = 0; i < transform.Find("Formal4").Find("Colliders").childCount; i++)
        {
            FormalCorrectAnswerList.Add(0);
        }
        FormalCorrectAnswerList[0] = 1;
        FormalCorrectAnswerList[1] = 1;
        FormalCorrectAnswerList[7] = 1;
        FormalCorrectAnswerList[8] = 1;
        FormalCorrectAnswerList[9] = 1;
        FormalCorrectAnswerList[10] = 1;
        FormalCorrectAnswerList[12] = 1;
        FormalCorrectAnswerList[13] = 1;
        FormalCorrectAnswerList[15] = 1;
        FormalCorrectAnswerList[17] = 1;
        FormalCorrectAnswerList[19] = 1;
        FormalCorrectAnswerList[20] = 1;
        FormalCorrectAnswerList[22] = 1;
        FormalCorrectAnswerList[24] = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.fullScreen && isInFullScreen == 0)
        {
            transform.Find("InitialPage").gameObject.SetActive(false);
            transform.Find("StartPage").gameObject.SetActive(true);
            isInFullScreen++;
        }

    }
    public void FromStartPageToTraining1()
    {
        if (ParticipantText.text.Length == 8)
        {
            PlayerPrefs.SetString("participant ID", ParticipantText.text);
            transform.Find("StartPage").gameObject.SetActive(false);
            transform.Find("Training1").gameObject.SetActive(true);
        }
   
    }
    public void FromTraining1ToTraining2()
    {
        transform.Find("Training1").gameObject.SetActive(false);
        transform.Find("Training2").gameObject.SetActive(true);
    }
    public void FromTraining2ToTraining3()
    {
        transform.Find("Training2").gameObject.SetActive(false);
        transform.Find("Training3").gameObject.SetActive(true);
    }
    public void FromTraining3ToPractice1()
    {
        transform.Find("Training3").gameObject.SetActive(false);
        transform.Find("Practice1").gameObject.SetActive(true);
    }
    public void FromPractice1ToPractice2()
    {
        transform.Find("Practice1").gameObject.SetActive(false);
        transform.Find("Practice2").gameObject.SetActive(true);
    }
    public void FromPractice2ToPractice3()
    {
        transform.Find("Practice2").gameObject.SetActive(false);
        transform.Find("Practice3").gameObject.SetActive(true);
    }
    public void FromPractice3ToPractice4()
    {
        transform.Find("Practice3").gameObject.SetActive(false);
        transform.Find("Practice4").gameObject.SetActive(true);
    }
    public void Practice4Done()
    {
        bool isUserCorrect = CheckListMatch(PracticeResponseList, PracticeCorrectAnswerList);
        // perfect trial
        if (isUserCorrect)
        {
            transform.Find("Practice4").gameObject.SetActive(false);
            transform.Find("Formal1").gameObject.SetActive(true);
        }
        else
        {
  transform.Find("Practice4").gameObject.SetActive(false);
        transform.Find("Practice2").gameObject.SetActive(true);    
            // reset everything
        for (int i = 0; i < transform.Find("Practice4").Find("Colliders").childCount; i++)
        {
  transform.Find("Practice4").Find("Colliders").GetChild(i).Find("Image").gameObject.SetActive(false);
                PracticeResponseList[i] = 0;
        }
        }

   

    }
    public void FromFormal1ToFormal2()
    {
        transform.Find("Formal1").gameObject.SetActive(false);
        transform.Find("Formal2").gameObject.SetActive(true);
    }
    public void FromFormal2ToFormal3()
    {
        transform.Find("Formal2").gameObject.SetActive(false);
        transform.Find("Formal3").gameObject.SetActive(true);
    }
    public void FromFormal3ToFormal4()
    {
        StartTrialTime = Time.time;
        transform.Find("Formal3").gameObject.SetActive(false);
        transform.Find("Formal4").gameObject.SetActive(true);
    }
    public void clickPracticeButton()
    {
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        int ButtonIndex = Convert.ToInt32(ButtonName.Replace("Button", ""));
        GameObject checkImageGameobject = transform.Find("Practice4").Find("Colliders").Find(ButtonName).Find("Image").gameObject; 
        bool isChecked = checkImageGameobject.activeSelf;
        if (!isChecked)
        {
            checkImageGameobject.SetActive(true);
            PracticeResponseList[ButtonIndex] = 1;
            Debug.Log("click " + "Button " +  ButtonIndex);
        }
        else
        {
            checkImageGameobject.SetActive(false);
            PracticeResponseList[ButtonIndex] = 0;
            Debug.Log("Uncheck " + "Button " + ButtonIndex);

        }
       
    }
    public void clickFormalButton()
    {
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        int ButtonIndex = Convert.ToInt32(ButtonName.Replace("Button", ""));
        GameObject checkImageGameobject = transform.Find("Formal4").Find("Colliders").Find(ButtonName).Find("Image").gameObject;
        bool isChecked = checkImageGameobject.activeSelf;
        if (!isChecked)
        {
            checkImageGameobject.SetActive(true);
            FormalResponseList[ButtonIndex] = 1;
            Debug.Log("click " + "Button " + ButtonIndex);
        }
        else
        {
            checkImageGameobject.SetActive(false);
            FormalResponseList[ButtonIndex] = 0;
            Debug.Log("Uncheck " + "Button " + ButtonIndex);

        }

    }
    public void FromFormal4ToFormal5or6()
    {
        endTrialTime = Time.time;
        FinishedFormalTrialNumber++;
        generatePerformanceData(FormalCorrectAnswerList, FormalResponseList);
        int displayTrialNumber = 5 - FinishedFormalTrialNumber;
        transform.Find("Formal5").Find("Text").GetComponent<Text>().text = "You can have up to " + displayTrialNumber.ToString()
            + " trial(s). Please, try again.";
        if (FinishedFormalTrialNumber == 5)
        {
            getTheData();
            transform.Find("Formal4").gameObject.SetActive(false);
            transform.Find("Formal6").gameObject.SetActive(true);
        }
        else
        {
            // reset everything
            for (int i = 0; i < transform.Find("Formal4").Find("Colliders").childCount; i++)
            {
                transform.Find("Formal4").Find("Colliders").GetChild(i).Find("Image").gameObject.SetActive(false);
                FormalResponseList[i] = 0;
            }
            transform.Find("Formal4").gameObject.SetActive(false);
            transform.Find("Formal5").gameObject.SetActive(true);
        }
          
    }
    public void fromFormal5toFormal2()
    {
        transform.Find("Formal5").gameObject.SetActive(false);
        transform.Find("Formal2").gameObject.SetActive(true);
    }
    public void FromFormal6ToFormal7()
    {
        transform.Find("Formal6").gameObject.SetActive(false);
        transform.Find("Formal7").gameObject.SetActive(true);
    }
   
    bool CheckListMatch(List<int> l1, List<int> l2)
    {

        if (l1.Count != l2.Count)
            return false;
        for (int i = 0; i < l1.Count; i++)
        {
            if (l1[i] != l2[i])
                return false;
        }
        return true;
    }
    public void generatePerformanceData(List<int> correctList, List<int> responseList)
    {
        int hitValue = 0;
        int correctRejectionValue = 0;
        int FalseAlarmVlue = 0;
        int MissValue = 0;
        float ADIValue = 0f;
        float TimeValue = 0f;
        for (int i = 0; i < correctList.Count; i++)
        {
            //correct
            if (responseList[i] == correctList[i])
            {
             if (correctList[i] == 0)
                {
                    correctRejectionValue++;
                }
                else
                {
                    hitValue++;
                }
            }
            //wrong
            else
            {
                if (correctList[i] == 0)
                {
                    FalseAlarmVlue++;
                }
                else
                {
                    MissValue++;
                }
            }
             
        }
        float HitRate = hitValue / 14f;
        float FalseAlarmRate = FalseAlarmVlue / 13f;
        ADIValue = (1f / 2f) + (((HitRate - FalseAlarmRate) * (1 + HitRate - FalseAlarmRate)) / (4 * HitRate * (1 - FalseAlarmRate)));
        TimeValue = endTrialTime - StartTrialTime;

        HitArray[FinishedFormalTrialNumber - 1] = hitValue;
        CorrectRejectionArray[FinishedFormalTrialNumber - 1] = correctRejectionValue;
        FalseAlarmArray[FinishedFormalTrialNumber - 1] = FalseAlarmVlue;
        MissArray[FinishedFormalTrialNumber - 1] = MissValue;
        ADIArray[FinishedFormalTrialNumber - 1] = ADIValue;
        TimeArray[FinishedFormalTrialNumber - 1] = TimeValue;
    }
    public void getTheData()
    {
        // string filePath = @"Assets/Resource/SWMT_saved_data.csv";
        //  string filePath = Application.dataPath + "/ExperimentData/SWMT_saved_data.csv";

        // FtpUpload.uploadFile(ParticipantText.text + "," + CorsiSpanScore.ToString(), "Corsi_"+ParticipantText.text + ".csv");
        // File.AppendAllText(filePath, ParticipantText.text + "," + CorsiSpanScore.ToString() + "\n");
        

       string participant_ID = PlayerPrefs.GetString("participant ID");


        FinishTime = System.DateTime.UtcNow.ToString("HH:mm:ss MMMM dd, yyyy");
string StringToUpload = "participant ID = " + participant_ID + "\n" +
            "start time = " + StartTime + "\n" +
            "end time = " + FinishTime; 
        for (int i = 0; i <5; i++)
        {
            StringToUpload = StringToUpload
         + "\n" +
            "T" + (i+1).ToString() + "RH = " + HitArray[i] + "\n" +
            "T" + (i+1).ToString() + "RM = " + CorrectRejectionArray[i] + "\n" +
            "T" + (i + 1).ToString() + "WH = " + FalseAlarmArray[i] + "\n" +
            "T" + (i + 1).ToString() + "WM = " + MissArray[i] + "\n" +
            "T" + (i + 1).ToString() + "ADI = " + ADIArray[i] + "\n" +
            "T" + (i + 1).ToString() + "Time = " + TimeArray[i];
        }
        
            
        // upload data to firebase storage
        GetComponent<WebDownloadHelper>().UploadStringLocal("ObjectLocationMemory", participant_ID, StringToUpload);

      


        PlayerPrefs.SetString("participant ID", "null");
    }
}
