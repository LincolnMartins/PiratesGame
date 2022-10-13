using System;
using UnityEngine;

public class GameSessionTime : MonoBehaviour
{
    public void OnClickNext()
    {
        int gameSessionTimeValue = Int32.Parse(GetComponentInParent<Options>().gameSessionTime.text);
        if (gameSessionTimeValue < 3) GetComponentInParent<Options>().gameSessionTime.text = (gameSessionTimeValue + 1).ToString();
    }

    public void OnClickPrevious()
    {
        int gameSessionTimeValue = Int32.Parse(GetComponentInParent<Options>().gameSessionTime.text);
        if (gameSessionTimeValue > 1) GetComponentInParent<Options>().gameSessionTime.text = (gameSessionTimeValue - 1).ToString();
    }
}
