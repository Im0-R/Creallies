using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZXing;
using ZXing.QrCode;
using Unity.VisualScripting;


public class QRCodeGenerator : MonoBehaviour
{
    [SerializeField]
    private RawImage qrCode;
    [SerializeField]
    private TMP_InputField inputField;

    private Texture2D encodedTexture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        encodedTexture = new Texture2D(256, 256);
    }

    private Color32[] Encode(string _text, Vector2Int _size)
    {
        BarcodeWriter qrCodeWriter = new BarcodeWriter();
        qrCodeWriter.Format = BarcodeFormat.QR_CODE;
        qrCodeWriter.Options = new QrCodeEncodingOptions{Height = _size.x, Width = _size.y};
        
        return qrCodeWriter.Write(_text);
    }

    public void OnClickEncode()
    {
        EncodeTextToQRCode();
    }

    private void EncodeTextToQRCode()
    {
        if (!string.IsNullOrEmpty(inputField.ToString()))
        {
            string text = inputField.text;
            Color32[] pixelTextureEncoded = Encode(text, new Vector2Int(encodedTexture.width, encodedTexture.height));
            encodedTexture.SetPixels32(pixelTextureEncoded);
            encodedTexture.Apply();

            qrCode.texture = encodedTexture;
            Debug.Log("QR texture set");
        }
    }
}
