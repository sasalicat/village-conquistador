using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkersCenter : MonoBehaviour {
    public const int TEAM1_TEXT = 1;
    public const int TEAM2_TEXT = 2;
    public  GameObject teamMarker1;
    public  GameObject teamMarker2;
    private  Text teamtext1;
    private  Text teamtext2;
    private static List<Dictionary<string, object>> events=new List<Dictionary<string, object>>();
    public  void changeteam1text(string word)
    {
        if (!teamtext1.gameObject.activeSelf)
        {
            teamtext1.gameObject.SetActive(true);
        }
        teamtext1.text=word;
    }
    public  void changeteam2text(string word)
    {
        if (!teamtext2.gameObject.activeSelf)
        {
            teamtext2.gameObject.SetActive(true);
        }
        teamtext2.text = word;
    }
    public static void AddMarkerEvent(Dictionary<string, object> e)
    {
        events.Add(e);
    }
    // Use this for initialization
    void Start () {
        teamtext1 = teamMarker1.GetComponent<Text>();
        teamtext2 = teamMarker2.GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        while (events.Count > 0)
        {
            int no = (int)(events[0])["no"];
            switch (no)
            {
                case TEAM1_TEXT:
                    {
                        int dem=(int)(events[0])["denominator"];
                        int num=(int)(events[0])["numerator"];
                        changeteam1text(num + "/" + dem);
                        break;
                    }
                case TEAM2_TEXT:
                    {
                        int dem = (int)(events[0])["denominator"];
                        int num = (int)(events[0])["numerator"];
                        changeteam2text(num + "/" + dem);
                        break;
                    }

            }
            events.RemoveAt(0);
        }
	}
}
