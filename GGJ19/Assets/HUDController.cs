using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : Singleton<HUDController>
{
    private int _currentBullets;
    public List<GameObject> Bullets = new List<GameObject>();
    public GameObject BulletsToInstantiate;
    public Transform BulletsPos;
    public float bulletsSpacing;
    public GameObject bulletsParent;
    private int currentAmountOfBullets;

    public Color ColorBulletOff;
    public Color ColorBulletOn;
    public Color ColorLudic;

    public Image HouseColor;
    public Image Tower;
    public Image Tail;
    public Image Spaceship;


    public Slider PillowSlider;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

        void Start()
    {
        
        for (int i = 0; i < PlayerAttackController.Instance.StartingBullets; i++) {
            Vector2 pos = new Vector2(BulletsPos.transform.position.x + (bulletsSpacing * i), BulletsPos.transform.position.y);
            GameObject bullet = Instantiate(BulletsToInstantiate, pos, BulletsToInstantiate.transform.rotation);
            bullet.transform.parent = bulletsParent.transform;
            Bullets.Add(bullet);
        }        
    }

    public void CheckBulletsAmount() {
        
        if (PlayerAttackController.Instance.Bullets < PlayerAttackController.Instance.StartingBullets)
        {
            for (int i = PlayerAttackController.Instance.Bullets; i < (PlayerAttackController.Instance.StartingBullets); i++)
            {
                Bullets[i].GetComponent<Image>().color = ColorBulletOff;
            //    Debug.Log("[i] " + i);
            }
        }
        for (int i = 0; i < (PlayerAttackController.Instance.Bullets); i++) {
          //  Debug.Log("Bullets " + (PlayerAttackController.Instance.Bullets));
          //  Debug.Log("[i] " + i);
            if (i < PlayerAttackController.Instance.Bullets)
            {
                Bullets[i].GetComponent<Image>().color = ColorBulletOn;
            }
            
        }
        
    }

    public void ChangeLudicMeter() {
        Debug.Log("Ludic " + LudicController.Instance.ludicMeterPercent);
        HouseColor.fillAmount = LudicController.Instance.ludicMeterPercent;
      
        ColorLudic.a = LudicController.Instance.ludicMeterPercent;
        Tower.color = ColorLudic;
        Spaceship.color = ColorLudic;
        Tail.color = ColorLudic;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLudicMeter();

    }
}
