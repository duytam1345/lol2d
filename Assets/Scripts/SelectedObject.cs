using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedObject : MonoBehaviour
{
    public GameObject g;

    [SerializeField]
    Image imageSelected;

    [SerializeField]
    Sprite spriteMinionCasterBlue;
    [SerializeField]
    Sprite spriteMinionCasterRed;

    [SerializeField]
    Sprite spriteTurretBlue;
    [SerializeField]
    Sprite spriteTurretRed;

    [SerializeField]
    Text textSelectedPhysicsDamage;
    [SerializeField]
    Text textSelectedMagicDamage;
    [SerializeField]
    Text textSelectedArrmor;
    [SerializeField]
    Text textSelectedMagicResistance;
    [SerializeField]
    Text textSelectedAttackSpeed;
    [SerializeField]
    Text textSelectedCooldown;
    [SerializeField]
    Text textSelectedCritRate;
    [SerializeField]
    Text textSelectedMoveSpeed;

    [SerializeField]
    Image imageHeal;

    [SerializeField]
    Image imageMana;

    private void Update()
    {
        if (g)
        {
            if (g.GetComponent<Creep>())
            {
                Creep c = g.GetComponent<Creep>();
                imageSelected.sprite = (c.team == Team.Blue ?
                    spriteMinionCasterBlue : spriteMinionCasterRed);

                textSelectedPhysicsDamage.text = c.property.damage.ToString();
                textSelectedMagicDamage.text = "0";
                textSelectedArrmor.text = c.property.arrmor.ToString();
                textSelectedMagicResistance.text = c.property.magicResistance.ToString(); ;
                textSelectedAttackSpeed.text = c.property.attackSpeed.ToString();
                textSelectedCooldown.text = "0%";
                textSelectedCritRate.text = "0%";
                textSelectedMoveSpeed.text = c.property.moveSpeedCurrent.ToString();

                imageMana.gameObject.SetActive(true);
                imageMana.gameObject.SetActive(false);
                imageHeal.transform.GetChild(0).GetComponent<Image>().fillAmount =
                    (float)g.GetComponent<Creep>().currentHealth / (float)g.GetComponent<Creep>().property.healthPoint;
            }
            else if (g.GetComponent<Champion>())
            {
                Champion c = g.GetComponent<Champion>();
                imageSelected.sprite = c.spriteAvatar;

                textSelectedPhysicsDamage.text = c.propertyChampion.physicsDamage_Real.ToString();
                textSelectedMagicDamage.text = c.propertyChampion.magicDamage_Real.ToString();
                textSelectedArrmor.text = c.propertyChampion.arrmor_Real.ToString();
                textSelectedMagicResistance.text = c.propertyChampion.magicResistance_Real.ToString(); ;
                textSelectedAttackSpeed.text = c.propertyChampion.attackSpeed_Real.ToString();
                textSelectedCooldown.text = c.propertyChampion.cooldown_Real.ToString();
                textSelectedCritRate.text = c.propertyChampion.critRate_Real.ToString();
                textSelectedMoveSpeed.text = c.propertyChampion.moveSpeed_Real.ToString();

                imageMana.gameObject.SetActive(true);
                imageMana.gameObject.SetActive(false);
                imageHeal.transform.GetChild(0).GetComponent<Image>().fillAmount =
                    (float)g.GetComponent<Champion>().propertyChampion.healthPointSecond / (float)g.GetComponent<Champion>().propertyChampion.healthPoint_Real;
            }
            else if (g.GetComponent<Turret>())
            {
                imageSelected.sprite = (g.GetComponent<Turret>().team == Team.Blue ?
                    spriteTurretBlue : spriteTurretRed);

                imageMana.gameObject.SetActive(true);
                imageMana.gameObject.SetActive(false);
                imageHeal.transform.GetChild(0).GetComponent<Image>().fillAmount =
                    (float)g.GetComponent<Turret>().currentHealth / (float)g.GetComponent<Turret>().property.healthPoint;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
