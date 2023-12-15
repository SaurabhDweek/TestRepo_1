using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DivideRandomNumber : MonoBehaviour
{
    public SpinReward _spinReward;

    public Image RedBG;

    public List<int> getRewardList = new List<int>();
    public List<int> finalRewardList = new List<int>();
    public List<int> winRewardList = new List<int>();
    public List<int> totalRewardList = new List<int>();

    public TextMeshProUGUI txtGamesCount,txtWrongValues ,txtBonusVal, txtcalculateReward, txtTotalRweardCounter, txtgetSpin;

    public float timeInterval = 0f;

    private void Start()
    {
        InvokeRepeating("StartDividing", 1f, timeInterval);
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //StartDividing();
    //    }
    //}
    public int BonusValue = 0;
    public int maxParts = 0;
    public int getSpin = 0;

    int gamesCounter = 0;
    int intTotalRweardCounter = 0;
    int intGetRewardVal = 0;
    int intRewardIndex = 0;
    int calculateReward = 0;
    int totalSpin = 0;

    private void StartDividing()
    {
        int randomNumber = Random.Range(320, 20001); // Generate a random number between 320 and 3200 (inclusive)
        maxParts = Random.Range(5, 9); // Maximum number of parts

        // Ensure the random number is divisible by 10
        randomNumber = Mathf.FloorToInt(randomNumber / 10f) * 10;
        BonusValue = randomNumber;
        DivideNumber();
    }

    private void AddWrongDataInList()
    {
        if(intTotalRweardCounter != getSpin || calculateReward != BonusValue)
        {
            string strWrongParts = "Random Spins => " + getSpin + " || RewardCounter => " + intTotalRweardCounter +
                " || BonusValue => " + BonusValue + " || calculateReward => " + calculateReward;
            RedBG.color = Color.red;
            txtWrongValues.text = strWrongParts;
            CancelInvoke("StartDividing");
        } 
    }

    public void ShuffleIntValue(int val, List<int> valInList, List<float> valFloatList)
    {
        if (val == 0)
        {
            for (int i = 0; i < valInList.Count; i++)
            {
                int temp = valInList[i];
                int randomIndex = UnityEngine.Random.Range(i, valInList.Count);
                valInList[i] = valInList[randomIndex];
                valInList[randomIndex] = temp;
            }
        }
        else
        {
            for (int i = 0; i < valFloatList.Count; i++)
            {
                float temp = valFloatList[i];
                int randomIndex = UnityEngine.Random.Range(i, valFloatList.Count);
                valFloatList[i] = valFloatList[randomIndex];
                valFloatList[randomIndex] = temp;
            }
        }
    }

    private void DisplayCurrCalculation()
    {
        gamesCounter++;
        txtBonusVal.text = "BonusVal = " + BonusValue;
        txtcalculateReward.text = "Calculate = " + calculateReward;
        txtgetSpin.text = "GetSpin = " + getSpin;
        txtTotalRweardCounter.text = "TotalReward = " + intTotalRweardCounter;
        txtGamesCount.text = "GamesPlayed : " + gamesCounter;
    }

    private void DivideNumber()
    {
        getSpin = 0;
        calculateReward = 0;
        totalSpin = 0;
        
        float val = BonusValue;

        //Debug.Log("TOTAL FREE SPIN REWARD:" + val);

        float totalweightSum = 0f;

        getSpin = maxParts;
        //Debug.Log("Bonus 1 Game Calcluation");

        intTotalRweardCounter = 0;
        intGetRewardVal = 0;
        intRewardIndex = 0;
        calculateReward = 0;

        winRewardList.Clear();
        finalRewardList.Clear();
        totalRewardList.Clear();
        getRewardList.Clear();

        //Debug.Log("TOTAL FREE SPIN REWARD:" + val);
        List<float> spinWList = new List<float>();

        for (int i = 0; i < _spinReward._weightedValueList.Count; i++)
        {
            spinWList.Add(_spinReward._weightedValueList[i]);
        }

        ShuffleIntValue(1, null, spinWList);

        for (int i = 0; i < getSpin; i++)
        {
            totalweightSum += spinWList[i];
        }

        finalRewardList.Clear();
        int addRemainder = 0;
        int loopTotal = 0;

        for (int i = 0; i < getSpin; i++)
        {
            float newVal = (float)spinWList[i];
            float cal = (val / totalweightSum * newVal);
            int abc = Mathf.RoundToInt(cal);

            int dividend = abc;
            int remainder = 0;
            int divisor = 10;
            int quotient = System.Math.DivRem((int)dividend, (int)divisor, out remainder);

            if (i < (getSpin - 1))
            {
                if (remainder != 0)
                {
                    abc = quotient * divisor;
                }

                addRemainder += remainder;
                loopTotal += abc;
            }
            else
            {
                abc = abc + addRemainder;
                abc = BonusValue - loopTotal;
            }

            finalRewardList.Add(abc);
        }
        ShuffleIntValue(0, finalRewardList, null);

        int addTotalRewardVal = 0;

        for (int i = 0; i < finalRewardList.Count; i++)
        {
            getRewardList.Add(finalRewardList[i]);
            winRewardList.Add(i);
            addTotalRewardVal = addTotalRewardVal + finalRewardList[i];
            totalRewardList.Add(addTotalRewardVal);
            calculateReward = addTotalRewardVal;
        }
        intTotalRweardCounter = finalRewardList.Count;
        DisplayCurrCalculation();
        AddWrongDataInList();
    }
}

