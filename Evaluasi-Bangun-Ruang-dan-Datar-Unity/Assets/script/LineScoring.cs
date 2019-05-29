using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScoring : MonoBehaviour
{
    List<int> score;
    float x1;
    float x2;
    float y1;
    float y2;
    List<float>avg = new List<float>();
    public LineScoring()
    {
        score = new List<int>();
    }

    public void SetFunction(float x1, float y1, float x2, float y2)
    {
        this.x1 = x1;
        this.x2 = x2;
        this.y1 = y1;
        this.y2 = y2;
    }
    public float Gradient()
    {
        return (y2 - y1) / (x2 - x1);
    }

    //mencari score dari garis yang dibuat
    public float Distance(float x, float y)
    {
        float m = Gradient();
        // Main idea y = mx + b
        float myY;
        float b;
        float distance;
        b = this.y1 - (m * this.x1);
        myY = m * x + b;
        if(this.x1 == this.x2)
        {
            distance = Mathf.Abs(this.x1 - x);
        }
        else
        {
            distance = Mathf.Abs(myY - y);
        }  
        //menyimpan score untuk dirata - ratakan ketika membentuk bangun datar
        avg.Add(distance);
        return distance;
    }

    public float AverageDistance(List<float[]> points)
    {
        //Debug.Log("points : " + points.Count);
        for(int i = 0; i < points.Count; i ++)
        {
            avg.Add(Distance(points[i][0], points[i][1]));
        }
        float sum=0;
        for(int i = 0; i < avg.Count; i++)
        {
            sum += avg[i];
        }
        return sum / avg.Count;
    }

    public int Scoring(List<float[]> points)
    {
        //Debug.Log("Average : "+ AverageDistance(points));
        avg.Clear();
        //Debug.Log(AverageDistance(points));
        if(AverageDistance(points) <= 0.02f)
        {
            score.Add(3);
            return 3;
        }
        else if (AverageDistance(points) <= 0.05f)
        {
            score.Add(2);
            return 2;
        }
        else
        {
            score.Add(1);
            return 1;
        }
    }

    public int TotalScoring()
    {
        float totalScore = 0;
        avg.Clear();
        for(int i = 0; i < score.Count; i ++)
        {
            //Debug.Log("score count " + score.Count);
            totalScore += score[i];
            //Debug.Log("Total score " + totalScore);
        }
        totalScore = Mathf.Round(totalScore/score.Count);
        score.Clear();

        return (int)totalScore;
    }
}
