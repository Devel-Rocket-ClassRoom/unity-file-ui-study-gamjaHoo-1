using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI scoreValue;
    public Button nextButton;

    public float statsDelay = 1f;
    public float scoreDuration = 2f;

    private const int totalStats = 6;
    private const int statsPerColumn = 3;
    
    public int[] statsRolls = new int[totalStats];
    private int finalScore;

    private TextMeshProUGUI[] statsLabels;
    private TextMeshProUGUI[] statsValues;

    private Coroutine routine;
    
    //private int[] stats = new int[6];
    //private int totalScore;

    private void Awake()
    {
        statsLabels = new TextMeshProUGUI[] { leftStatLabel, rightStatLabel };
        statsValues = new TextMeshProUGUI[] {leftStatValue, rightStatValue };

        nextButton.onClick.AddListener(OnNext);
    }

    public override void Open()
    {
        if (routine != null)
        {
            StopAllCoroutines();
            routine = null;
        }

        base.Open();
        ResetStats();
        routine = StartCoroutine(CoPlayGameOverRoutine());


        //for (int i = 0; i < 6; i++)
        //{
        //    stats[i] = Random.Range(0, 99);
        //}
        //totalScore = Random.Range(0, 99999999);

        //leftStatValue.text = "";
        //rightStatValue.text = "";
        //scoreValue.text = "";

        //StartCoroutine(PlayStats());
    }

    //private IEnumerator PlayStats()
    //{
    //    for (int i = 0; i < 6; i++)
    //    {
    //        TextMeshProUGUI target = i < 3 ? leftStatValue : rightStatValue;
    //        int lineIndex = i % 3;
    //        int final = stats[i];

    //        float timer = 0f;
    //        while (timer < 0.6f)
    //        {
    //            timer += Time.deltaTime;
    //            SetLine(target, lineIndex, Random.Range(0, final + 1));
    //            yield return null;
    //        }

    //        SetLine(target, lineIndex, final);
    //        yield return new WaitForSeconds(0.4f);
    //    }

    //    /*
    //    // 랜덤하게 찍다가 결과 띡 나오는 버전
    //    float st = 0f;
    //    while (st < 0.6f)
    //    {
    //        st += Time.deltaTime;
    //        scoreValue.text = Random.Range(0, totalScore + 1).ToString("N0");
    //        yield return null;
    //    }
    //    scoreValue.text = totalScore.ToString("N0");
    //    */

    //    //0부터 차근차근 올라가는 버전

    //    float st2 = 0f;
    //    string totalStr = totalScore.ToString();
    //    int digitCount = totalStr.Length;
    //    float perDigit = 1.6f / digitCount; // 자리당 할당 시간

    //    // 미리 최종값 배열로
    //    int[] digits = new int[digitCount];
    //    for (int d = 0; d < digitCount; d++)
    //        digits[d] = totalStr[d] - '0';

    //    System.Text.StringBuilder sb = new System.Text.StringBuilder();

    //    while (st2 < 1.6f)
    //    {
    //        st2 += Time.deltaTime;
    //        sb.Clear();

    //        for (int d = 0; d < digitCount; d++)
    //        {
    //            // 역순: 마지막 인덱스 = 1의 자리
    //            int reverseIdx = digitCount - 1 - d;
    //            float start = reverseIdx * perDigit;
    //            float end = start + perDigit;

    //            float t = Mathf.Clamp01((st2 - start) / perDigit);
    //            sb.Append((int)Mathf.Lerp(0, digits[d], t));
    //        }

    //        scoreValue.text = sb.ToString();
    //        yield return null;
    //    }
    //    scoreValue.text = totalScore.ToString();
    //}

    //private void SetLine(TextMeshProUGUI target, int lineIndex, int value)
    //{
    //    string[] lines = target.text == "" ? new string[] { "", "", "" } : target.text.Split('\n');
    //    if (lines.Length < 3) System.Array.Resize(ref lines, 3);
    //    lines[lineIndex] = value.ToString();
    //    target.text = string.Join("\n", lines);
    //}

    public override void Close()
    {
        if(routine != null)
        {
            StopAllCoroutines();
            routine = null;
        }
        base.Close();
    }

    public void OnNext()
    {
        windowManager.Open(0);
    }

    private void ResetStats()
    {
        statsRolls = new int[totalStats];
        for(int i = 0; i < totalStats; ++i)
        {
            statsRolls[i] = Random.Range(0, 1000);
        }
        finalScore = Random.Range(0, 10000000);

        for (int i = 0; i <statsLabels.Length; ++i)
        {
            statsLabels[i].text = string.Empty;
            statsValues[i].text = string.Empty;
        }
        scoreValue.text = $"{0:D9}";
    }

    private IEnumerator CoPlayGameOverRoutine()
    {
        for(int i = 0; i < totalStats; ++i)
        {
            yield return new WaitForSeconds(statsDelay);

            int column = i / statsPerColumn;
            var labelText = statsLabels[column];
            var valueText = statsValues[column];

            string newline = i % statsPerColumn == 0 ? string.Empty : "\n"; // 첫줄일 시에 empty
            labelText.text = $"{labelText.text}{newline}Stat {i % 3 + 1}";
            valueText.text = $"{valueText.text}{newline}{statsRolls[i]:D4}";
        }

        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime / scoreDuration;
            int current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreValue.text = $"{current:D9}";
            yield return null;
        }

        scoreValue.text = $"{finalScore:D9}";
        routine = null;
    }
}