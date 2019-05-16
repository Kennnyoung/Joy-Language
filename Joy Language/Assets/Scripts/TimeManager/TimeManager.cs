using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public float slowdownFactor = 0.05f;

    public void SlowDown()
    {
        Time.timeScale = slowdownFactor;
    }

    public void SpeedUp()
    {
        Time.timeScale = 1;
    }
}
