using System.IO;
using UnityEditor;
using UnityEngine;


//	### 문제 2. FileStream으로 간이 파일 암호화

//`FileStream`의 `Position`과 바이트 조작을 활용하여 텍스트 파일을 간단히 암호화/복호화하는 스크립트를 작성하시오.

//**요구사항**

//1. **원본 파일 생성**: `File.WriteAllText`로 `secret.txt`에 영문 메시지를 저장할 것 (예: `"Hello Unity World"`)
//2. * *암호화 * *: `FileStream`으로 `secret.txt`를 열어 모든 바이트를 한 바이트씩 읽고, 각 바이트를 특정 키 값(예: `0xAB`)과 XOR 연산한 뒤 `encrypted.dat`에 쓸 것
//   - `ReadByte()`로 읽고 `-1`(EOF)이면 종료
//   - 각 바이트에 XOR 연산(`^`)을 적용하여 `WriteByte()`로 저장
//3. **복호화**: `encrypted.dat`를 읽어 각 바이트에 동일한 키(`0xAB`)로 다시 XOR 연산하여 `decrypted.txt`에 쓸 것
//   - XOR은 같은 키로 두 번 적용하면 원본이 복원되는 성질을 이용
//4. 원본, 암호화 결과, 복호화 결과를 각각 출력할 것
//5. 
//**예상 출력**

//```
//원본: Hello Unity World
//암호화 완료 (파일 크기: 17 bytes)
//복호화 완료
//복호화 결과: Hello Unity World
//원본과 일치: True
//```

public class Q2 : MonoBehaviour
{
    void Start()
    {
        string originalPath = "secret.txt";
        string encryptedPath = "encrypted.dat";
        string decryptedPath = "decrypted.txt";
        byte key = 0xAB;
        
        string originalText = "Hello Unity World";
        File.WriteAllText(originalPath, originalText);
        Debug.Log($"원본: {originalText}");

        using (FileStream readFs = new FileStream(originalPath, FileMode.Open, FileAccess.Read))
            using (FileStream writeFs = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
            {
                int b;
                while ((b = readFs.ReadByte()) != -1)
                    writeFs.WriteByte((byte)(b ^ key));
            }
        Debug.Log($"암호화 완료 (파일 크기: {new FileInfo(encryptedPath).Length} bytes)");

        using (FileStream readFs = new FileStream(encryptedPath, FileMode.Open, FileAccess.Read))
            using (FileStream writeFs = new FileStream(decryptedPath, FileMode.Create, FileAccess.Write))
            {
                int b;
                while ((b = readFs.ReadByte()) != -1)
                    writeFs.WriteByte((byte)(b ^ key));
            }
        Debug.Log("복호화 완료");

        string decryptedText = File.ReadAllText(decryptedPath);
        Debug.Log($"복호화 결과: {decryptedText}");
        Debug.Log($"원본과 일치: {originalText == decryptedText}");
    }

}