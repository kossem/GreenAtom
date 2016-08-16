using System;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedBack_5x4 : MonoBehaviour
{
    private static Button mail, info, back, home, close;
    private InputField name, organization, position, subject, message, dilivery;
    // Use this for initialization
    void Start()
    {
        FeedBackContent._5x4();
        //Debug.Log(PlayerPrefs.GetString("email"));
        name = GameObject.Find("FullName_5x4").GetComponent<InputField>();
        organization = GameObject.Find("Organization_5x4").GetComponent<InputField>();
        position = GameObject.Find("Position_5x4").GetComponent<InputField>();
        subject = GameObject.Find("Subject_5x4").GetComponent<InputField>();
        message = GameObject.Find("MessageText_5x4").GetComponent<InputField>();
        dilivery = GameObject.Find("DiliveryStatus_5x4").GetComponent<InputField>();
        initButtons();
        back = GameObject.Find("BackWhite").GetComponent<Button>();
        back.onClick.AddListener(delegate { SceneManager.LoadScene(PlayerPrefs.GetString("Back")); });
        home = GameObject.Find("HomeWhite").GetComponent<Button>();
        home.onClick.AddListener(delegate { SceneManager.LoadScene("mainWindow_5x4"); });
        info = GameObject.Find("InfoWhite").GetComponent<Button>();
        info.onClick.AddListener(delegate { SceneManager.LoadScene("aboutCompany_5x4"); });
        close = GameObject.Find("CloseWhite").GetComponent<Button>();
        close.onClick.AddListener(Application.Quit);

    }


    private bool IsStringEmpty(string a)
    {
        if (String.Equals(a, "", StringComparison.Ordinal))
            return true;
        else
            return false;
    }
    private void sendMail(string from, string organization, string position, string subject, string text)
    {
        SmtpClient _smtpServer = new SmtpClient("smtp.gmail.com");
        MailMessage _message = new MailMessage();
        if (!CheckForInternetConnection())
        {
            dilivery.text = "проверьте соединение с интернетом";
            return;

        }
        if (IsStringEmpty(from))
        {
            dilivery.text = "Заполните строку Ф.И.О";
            return;
        }

        if (IsStringEmpty(organization))
        {
            dilivery.text = "Заполните строку Организация";
            return;
        }

        if (IsStringEmpty(position))
        {
            dilivery.text = "Заполните строку Должность";
            return;
        }

        if (IsStringEmpty(subject))
        {
            dilivery.text = "Заполните строку Тема";
            return;
        }
        if (IsStringEmpty(text))
        {
            dilivery.text = "Заполните поле Текст сообщения";
            return;
        }
        _message.From = new MailAddress("oaogspiit@gmail.com", from + " " + organization + " " + position);
        _message.To.Add(PlayerPrefs.GetString("email"));
        _message.Subject = subject;
        _message.Body = text;
        _smtpServer.Port = 587;
        _smtpServer.Credentials = new System.Net.NetworkCredential("oaogspiit@gmail.com", "shi9loohf") as ICredentialsByHost;
        _smtpServer.EnableSsl = true;


        ServicePointManager.ServerCertificateValidationCallback =
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
        _smtpServer.Send(_message);

        _message.Dispose();
        dilivery.text = "Сообщение успешно отправлено";
    }

    private void initButtons()
    {
        mail = GameObject.Find("sendButton").GetComponent<Button>();
        mail.onClick.AddListener(
            delegate { sendMail(name.text, organization.text, position.text, subject.text, message.text); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static bool CheckForInternetConnection()
    {
        try
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead("http://www.google.com"))
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}
