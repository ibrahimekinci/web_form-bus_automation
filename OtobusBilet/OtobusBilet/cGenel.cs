using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace OtobusBiletSatis
{
    public class cGenel
    {
        //public string connStr = "Data Source=SC-105-03;Initial Catalog=otobus;uid=sa;pwd=12345;";
        public string connStr = "Data Source=.;Initial Catalog=otobus; Integrated Security=True;";
        public string alertHata(string title, string mesaj)
        {
            string html = " <script type=\"text/javascript\">$(function () {function alert(title, text) {var html = '';html += '<section class=\"ae-modal-scope default\">';html += '<section class=\"ae-modal-box\">';html += '<div class=\"ae-modal-body\">';html += '<h2 class=\"heading\">' + title + '</h2>';html += '<p class=\"caption\">' + text + '</p>';html += '<div class=\"ae-modal-footer\">';html += '<a href=\"\" class=\"button blue\">Sayfayı Yenile</a>';html += '<a class=\"button gray close\">iptal</a>';html += '</div>';html += '</div>';html += '</section>';html += '</section>';$(\"body\").append(html);$(\"body\").on(\"click\", \".ae-modal-scope .close\", function () {$(this).parents(\".ae-modal-scope\").remove();});}alert(\"" + title + "\", \"" + mesaj + "\");});</script>";
            return html;
        }
        public string alertMesaj(string title, string mesaj)
        {
            string html = " <script title=\"Mesaj\" type=\"text/javascript\">function alert(title, text) {var html = '';html += '<section class=\"ae-modal-scope default\">';html += '<section class=\"ae-modal-box\">';html += '<div class=\"ae-modal-body\">';html += '<h2 class=\"heading\">' + title + '</h2>';html += '<p class=\"caption\">' + text + '</p>';html += '<div class=\"ae-modal-footer\">';html += '<a class=\"button blue close\">Kapat</a>';html += '</div>';html += '</div>';html += '</section>';html += '</section>';$(\"body\").append(html);$(\"body\").on(\"click\", \".ae-modal-scope .close\", function () {$(this).parents(\".ae-modal-scope\").remove();});}alert(\"" + title + "\", \" " + mesaj + " \");</script>";

            return html;
        }

        public DateTime tarihCevir(string strDateTime)
        {
            DateTime tarih = DateTime.Now;
            try
            {
                try
                {
                    tarih = Convert.ToDateTime(strDateTime);
                }
                catch (Exception)
                {
                    if (tarih.ToShortDateString().Length != 10)
                    {
                        string[] parcala;
                        string fTarih;
                        try
                        {
                            parcala = strDateTime.Split('/');
                            for (int i = 0; i < parcala.Length; i++)
                            {
                                if (parcala[i].Length < 2)
                                {
                                    parcala[i] = "0" + parcala[i];
                                }
                            }
                            fTarih = parcala[0] + "/" + parcala[1] + "/" + parcala[2];
                            tarih = Convert.ToDateTime(fTarih);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                parcala = strDateTime.Split('-');
                                for (int i = 0; i < parcala.Length; i++)
                                {
                                    if (parcala[i].Length < 2)
                                    {
                                        parcala[i] = "0" + parcala[i];
                                    }
                                }
                                fTarih = parcala[0] + "/" + parcala[1] + "/" + parcala[2];
                                tarih = Convert.ToDateTime(fTarih);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    parcala = strDateTime.Split('.');
                                    for (int i = 0; i < parcala.Length; i++)
                                    {
                                        if (parcala[i].Length < 2)
                                        {
                                            parcala[i] = "0" + parcala[i];
                                        }
                                    }
                                    fTarih = parcala[0] + "/" + parcala[1] + "/" + parcala[2];
                                    tarih = Convert.ToDateTime(fTarih);
                                }
                                catch (Exception)
                                {
                                    tarih = DateTime.Now;
                                }
                            }
                        }

                    }
                }

            }
            catch (Exception)
            {

            }
            return tarih;
        }
        public string tarihCevirString(string strDateTime)
        {
            string strDate = "";
            for (int i = 0; i < strDateTime.Length; i++)
            {
                if (strDateTime[i]=='.' ||strDateTime[i]=='-' || strDateTime[i]==' ')
                {
                    strDate += '/';
                }
                else
                strDate += strDateTime[i];
            }
            return strDate;
        }
        public bool tarihMi(string strTarih)
        {
            bool sonuc = false;
            try
            {
                DateTime d = Convert.ToDateTime(strTarih);
                sonuc = true;
            }
            catch (Exception)
            {

                throw;
            }
            return sonuc;
        }

    }
}