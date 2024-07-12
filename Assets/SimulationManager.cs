using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public GameObject TitleText;
    public Button btnPrefab;
    public Transform anchor;
    public Transform anchorSpawn;
    public GameObject ball;
    public int ScaleFactor = 4;
    public float ballSize = 0.5f;
    public Vector2 btnSize = new Vector2(200, 50);

    private WorkOut Data;


    private int requiredBtns;
    private int numberOfBalls;
    private int DoublePop = 0;
    private int DoubleRoll = 0;
    private string myName = string.Empty;

    private void Start()
    {
        Data = JsonReader();

        requiredBtns = Data.workoutInfo.Count;
        numberOfBalls = Data.numberOfWorkoutBalls;
        myName = Data.ProjectName;

        GameObject obj = Instantiate(TitleText, Vector3.zero, Quaternion.identity, anchor);
        TextMeshProUGUI temp = obj.GetComponent<TextMeshProUGUI>();
        temp.text = myName;

        for (int i = 0; i < requiredBtns; i++)
        {
            Button currentBtn = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity,  anchor);
            RectTransform btn = currentBtn.GetComponent<RectTransform>();
            btn.sizeDelta = btnSize;

            TextMeshProUGUI objText = currentBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            objText.text = Data.workoutInfo[i].workoutName;
            objText.text += "\n" + "<size=12>" + Data.workoutInfo[i].description + "</size>";
            if (Data.workoutInfo[i].ballType == "rolling ball")
            {
                if (DoubleRoll == 1)
                {
                    currentBtn.onClick.AddListener(RollingBallSecond);
                    continue;
                }
                currentBtn.onClick.AddListener(RollingBall);
                DoubleRoll += 1;
            }
            else if (Data.workoutInfo[i].ballType == "bouncing ball")
            {
                currentBtn.onClick.AddListener(BouncingBalling);
            }
            else if (Data.workoutInfo[i].ballType == "linedrive ball")
            {
                currentBtn.onClick.AddListener(LineDriveBall);
            }
            else if (Data.workoutInfo[i].ballType == "pop-up ball")
            {
                if (DoublePop == 1)
                {
                    currentBtn.onClick.AddListener(PopUpBallSecond);
                    continue;
                }
                currentBtn.onClick.AddListener(PopUpBall);
                DoublePop += 1;
            }
        }
    }


    public void RollingBall()
    {
        for (int j = 0; j < Data.workoutInfo.Count; j++)
        {
            if (Data.workoutInfo[j].ballType == "rolling ball")
            {
                for (int i = 0; i < numberOfBalls; i++)
                {
                    GameObject obj = Instantiate(ball, new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * ScaleFactor, Quaternion.identity, anchorSpawn);
                    obj.transform.localScale = Vector3.one * ballSize;
                    obj.name = "rolling ball";
                    Ball b = obj.GetComponent<Ball>();
                    b.ballId = Data.workoutInfo[j].workoutDetails[i].ballId;
                    b.Fire(Data.workoutInfo[j].workoutDetails[i].ballDirection, Data.workoutInfo[j].workoutDetails[i].speed);

                    Material m = obj.GetComponent<MeshRenderer>().material;
                    m.color = Color.red;
                }

                break;
            }

        }
    }

    public void RollingBallSecond()
    {
        int Second = 0;
        for (int j = 0; j < Data.workoutInfo.Count; j++)
        {
            if (Data.workoutInfo[j].ballType == "rolling ball")
            {
                if (Second == 1)
                {
                    for (int i = 0; i < numberOfBalls; i++)
                    {
                        GameObject obj = Instantiate(ball, new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * ScaleFactor, Quaternion.identity, anchorSpawn);
                        obj.transform.localScale = Vector3.one * ballSize;
                        obj.name = "rolling ball";
                        Ball b = obj.GetComponent<Ball>();
                        b.ballId = Data.workoutInfo[j].workoutDetails[i].ballId;
                        b.Fire(Data.workoutInfo[j].workoutDetails[i].ballDirection, Data.workoutInfo[j].workoutDetails[i].speed);

                        Material m = obj.GetComponent<MeshRenderer>().material;
                        m.color = Color.red;
                    }

                    break;
                }
                else
                {
                    Second += 1;
                }

            }
        }
    }

    public void BouncingBalling()
    {
        for (int j = 0; j < Data.workoutInfo.Count; j++)
        {
            if (Data.workoutInfo[j].ballType == "bouncing ball")
            {
                for (int i = 0; i < numberOfBalls; i++)
                {
                    GameObject obj = Instantiate(ball, new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * ScaleFactor, Quaternion.identity, anchorSpawn);
                    obj.transform.localScale = Vector3.one * ballSize;
                    obj.name = "bouncing ball";
                    Ball b = obj.GetComponent<Ball>();
                    b.ballId = Data.workoutInfo[j].workoutDetails[i].ballId;
                    b.Fire(Data.workoutInfo[j].workoutDetails[i].ballDirection, Data.workoutInfo[j].workoutDetails[i].speed);

                    Material m = obj.GetComponent<MeshRenderer>().material;
                    m.color = Color.blue;
                }
            }
        }
    }

    public void LineDriveBall()
    {
        for (int j = 0; j < Data.workoutInfo.Count; j++)
        {
            if (Data.workoutInfo[j].ballType == "linedrive ball")
            {
                for (int i = 0; i < numberOfBalls; i++)
                {
                    GameObject obj = Instantiate(ball, new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * ScaleFactor, Quaternion.identity, anchorSpawn);
                    obj.transform.localScale = Vector3.one * ballSize;
                    obj.name = "linedrive ball";
                    Ball b = obj.GetComponent<Ball>();
                    b.ballId = Data.workoutInfo[j].workoutDetails[i].ballId;
                    b.Fire(Data.workoutInfo[j].workoutDetails[i].ballDirection, Data.workoutInfo[j].workoutDetails[i].speed);
                    Material m = obj.GetComponent<MeshRenderer>().material;
                    m.color = Color.yellow;
                }
            }
        }
    }

    public void PopUpBall()
    {
        for (int j = 0; j < Data.workoutInfo.Count; j++)
        {
            if (Data.workoutInfo[j].ballType == "pop-up ball")
            {
                for (int i = 0; i < numberOfBalls; i++)
                {
                    GameObject obj = Instantiate(ball, new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * ScaleFactor, Quaternion.identity, anchorSpawn);
                    obj.transform.localScale = Vector3.one * ballSize;
                    obj.name = "pop-up ball";
                    Ball b = obj.GetComponent<Ball>();
                    b.ballId = Data.workoutInfo[j].workoutDetails[i].ballId;
                    b.Fire(Data.workoutInfo[j].workoutDetails[i].ballDirection, Data.workoutInfo[j].workoutDetails[i].speed);
                    Material m = obj.GetComponent<MeshRenderer>().material;
                    m.color = Color.green;
                }

                break;
            }
        }
    }

    public void PopUpBallSecond()
    {
        int Second = 0;
        for (int j = 0; j < Data.workoutInfo.Count; j++)
        {
            if (Data.workoutInfo[j].ballType == "pop-up ball")
            {
                if (Second == 1)
                {
                    for (int i = 0; i < numberOfBalls; i++)
                    {
                        GameObject obj = Instantiate(ball, new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * ScaleFactor, Quaternion.identity, anchorSpawn);
                        obj.transform.localScale = Vector3.one * ballSize;
                        obj.name = "pop-up ball";
                        Ball b = obj.GetComponent<Ball>();
                        b.ballId = Data.workoutInfo[j].workoutDetails[i].ballId;
                        b.Fire(Data.workoutInfo[j].workoutDetails[i].ballDirection, Data.workoutInfo[j].workoutDetails[i].speed);
                        Material m = obj.GetComponent<MeshRenderer>().material;
                        m.color = Color.green;
                    }
                    break;
                }
                else
                {
                    Second += 1;
                }

            }
        }
    }




    #region DESERIALIZATION
    private readonly string fileName = "data.json";


    public WorkOut JsonReader()
    {
        string filePath = Path.Combine(Application.dataPath, fileName);

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("File not Found!!");
        }

        string data = File.ReadAllText(filePath);
        Debug.Log(data);

        WorkOut dataInfo = JsonUtility.FromJson<WorkOut>(data);
        return dataInfo;

    }
    #endregion

}

#region CLASS MODELS

[System.Serializable]
public class WorkOut
{
    public string ProjectName;
    public int numberOfWorkoutBalls;
    public List<WorkoutInfo> workoutInfo;
}

[System.Serializable]
public class WorkoutInfo
{
    public int workoutID;
    public string workoutName;
    public string description;
    public string ballType;
    public List<WorkoutDetails> workoutDetails;
}

[System.Serializable]
public class WorkoutDetails
{
    public int ballId;
    public float speed;
    public float ballDirection;
}

#endregion