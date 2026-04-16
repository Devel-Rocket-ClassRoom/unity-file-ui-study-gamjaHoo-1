using System.IO;
using UnityEngine;

public class FileEncryption : MonoBehaviour
{
    private string encryptionDir;
    private string secretPath;
    private string encryptedPath;
    private string decryptedPath;

    private string secretData;

    private byte key = 0xAB;

    void Start()
    {
        encryptionDir = Path.Combine(Application.persistentDataPath, "EncryptionData");
        secretPath = Path.Combine(encryptionDir, "secret.txt");
        encryptedPath = Path.Combine(encryptionDir, "encrypted.dat");
        decryptedPath = Path.Combine(encryptionDir, "decrypted.txt");

        secretData = "Hello Unity World";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Directory.Exists(encryptionDir))
            {
                Directory.CreateDirectory(encryptionDir);
                Debug.Log($"폴더 생성: {encryptionDir}");
            }
            else
            {
                Debug.Log("폴더가 이미 존재함");
            }

            File.WriteAllText(secretPath, secretData);

            string plainText = File.ReadAllText(secretPath);
            Debug.Log($"원본: {plainText}");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            using (FileStream secretFile = new FileStream(secretPath, FileMode.Open))
            using (FileStream encryptedFile = new FileStream(encryptedPath, FileMode.OpenOrCreate))
            {
                int readByte;

                while ((readByte = secretFile.ReadByte()) != -1)
                {
                    byte encryptedByte = (byte)(readByte ^ key);
                    encryptedFile.WriteByte(encryptedByte);
                }
                Debug.Log($"암호화 완료 (파일 크기: {encryptedFile.Length} bytes)");
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            using (FileStream encryptedFile = new FileStream(encryptedPath, FileMode.Open))
            using (FileStream decryptedFile = new FileStream(decryptedPath, FileMode.OpenOrCreate))
            {
                int readByte;

                while ((readByte = encryptedFile.ReadByte()) != -1)
                {
                    byte decryptedByte = (byte)(readByte ^ key);
                    decryptedFile.WriteByte(decryptedByte);
                }
                Debug.Log("복호화 완료");
            }
            string decryptedData = File.ReadAllText(decryptedPath);
            bool isMatch = secretData == decryptedData;

            Debug.Log($"복호화 결과: {decryptedData}");
            Debug.Log($"원본과 일치: {isMatch}");
        }   
    }
}