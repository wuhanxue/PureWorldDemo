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
    // UPDATE: 2019.07.07去除
    //public GameObject player;
    //public GameObject enemy;
    public int rows = 20;               // 地图范围 行
    public int cols = 10;               // 列
    private Transform mapHolder;        // 承装地图物品  方便管理

    public GameObject[] enemyArray;     // 敌人 update by whx
    public GameObject[] helperArray;    // 帮手 update by whx
    private List<Vector2> positionList = new List<Vector2>();  // 存放位置信息  update by whx

    public GameObject actionPanel;
    public GameObject skillPanel;
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
        InstantiateMap();  // 初始化地图及物品
        skillPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 任意一方血量为0以及游戏未结束
        if (!isOver && (hp_enemy <= 0 || hp_player <= 0))
        {
            isOver = true;
            actionPanel.SetActive(false);
            if (hp_enemy <= 0)
            {
                hp_enemy = 0;
                warInfoText.GetComponent<Text>().text += "\n战斗胜利！";
            }
            else if (hp_player <= 0)
            {
                hp_player = 0;
                warInfoText.GetComponent<Text>().text += "\n战斗失败！";
            }
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
    /// 技能按钮点击事件
    /// </summary>
    public void SkillButtonOnclick()
    {
        StartCoroutine(Skill());
    }

    /// <summary>
    /// 技能1按钮点击事件
    /// </summary>
    public void SkillButton1Onclick()
    {
        StartCoroutine(Skill_1());
    }

    /// <summary>
    /// 技能2按钮点击事件
    /// </summary>
    public void SkillButton2Onclick()
    {
        StartCoroutine(Skill_2());
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
        // TODO: 2019.07.07多敌人攻击逻辑待考虑
        actionPanel.SetActive(true);
        yield return null;
    }

    /// <summary>
    /// 防御携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Defend()
    {
        actionPanel.SetActive(false);
        StartCoroutine(Player_Defend());
        StartCoroutine(Enemy_Attack());
        actionPanel.SetActive(true);
        yield return null;
    }

    /// <summary>
    /// 技能携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Skill()
    {
        skillPanel.SetActive(!skillPanel.activeSelf);
        yield return null;
    }

    /// <summary>
    /// 技能1
    /// </summary>
    /// <returns></returns>
    IEnumerator Skill_1()
    {
        skillPanel.SetActive(false);
        StartCoroutine(Player_Attack());
        StartCoroutine(Enemy_Attack());
        actionPanel.SetActive(true);
        yield return null;
    }

    /// <summary>
    /// 技能2
    /// </summary>
    /// <returns></returns>
    IEnumerator Skill_2()
    {
        actionPanel.SetActive(false);
        skillPanel.SetActive(false);
        warInfoText.GetComponent<Text>().text += "\n玩家不会这个技能！";
        StartCoroutine(ScrollToBottom());
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
        yield return new WaitForSeconds(0.5f);
        warInfoText.GetComponent<Text>().text += "\n玩家攻击，敌人-20HP";
        StartCoroutine(ScrollToBottom());
        hp_enemy -= 20;
        isPlayerDefend = false;
        // UPDATE: 2019.07.07位置调整
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SendMessage("Attack");
        }
    }

    /// <summary>
    /// 敌人攻击携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Enemy_Attack()
    {
        yield return new WaitForSeconds(0.5f);
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
        // UPDATE: 2019.07.07位置调整
        // 敌人攻击动画
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            enemy.SendMessage("Attack");
        }
    }

    /// <summary>
    /// 玩家防御携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Player_Defend()
    {
        yield return new WaitForSeconds(0.5f);
        warInfoText.GetComponent<Text>().text += "\n玩家防御";
        // UPDATE: 2019.07.07位置调整
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // 玩家受伤动画
            player.SendMessage("Damage");
        }
        // 文字条拉至底部
        StartCoroutine(ScrollToBottom());
        isPlayerDefend = true;
    }

    /// <summary>
    /// 文字条拉至底部
    /// </summary>
    /// <returns></returns>
    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        warInfoText.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }


    /// <summary>
    /// 初始化地图和物品
    /// </summary>
    private void InstantiateMap()
    {
        mapHolder = new GameObject("Map").transform;        // 创建一个叫map的空物体并将其transform赋值给mapholder
        positionList.Clear();  // 清空历史数据
        for (int x = 2; x < cols - 2; x++)
        {
            for (int y = 2; y < rows - 2; y++)
            {
                positionList.Add(new Vector2(x, y));        // 设置创建的区域
            }
        }
        Vector2 pos = new Vector2(-2, 1);
        InstantiateItems(1, enemyArray, pos);               // 随机生成1个敌人
        int helperCount = Random.Range(1, helperArray.Length);
        // UPDATE: 2019.07.07微调
        Debug.Log("生成帮手数：" + helperCount);
        Vector2 pos1 = new Vector2(8, 1);
        InstantiateItems(helperCount, helperArray, pos1);   // 随机生成帮手
    }

    /// <summary>
    /// 随机生成一个位置
    /// </summary>
    /// <returns>位置</returns>
    private Vector2 RandomPosition()
    {
        int positionIndex = Random.Range(0, positionList.Count);
        Vector2 pos = positionList[positionIndex];
        positionList.RemoveAt(positionIndex);
        return pos;
    }

    /// <summary>
    /// 从数组从随机选择一个物体
    /// </summary>
    /// <param name="prefabs">物体数组</param>
    /// <returns>物体</returns>
    private GameObject RandomPrefab(GameObject[] prefabs)
    {
        int index = Random.Range(0, prefabs.Length);
        return prefabs[index];
    }

    /// <summary>
    /// 随机生成物体  
    /// </summary>
    /// <param name="count">生成数量</param>
    /// <param name="prefabs">物体数组</param>
    /// <param name="pos">坐标</param>
    private void InstantiateItems(int count , GameObject[] prefabs, Vector2 pos)
    {
        for (int i = 0; i < count; i++)
        {
            // UPDATE: 2019.07.07微调
            //Vector2 pos = RandomPosition();           // 获取一个随机位
            GameObject prefab = RandomPrefab(prefabs);  // 随机获取一个物体
            GameObject go = Instantiate(prefab, pos, Quaternion.identity);
            go.transform.SetParent(mapHolder);
        }
    }
}
