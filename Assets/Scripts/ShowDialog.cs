using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class ShowDialog : MonoBehaviour
{
    // 人物对话面板
    public GameObject dialogCanvas;
    // F键提示符
    public GameObject hint;
    // 对话框
    public GameObject dialog;
    // 对话文件
    public TextAsset dialogTextAsset;
    // 对话文本数组
    private List<string[]> dialogList;
    // 对话文本对象
    private Text dialogText;
    // 对话框是否显示
    private bool isDialog = false;
    // 对话框行数索引
    private int dialogIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 初始化时将面板取消激活
        dialogCanvas.SetActive(false);
        // 通过文件创建对话文本
        dialogList = CreateText(dialogTextAsset);
        // 获取对话文本对象
        dialogText = dialog.GetComponentsInChildren<Text>()[0];
        // 设置初始对话
        dialogText.text = dialogList[dialogIndex][0] + ": " + dialogList[dialogIndex][1];
        // 对话索引值加1
        dialogIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        // 按键后激活对话框，隐藏提示
        if (!isDialog && Input.GetKeyDown(KeyCode.F))
        {
            if (dialogCanvas.activeSelf)
            {
                hint.SetActive(false);
                dialog.SetActive(true);
                isDialog = true;
            }
        }

        // 如果对话框存在
        if (isDialog && Input.GetKeyDown(KeyCode.F))
        {
            // 设置对话
            if (dialogIndex < dialogList.Count)
            {
                dialogText.text = dialogList[dialogIndex][0] + ": " + dialogList[dialogIndex][1];
                dialogIndex++;
            }
            else
            {
                dialogIndex = 0;
                hint.SetActive(true);
                dialog.SetActive(false);
                isDialog = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 进入碰撞区域后激活面板
        dialogCanvas.SetActive(true);
        hint.SetActive(true);
        dialog.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        dialogCanvas.SetActive(false);
        hint.SetActive(false);
        dialogIndex = 0;
        dialogCanvas.SetActive(false);
        isDialog = false;
    }

    List<string[]> CreateText(TextAsset textAsset)
    {
        List<string[]> dialogArrayList = new List<string[]>();
        //初始化文本资源里的对话内容
        string[] textArray = textAsset.text.Split('\n');//先根据换行符切割出每一行文字
        for (int i = 0; i < textArray.Length; i++)
        {
            string[] contents = textArray[i].Split('%'); //根据%切割出三个 0 名字 1说的话 2头像
            dialogArrayList.Add(contents); //把名字 对话 头像 存进List
        }
        return dialogArrayList;
    }
}
