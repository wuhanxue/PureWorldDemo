using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarControl : MonoBehaviour
{
    // 当前血量
    private int hp_player = 100;
    private int hp_enemy = 100;
    // 最大血量
    public int MAX_HP_PLAYER = 100;
    public int MAX_HP_ENEMY = 100;
    // 游戏对象
    public GameObject player;
    public GameObject enemy;
    public GameObject actionPanel;
    public GameObject hpInfoPlayer;
    public GameObject hpInfoEnemy;
    public GameObject warInfoText;
    public GameObject btnExit;

    // Start is called before the first frame update
    void Start()
    {
        warInfoText.GetComponent<Text>().text = "战斗开始！\n";
    }

    // Update is called once per frame
    void Update()
    {
        if (hp_enemy <= 0 || hp_player <= 0)
        {
            actionPanel.SetActive(false);
            warInfoText.GetComponent<Text>().text += "战斗结束！\n";
        }
        hpInfoPlayer.GetComponent<Text>().text = "生命值：" + hp_player + "/" + MAX_HP_PLAYER;
        hpInfoEnemy.GetComponent<Text>().text = "生命值：" + hp_enemy + "/" + MAX_HP_ENEMY;
    }

    /// <summary>
    /// 攻击按钮点击事件
    /// </summary>
    public void AttackButtonOnclick()
    {
        StartCoroutine(Attack());
    }

    /// <summary>
    /// 防御按钮点击事件
    /// </summary>
    public void DefendButtonOnclick()
    {

    }

    /// <summary>
    /// 攻击携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        actionPanel.SetActive(false);
        StartCoroutine(Player_Attack());
        actionPanel.SetActive(true);
        yield return null;
    }

    /// <summary>
    /// 玩家攻击携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Player_Attack()
    {
        yield return new WaitForSeconds(0.3f);
        warInfoText.GetComponent<Text>().text += "玩家攻击，敌人-20HP\n";
        hp_enemy -= 20;
    }
}
