using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using PokeDatas;
public class QRCodeScanner : MonoBehaviour
{
    [SerializeField]
    private RawImage cameraOutput;
    [SerializeField]
    private AspectRatioFitter aspectRatioFitter;
    [SerializeField]
    private TextMeshProUGUI textOutput;
    [SerializeField]
    private RectTransform scanZone;

    private bool isCameraAvaible = false;
    private WebCamTexture cameraTexture;

    [SerializeField] float timerScan = 0.5f;
    private float timer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupCamera();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();

        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            Scan();
            timer = timerScan;
            if(!isCameraAvaible)
            {
                SetupCamera();
            }
        }
    }


    private void SetupCamera()
    {
        WebCamDevice[] nbCamera = WebCamTexture.devices;

        if (nbCamera.Length <= 0)
        {
            isCameraAvaible = false;
            return;
        }

        for (int i = 0; i < nbCamera.Length; i++)
        {
            if (nbCamera[i].isFrontFacing == false)
            {
                cameraTexture = new WebCamTexture(nbCamera[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
            }
        }
        if (cameraTexture != null)
        {
            cameraTexture.Play();
            cameraOutput.texture = cameraTexture;
            isCameraAvaible = true;
        }
    }

    private void UpdateCameraRender()
    {
        if (isCameraAvaible == true)
        {
            float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
            aspectRatioFitter.aspectRatio = ratio;

            int orientation = -cameraTexture.videoRotationAngle;
            cameraOutput.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
        }
    }

    public void OnClickScan()
    {
        Scan();
    }

    private string Scan()
    {
        try
        {
            IBarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);
            if(result != null)
            {
                GameData.Instance.QRCodeStr = result.Text + GameData.Instance.infoplayer.starterChoose; ;
                //textOutput.text = result.Text;

                LoadingManager.instance.LoadSceneAsync("ZoneScanned");

                return result.Text;
            }
            else
            {
                textOutput.text = "Can't scan the QR code";
            }
        }
        catch
        {
            textOutput.text = "Failure to scan";
        }
        return "";
    }
}