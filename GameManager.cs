using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Mob[] mobsDatabase;
    string currentMobName;
    short currentMobHP;
    int currentMobGold;
    Sprite currentMobSprite;
    public Slider currentMobHPSlider;
    public Text currentMobHPText;
    public Image currentMobImage;
    public Text currentMobNameText;
    short currentMobMaxHP;

    int generatedNumber;

    short damage = 0;
    int gold;
    public Text currentPlayerGold;

    short killedMobs = 0;

    public Transform damageIndicator;
    public Transform damageArea;
    Transform currentDamageIndicator;

    public Image background;
    public Sprite[] backgroundSprites;

    public GameObject shopWindow;
    short[] swordsCosts = new short[] {0, 100, 500, 1000 };
    short currentSword = 0;
    short[] swordsDamages = new short[] { 3, 10, 25, 60 };
    public Image[] swordsBackgrounds;

    private void Start()
    {
        GenerateNewMob();
    }



    public void DisplayShop()
    {
        shopWindow.SetActive(!shopWindow.activeSelf);
    }

    public void BuySword(int i)
    {
        if(gold >= swordsCosts[i] && currentSword == i)
        {
            foreach (Image bg in swordsBackgrounds)
            {
                bg.color = Color.red;
            }
            damage = swordsDamages[i];
            swordsBackgrounds[currentSword].color = Color.green;
            gold -= swordsCosts[currentSword];
            UpdateGoldText();
            currentSword += 1;
            
        }
    }

    public void UpdateGoldText()
    {
        currentPlayerGold.text = "Gold : " + gold.ToString();
    }

    public void GenerateNewMob()
    {
        if (killedMobs == 5)
        {
            background.sprite = backgroundSprites[1];
            generatedNumber = 5;
        }
        else
        {
            background.sprite = backgroundSprites[0];
            generatedNumber = Random.Range(0, 5);
        }
        currentMobHPSlider.value = 1;
        currentMobName = mobsDatabase[generatedNumber].mobName;
        currentMobGold = mobsDatabase[generatedNumber].gold;
        currentMobHP = mobsDatabase[generatedNumber].hp;
        currentMobSprite = mobsDatabase[generatedNumber].sprite;
        currentMobMaxHP = currentMobHP;
        currentMobNameText.text = currentMobName;
        currentMobImage.sprite = mobsDatabase[generatedNumber].sprite;
        currentMobImage.SetNativeSize();
        currentMobHPText.text = currentMobHP.ToString() + "/" + currentMobMaxHP.ToString();
    }

    public void Attack()
    {
        Instantiate(damageIndicator, damageArea, true).GetComponent<DamageIndicator>().Spawn(damage);
        currentMobHP -= damage;
        currentMobHPText.text = currentMobHP.ToString() + "/" + currentMobMaxHP.ToString();
        if (currentMobHP <= 0)
        {
            KillMob();
        }else
        {
            currentMobHPSlider.value = (float)currentMobHP / (float)currentMobMaxHP;
        }
    }

    public void KillMob()
    {
        if(killedMobs == 5)
        {
            killedMobs = 0;
        }
        gold += currentMobGold;
        killedMobs += 1;
        UpdateGoldText();
        GenerateNewMob();
    }

}
