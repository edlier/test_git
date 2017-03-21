using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
namespace misSystem
{
    public class validateSession
    {
        public void validate_AU(Page pg, string None_AU, string NoneLogin,int auNum)
        {
            //switch權限是哪幾種
            switch (auNum)
            {
                case 1:
                    resolveSession(pg, SessionString.auDep1, None_AU, NoneLogin);
                    break;
                case 2:
                    resolveSession(pg, SessionString.auDep2, None_AU, NoneLogin);
                    break;
                case 3:
                    resolveSession(pg, SessionString.auDep3, None_AU, NoneLogin);
                    break;
                case 4:
                    resolveSession(pg, SessionString.auDep4, None_AU, NoneLogin);
                    break;
                case 5:
                    resolveSession(pg, SessionString.auDep5, None_AU, NoneLogin);
                    break;
                case 6:
                    resolveSession(pg, SessionString.auDep6, None_AU, NoneLogin);
                    break;
                case 7:
                    resolveSession(pg, SessionString.auDep7, None_AU, NoneLogin);
                    break;
                case 8:
                    resolveSession(pg, SessionString.auDep8, None_AU, NoneLogin);
                    break;
                case 9:
                    resolveSession(pg, SessionString.auDep9, None_AU, NoneLogin);
                    break;
                case 10:
                    resolveSession(pg, SessionString.auDep10, None_AU, NoneLogin);
                    break;
                case 11:
                    resolveSession(pg, SessionString.auDep11, None_AU, NoneLogin);
                    break;
            }

        }


        #region Multiple AU
        public void validate_AU(Page pg, string None_AU, string NoneLogin, int[] auNum)
        {
            //  []auNum 為可以通過的AU ID
            int au = 0;
            if (pg.Session[SessionString.userID] != null)
            {
                for (int i = 0; i < auNum.Count(); i++)
                {
                    //resolveSessionID(auNum[i], pg, None_AU, NoneLogin);
                    //把每個 auNum[i] 去驗證 權限 ，若有過 則回傳 Bool = 1
                    bool x = switchAuNumber(auNum[i], pg);
                    if (x != false) { au++; }
                }
                //有權限
                if (au > 0)
                {
                }
                else
                {
                    pg.Response.Redirect(pg.ResolveClientUrl(None_AU));
                }

            }
            //沒登入
            else
            {
                pg.Response.Redirect(pg.ResolveClientUrl(NoneLogin));
            }


        }

        // Switch  AU
        private bool switchAuNumber(int auNum, Page pg)
        {
            bool au = false;
            //switch權限是哪幾種
            switch (auNum)
            {
                case 1:
                    au = checkAU(pg, SessionString.auDep1);
                    break;
                case 2:
                    au = checkAU(pg, SessionString.auDep2);
                    break;
                case 3:
                    au = checkAU(pg, SessionString.auDep3);
                    break;
                case 4:
                    au = checkAU(pg, SessionString.auDep4);
                    break;
                case 5:
                    au = checkAU(pg, SessionString.auDep5);
                    break;
                case 6:
                    au = checkAU(pg, SessionString.auDep6);
                    break;
                case 7:
                    au = checkAU(pg, SessionString.auDep7);
                    break;
                case 8:
                    au = checkAU(pg, SessionString.auDep8);
                    break;
                case 9:
                    au = checkAU(pg, SessionString.auDep9);
                    break;
                case 10:
                    au = checkAU(pg, SessionString.auDep10);
                    break;
                case 11:
                    au = checkAU(pg, SessionString.auDep11);
                    break;
            }
            return au;
        }

        private bool checkAU(Page pg, string sessionID)
        {
            bool au = false;
            if (Convert.ToInt32(pg.Session[sessionID]) == 1)
            {
                au = true;
            }
            return au;
        }
        #endregion

        private void resolveSession(Page pg, string sessionID, string None_AU, string NoneLogin)
        {
            if (Convert.ToInt32(pg.Session[sessionID]) == 1)
            {
            }
            else
            {
                if (pg.Session[SessionString.userID] != null)
                {
                    //pg.ClientScript.RegisterStartupScript(this.GetType(), "Test", "<script>alert('修改成功!');location.href='../Default.aspx';</script>");
                    //pg.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "alert('You don't have the permission to access here!');location.href='../Default.aspx';", true);
                    //pg.Response.Write("<script>alert('You don't have the permission to access here!;location.href='../Default.aspx';');</script>");
                    pg.Response.Redirect(pg.ResolveClientUrl(None_AU));

                }
                else
                {
                    pg.Response.Redirect(pg.ResolveClientUrl(NoneLogin));
                }
            }
        }


        //驗證密碼強度
        public int checkPassword(string password)
        {
            int strong = 0;
            Regex teShu = new Regex("[~!@#$%_^&*()=+[\\]{}''\";:/?.,><`|！·￥…—（）\\-、；：。，》《]");
            Regex daXie = new Regex("[A-Z]");
            Regex xiaoXie = new Regex("[a-z]");
            Regex shuZi = new Regex("[0-9]");
            if (teShu.IsMatch(password) == true)
                strong++;
            if (daXie.IsMatch(password) == true)
                strong++;
            if (xiaoXie.IsMatch(password) == true)
                strong++;
            if (shuZi.IsMatch(password) == true)
                strong++;
            return strong;
        }
    }
}