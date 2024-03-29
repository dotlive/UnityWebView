﻿using UnityEngine;
using UnityEngine.UI;

public class UnityWebViewDemo : MonoBehaviour
{
    public InputField m_InputField;
    public Button m_ButtonEnter;
    public Button m_ButtonClose;
    public Button m_ButtonUnityCallWebView;
    public Text m_Text;
    private UniWebView m_UniWebView;
    //！！！！需要将这个html文件放到服务器，因为uniwebview只支持http协议
    private string url = "http://47.89.12.59/Test/HtmlDemo.html";

    private void Awake()
    {
        m_ButtonEnter.onClick.AddListener(LoadUrl);
        m_ButtonClose.onClick.AddListener(CloseUrl);
        m_ButtonUnityCallWebView.onClick.AddListener(UnityCallWebView);
        m_InputField.text = url;
    }

    public void LoadUrl()
    {
        if (m_UniWebView != null)
        {
            m_UniWebView.CleanCache();
        }
        if (m_InputField.text == null)
        {
            return;
        }

        m_UniWebView = UniWebViewHelper.CreateUniWebView(gameObject, url, 200, 0, 0, 0);
        m_UniWebView.OnLoadComplete += OnLoadComplete;
        m_UniWebView.OnReceivedMessage += OnReceivedMessage;
        m_UniWebView.Load();
    }
    private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
    {
        if (success)
        {
            //  显示 加载完成的界面  
            webView.Show();
        }
        else
        {
            //   输出 错误码  
            Debug.LogError("Something wrong in webview loading: " + errorMessage);
        }
    }
    public void CloseUrl()
    {
        m_UniWebView.Hide();
        //反注册
        m_UniWebView.OnLoadComplete -= OnLoadComplete;
        m_UniWebView.OnReceivedMessage -= OnReceivedMessage;
        Destroy(m_UniWebView);
    }
    /// <summary>
    /// UniWebView Call Unity
    /// </summary>
    /// <param name="webView"></param>
    /// <param name="message"></param>
    private void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
    {
        //Debug.Log(message.rawMessage);
        m_Text.text = message.rawMessage;
    }
    /// <summary>
    /// Unity Call WebView
    /// </summary>
    private void UnityCallWebView()
    {
        //调用webView中的名为pupop的函数
        string javaScript = "pupop();";
        m_UniWebView.EvaluatingJavaScript(javaScript);
    }
}
