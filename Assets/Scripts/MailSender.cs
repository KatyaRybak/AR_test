using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailSender : MonoBehaviour
{
    public void SendEmail()
    {
        string email = "vb@marevo.vision";
        string subject = MyEscapeURL("Test My APP");
        string body = MyEscapeURL(" ");

        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    string MyEscapeURL(string URL)
    {
        return WWW.EscapeURL(URL).Replace("+", "%20");
    }
}
