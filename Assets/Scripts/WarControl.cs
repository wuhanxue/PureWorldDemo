using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarControl : MonoBehaviour
{
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

    // 游戏是否结束
    private bool isOver = false;
    // 当前血量
    private int hp_player = 100;
    private int hp_enemy = 100;

    private bool isPlayerDefend = false;

    // Start is called before the first frame update
    void Start()
    {
        warInfoText.GetComponent<Text>().text = "战斗开始！";
        StartCoroutine(ScrollToBottom());
    }

    // Update is called once per frame
    void Update()
    {
        // 任意一方血量为0以及游戏未结束
        if (!isOver && (hp_enemy <= 0 || hp_player <= 0))
        {
            isOver = true;
            actionPanel.SetActive(false);
            if (hp_enemy <= 0) warInfoText.GetComponent<Text>().text += "\n战斗胜利！";
            else warInfoText.GetComponent<Text>().text += "\n战斗失败！";
            StartCoroutine(ScrollToBottom());
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
        StartCoroutine(Defend());
    }

    /// <summary>
    /// 攻击携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        actionPanel.SetActive(false);
        StartCoroutine(Player_Attack());
        StartCoroutine(Enemy_Attack());
        actionPanel.SetActive(true);
        yield return null;
    }

    IEnumerator Defend()
    {
        actionPanel.SetActive(false);
        StartCoroutine(Player_Defend());
        StartCoroutine(Enemy_Attack());
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
        warInfoText.GetComponent<Text>().text += "\n玩家攻击，敌人-20HP";
        StartCoroutine(ScrollToBottom());
        hp_enemy -= 20;
        isPlayerDefend = false;
    }

    /// <summary>
    /// 敌人攻击携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Enemy_Attack()
    {
        yield return new WaitForSeconds(0.3f);
        if (!isPlayerDefend)
        {
            warInfoText.GetComponent<Text>().text += "\n敌人攻击，玩家-15HP";
            StartCoroutine(ScrollToBottom());
            hp_player -= 15;
        }
        else
        {
            warInfoText.GetComponent<Text>().text += "\n敌人攻击，玩家-7HP";
            StartCoroutine(ScrollToBottom());
            hp_player -= 7;
        }
    }

    /// <summary>
    /// 玩家防御携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Player_Defend()
    {
        yield return new WaitForSeconds(0.3f);
        warInfoText.GetComponent<Text>().text += "\n玩家防御";
        StartCoroutine(ScrollToBottom());
        isPlayerDefend = true;
    }

    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        warInfoText.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }
}
