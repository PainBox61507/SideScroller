using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiloZitare;

public class EnemyHealthDisplay : MonoBehaviour
{
    
    public EnemyController enemyController;
    public List<Image> instances = new List<Image>();
    public Image baseImage;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = this.transform.parent.GetComponentInParent<EnemyController>();
        enemyController.OnReceiveDamage += UpdateIcons;
        CreateIcons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateIcons()
    {
        List<AttackType> attackTypes = enemyController.healthBarCombo;
        for (int i = 0; i < attackTypes.Count; i++)
        {
            if (attackTypes[i] == null)
            {
                Debug.LogError(enemyController.gameObject.name + " has an unasigned AttackType at " + enemyController.transform.position);
            }
            Image icon = Instantiate(baseImage, baseImage.transform.parent);
            icon.transform.SetParent(this.transform);
            icon.overrideSprite = attackTypes[i].attackSprite;
            icon.gameObject.SetActive(true);
            instances.Add(icon);           
        }               
        UpdateIcons();
    }

    public void UpdateIcons()
    {
        for (int i = 0; i < instances.Count; i++)
        {
            if (i < enemyController.currentHealth)
            {
                instances[i].gameObject.SetActive(true);
            }
            else
            {
                instances[i].gameObject.SetActive(false);
            }
        }
    }
}
