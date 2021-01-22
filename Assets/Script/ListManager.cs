using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Items
{
    public string name;         //リスト中の名前
    public string fileTitle;    //セルのタイトル
    public Button.ButtonClickedEvent buttonClicked;
}

public class ListManager : MonoBehaviour
{
    public GameObject sampleButton;  //ボタンのPrefab
    public GameObject sampleDialog;  //モーダルダイアログのPrefab
    private InputField inputField;   //Input Fieldのゲームオブジェクト
    private Scrollbar  scrollBar;    //Scroll Barのゲームオブジェクト

    private Transform mainContainer;     
    private Transform contentContainer;
        
    public List<Items> itemsList = new List<Items>();

    private int count;               //リストオブジェクトの総数(リスト作成用)
    private bool isReversed;         //リストの状態
                                     
    private string fileName;         //モーダルダイアログのタイトル
    private string fileContent;      //モーダルダイアログの内容


    private void Start()
    {
        inputField       = GameObject.Find("InputField").GetComponent<InputField>();
        scrollBar        = GameObject.Find("Scrollbar Vertical").GetComponent<Scrollbar>();
        mainContainer    = GameObject.Find("Canvas").GetComponent<Transform>();
        contentContainer = GameObject.Find("Content").GetComponent<Transform>();
        GenerateList();

        isReversed = false;
    }

    //最初のリストを作成
    public void GenerateList()
    {
        foreach(var item in itemsList)
        {
            GameObject newButton    = Instantiate(sampleButton) as GameObject;
            ButtonManager button    = newButton.GetComponent<ButtonManager>();            
            button.name.text        = (count + 1).ToString();
            button.gameObject.name  = (count + 1).ToString();
            button.content.text     = item.fileTitle;
            button.button.onClick   = item.buttonClicked;

            item.name = button.name.text;
            newButton.transform.SetParent(contentContainer);

            //入力してないならデフォルト文字を設定する
            if (item.fileTitle == "" || item.fileTitle == null)
            {
                button.content.text = "Default Name";
            }

            count++;
        }
    }

    //モーダルダイアログを作成
    public void ListOnClicked()
    {
        GameObject newDialog = Instantiate(sampleDialog) as GameObject;
        DialogManager dialog = newDialog.GetComponent<DialogManager>();
        dialog.fileName.text = fileName;
        dialog.content.text  = fileContent;
        newDialog.transform.SetParent(mainContainer);
        newDialog.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
    }

    //モーダルダイアログ内のタイトルを入力
    public void AddDialogName(string name)
    {
        //入力してないならデフォルト文字を設定する
        if (name == "" || name == null)
            fileName = "No Name";
        else
            fileName = name.ToString();
    }

    //モーダルダイアログ内の内容を入力
    public void AddDialogContent(string content)
    {
        //入力してないならデフォルト文字を設定する
        if (content == "" || content == null)　
            fileContent = "No Content";
        else
            fileContent = content.ToString();
    }

    //逆順処理
    public void ReverseOnClick()
    {
        //逆順前のリストを削除
        foreach (Transform contentContainer in contentContainer)
        {
            GameObject.Destroy(contentContainer.gameObject);
        }

        //新しいのリストを作成
        if (isReversed == false)
        {
            itemsList.Reverse();

            foreach (var item in itemsList)
            {
                GameObject newButton    = Instantiate(sampleButton) as GameObject;
                ButtonManager button    = newButton.GetComponent<ButtonManager>();
                button.name.text        = (count).ToString();
                button.gameObject.name  = (count).ToString();
                button.content.text     = item.fileTitle;
                button.button.onClick   = item.buttonClicked;

                item.name = button.name.text;
                newButton.transform.SetParent(contentContainer);

                //入力してないならデフォルト文字を設定する
                if (item.fileTitle == "" || item.fileTitle == null)
                {
                    button.content.text = "Default Name";
                }

                count--;
                isReversed = true;
            }
        }
        else
        {
            itemsList.Reverse();
    
            foreach (var item in itemsList)
            {
                GameObject newButton    = Instantiate(sampleButton) as GameObject;
                ButtonManager button    = newButton.GetComponent<ButtonManager>();
                button.name.text        = (count + 1).ToString();
                button.gameObject.name  = (count + 1).ToString();
                button.content.text     = item.fileTitle;
                button.button.onClick   = item.buttonClicked;
                
                item.name = button.name.text;
                newButton.transform.SetParent(contentContainer);

                //入力してないならデフォルト文字を設定する
                if (item.fileTitle == "" || item.fileTitle == null)
                {
                    button.content.text = "Default Name";
                }

                count++;
                isReversed = false;
            }
        }
    }

    //入力したの値を読み取る
    public void GetInputName()
    {
        //InputFieldからの値を取得する
        int inputNumber = int.Parse(inputField.text);
        int listCount = itemsList.Count;

        //値が範囲内か確認
        if(inputNumber <= listCount && inputNumber >= 0)
        {
            ScrollViewPositionChange(inputNumber);
        }

        //InputFieldをクリアする
        inputField.text = "";
    }

    void ScrollViewPositionChange(int inputedNum)
    {
        int listCount = itemsList.Count;

        float scrollpos = (float)inputedNum / (float)listCount;

        if (!isReversed)　//正常状態
        {
            scrollBar.value = 1 - scrollpos;　       //スクロール
        }
        else　//逆順状態
            scrollBar.value = 1 - (1 - scrollpos);　 //スクロール
    }
}
