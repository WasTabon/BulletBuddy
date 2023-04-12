using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float StartTimer = 60;
    public Text TimerText;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        TimerText.text = StartTimer.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        StartTimer -= Time.deltaTime;
        TimerText.text = Mathf.Round(StartTimer).ToString();
        DeathPlayer();
        TimerStop();
        
    }

    private void DeathPlayer()
    {
        if (StartTimer <= 0)
        {
            Destroy(Player);
        }
    }

    private void TimerStop()
    {
        if (StartTimer <= 0)
        {
            StartTimer = 0;
        }
    }


}
