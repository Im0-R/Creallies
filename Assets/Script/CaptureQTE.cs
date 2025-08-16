using PokeDatas;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CaptureQTE : MonoBehaviour
{
    [SerializeField] CaptureManager captureManager;

    [SerializeField] GameObject backgroundBar;
    RawImage backgroundBarImage;

    [SerializeField] GameObject cursor;
    RawImage cursorImage;
    RectTransform cursorRectTransform;

    [SerializeField] GameObject validZone;
    RawImage validZoneImage;
    RectTransform validZoneRectTransform;
    Vector2 validRange;


    [SerializeField] float barFillSecond = 150.0f;
    [SerializeField] float percentModifiedPerSecond = 10.0f;
    public float capturePercent = 50.0f;
    public float barPercent = 0.0f;


    [SerializeField] float timerChoseChangeDir = 0.5f;
    [SerializeField] Vector2 speedRange = new Vector2(150.0f, 250.0f);
    float speedZone = 75.0f;
    int dirZone = 1;


    [SerializeField] ParticleSystem particleFailed;
    [SerializeField] ParticleSystem particleSuccess;
    [SerializeField] RawImage allieRawImage;


    Color newColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
    bool hasCaptureStarted = false;
    bool isOutputReached = false;


    [SerializeField] TMP_Text endText;
    [SerializeField] GameObject resultUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundBarImage = backgroundBar.GetComponent<RawImage>();
        cursorImage = cursor.GetComponent<RawImage>();
        validZoneImage = validZone.GetComponent<RawImage>();

        cursorRectTransform = cursor.GetComponent<RectTransform>();
        validZoneRectTransform = validZone.GetComponent<RectTransform>();

        validRange = new Vector2(validZoneRectTransform.localPosition.x - validZoneRectTransform.rect.width / 2.0f, validZoneRectTransform.localPosition.x + validZoneRectTransform.rect.width / 2.0f);

        Debug.Log(validRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCaptureStarted == true)
        {
            CalculatedPercentage();

            MoveValidZone();

            SetColorItems();

            if (isOutputReached == false)
            {
                if (capturePercent == 100.0f)
                {
                    //Debug.Log("Capture at 100%");
                    CaptureSuccess();
                }
                else if (capturePercent == 0.0f)
                {
                    //Debug.Log("Capture at 0%");
                    CaptureFailed();
                }
            }
            else
            {
                if (!particleSuccess.isPlaying && !particleFailed.isPlaying)
                {
                    if (!resultUI.activeInHierarchy)
                    {
                        resultUI.SetActive(true);
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasCaptureStarted = true;
            }
        }
    }

    private void CalculatedPercentage()
    {
        if (Input.GetMouseButton(0))
        {
            barPercent += barFillSecond * Time.deltaTime;
        }
        else
        {
            barPercent -= barFillSecond * Time.deltaTime;
        }
        barPercent = Mathf.Clamp(barPercent, 0.0f, 100.0f);

        cursorRectTransform.localPosition = new Vector3(Mathf.Lerp(-300, 300, barPercent / 100.0f), cursorRectTransform.localPosition.y, cursorRectTransform.localPosition.z);
        //Debug.Log(Mathf.Lerp(-300, 300, barPercent / 100.0f));

        //Check if cursor is in the valid range
        if (cursorRectTransform.localPosition.x >= validRange.x && cursorRectTransform.localPosition.x <= validRange.y)
        {
            capturePercent += percentModifiedPerSecond * Time.deltaTime;
        }
        else
        {
            capturePercent -= percentModifiedPerSecond * Time.deltaTime;
        }

        capturePercent = Mathf.Clamp(capturePercent, 0.0f, 100.0f);
    }

    private void MoveValidZone()
    {
        timerChoseChangeDir -= Time.deltaTime;
        if (timerChoseChangeDir <= 0)
        {
            if (Random.Range(0, 3) != 0)
            {
                dirZone = -dirZone;

                if (GameData.Instance.stockedIndividualAllie != null)
                {
                    speedZone = Random.Range(speedRange.x, speedRange.y) + (50.0f * GameData.Instance.stockedIndividualAllie.rarity);
                }
            }
            timerChoseChangeDir = 0.5f;
        }

        validZoneRectTransform.localPosition += new Vector3(dirZone * speedZone * Time.deltaTime, 0.0f, 0.0f);

        validZoneRectTransform.localPosition = new Vector3(Mathf.Clamp(validZoneRectTransform.localPosition.x, -300, 300), validZoneRectTransform.localPosition.y, validZoneRectTransform.localPosition.z);

        validRange = new Vector2(validZoneRectTransform.localPosition.x - validZoneRectTransform.rect.width / 2.0f, validZoneRectTransform.localPosition.x + validZoneRectTransform.rect.width / 2.0f);
    }

    private void SetColorItems()
    {
        //Set color ui element
        newColor.r = capturePercent >= 50 ? 255 - 255 / 50 * (capturePercent - 50) : 255;
        newColor.g = capturePercent <= 50 ? 255 / 50 * capturePercent : 255;

        //Debug.Log("Color before convert: " + newColor);

        // Convert RBG value from a range of 0 to 255, to a new range of 0 to 1 because unity use this range for some reason
        newColor = new Color(newColor.r / 255.0f, newColor.g / 255.0f, newColor.b / 255.0f, 1.0f);

        //Debug.Log("Color after convert: " + newColor);

        backgroundBarImage.color = newColor;
        cursorImage.color = newColor;
        validZoneImage.color = newColor;
    }

    private void CaptureSuccess()
    {
        HideBar();
        endText.text = GameData.Instance.stockedIndividualAllie.name + " captured !";
        particleSuccess.Play();
        isOutputReached = true;
        captureManager.Capture();
    }

    private void CaptureFailed()
    {
        HideBar();
        endText.text = GameData.Instance.stockedIndividualAllie.name + " fled !";
        particleFailed.Play();
        allieRawImage.enabled = false;
        isOutputReached = true;
        GameData.Instance.stockedIndividualAllie = null;
    }

    private void HideBar()
    {
        backgroundBarImage.enabled = false;
        cursorImage.enabled = false;
        validZoneImage.enabled = false;
    }
}