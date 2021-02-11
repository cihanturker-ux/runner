using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Ne kadar yukarıdan baktığımız
    public float followHeight = 2.5f;
    //Açımızın değeri
    public float followDistence = 5f;
    //Kameranın bizi takip etme hızı
    public float followHeightSpeed = 2f;

    private Transform Player;
    
    //hedef yüksekliğimiz
    private float targetHeight;
    //mevcut yüksekliğimiz
    private float currentHeight;
    //mevcut rotasyonumuz
    private float currentRotation;

    private void Awake()
    {
        /*
         Karakterimizin tagını "Player" olarak değiştirdikten sonra
         private Transform Player olarak belirlediğimiz değişkenimizi
         Karakterimizin transofrmuna eşitliyoruz. 
         */
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        //Hedef yüksekliğimizi belirlemek için oyuncumuzun y pozisyonuyla kameramızın yüksekliğini topluyoruz.
        targetHeight = Player.position.y + followHeight;
        
        /*
         Mevcut yükekliğimizi kameranın eulerEngles'indeki Y açısına eşitliyoruz.
         Bu şekilde sağa sola gitmelerde kamera o tarafa yönelecek
         */
        currentRotation = transform.eulerAngles.y;
        
        /*
         A noktası olarak kameramızın Y açısını alıp B noktası olarak ta hedef yüksekliğimizi alıyoruz
         Mathf.Lerp ile A dan B noktasına Smooth bir geçiş olacak
         followHeight ile de Time.deltaTime ı çarparak aşağı ani inişlerde veya çıkışlarda 
         smooth bir şekilde kamerayı oynatmasını sağlıyoruz
         */
        currentHeight = Mathf.Lerp(transform.position.y, targetHeight, followHeight * Time.deltaTime);
        
        //Quaternion unity de dönüşleri temsil eden fonskiyonumuz
        //Kameramızın oyuncunun etrafına y ekseninde dönüşünü sağlayacak
        Quaternion euler = Quaternion.Euler(0f, currentRotation, 0f);

        /*
         Yeni vektör oluşturup, onu player.position vektörümüze eşitliyoruz. Daha sonra kameramızın
         oyuncuya belirli bir açıyla bakması için kamerayla baktığımız açıdan takip ettiğimiz mesafeyi 
         çıkartıyoruz. Bu işlemler için ise;
         
         Kameramızın y açısındaki değeri yani euler ile Vector3.forward yani Z değerini 1 yapıp iki değeri
         çarpıyoruz. Ve daha sonra bu değeri açımızın değeri olan followDistence ile çarpıyoruz.
         
         Son olarak ta bulduğumuz iki değeri birbirinden çıkartıyoruz.
         */
        Vector3 targetPosition = Player.position - (euler * Vector3.forward) * followDistence;

        //Yukarıda yaptığımız işlemler sonucunda bulduğumuz değerleri birbirine eşitliyoruz
        
        //Oyuncunun pozisyonuyla kendi verdiğimiz değerin toplamı olan currentHeight'ı kameranın y
        //pozisyonuna eşitliyoruz.
        targetPosition.y = currentHeight;
        
        
        transform.position = targetPosition;
        
        //Kamera her zaman oyuncuya doğru bakacak
        transform.LookAt(Player);

    }
}
