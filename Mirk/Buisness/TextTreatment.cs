using Mirk.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mirk.Buisness
{
    public class TextTreatment
    {
        private static TextTreatment instance;

        public static TextTreatment getInstance()
        {
            if (instance == null)
            {
                instance = new TextTreatment();
            }
            return instance;
        }

        public string removeFirstWord(string txt)
        {
            List<string> txtList = txt.Split(' ').ToList();
            txtList.RemoveAt(0);

            return String.Join(" ", txtList); 
        }

        public string Refine(string txt)
        {
            int temp = -1;
            List<string> strList = txt.Split(' ').ToList();
            for (int i = 0; i < strList.Count(); i++)
            {
                if (strList[i].Length > 3 && (strList[i].Substring(0, 4) == "http" || strList[i].Substring(0, 3) == "www"))
                {
                    strList[i] = "<a target=\"_blank\" href=\"" + strList[i] + "\">" + strList[i] + "</a>";
                    temp = i;
                }
            }
            if (temp != -1)
            {
                List<string> strList1 = strList.Take(temp).ToList();
                List<string> strList2 = strList.Skip(temp+1).ToList();
                string txt1 = String.Join(" ", strList1);
                string txt2 = String.Join(" ", strList2);
                txt1 = txt1 != null ? GetStyle(txt1) : "";
                txt2 = txt2 != null ? GetStyle(txt2) : "";
                return txt1+' '+strList[temp]+' '+(txt2);
            }

            if (txt.Split(' ').Count() > 1)
            {
                switch (txt.Split(' ').ElementAt(0))
                {
                    case "/code":
                        return CodeRefine(txt);
                    default:
                        return GetStyle(txt);
                }
            }
            else
            {
                return GetStyle(txt);
            }
        }

        public string CodeRefine(string txt)
        {
            txt = removeFirstWord(txt);
            List<string> txtList = txt.Split(' ').ToList();

            for (int i =0; i< txtList.Count() ; i++)
            {
                switch (txtList[i])
                {
                    case "if":
                    case "if(":
                    case "else":
                    case "else{":
                    case "{":
                    case "}":
                    case "(":
                    case ")":
                    case "break;":
                    case "break":
                    case "var":
                    case "string":
                    case "char":
                    case "int":
                    case "bool":
                    case "case":
                    case "switch":
                    case "return":
                    case "public":
                    case "private":
                        txtList[i] = "<span style='color: lightblue'>" + txtList[i] + "</span>";
                        break;
                    case "null":
                    case "delete":
                    case "new":
                    case "get":
                    case "set":
                        txtList[i] = "<span style='color: #BF5FFF'>" + txtList[i] + "</span>";
                        break;

                    case "List<string>":
                    case "List<int>":
                    case "List<char>":
                    case "List<var>":
                    case "List<bool>":
                    case "Class":
                    case "class":
                        txtList[i] = "<span style='color: green'>"+ txtList[i] +"</span>";
                        break;
                    case "=":
                    case ">":
                    case "<":
                    case ">=":
                    case "<=":
                    case "==":
                    case "!=":
                    case "+":
                    case "-":
                    case "%":
                    case "*":
                    case "&&":
                    case "||":
                        txtList[i] = "<span style='color: #bb99ff'>" + txtList[i] + "</span>";
                        break;
                    case ";":
                        txtList[i] = txtList[i] + "<p></p>";
                        break;
                }
            }
            txt = String.Join(" ", txtList);
            txt = "<b>Code Snippet :</b><br/><br/><div class='container keepspaces'>" + txt + "</div>";
            return txt;
        }

        public string GetStyle(string pText)
        {
            List<char> lCharToFind = new List<char>() { '/', '*', '_', '~' };
            List<string> lBalise = new List<string>() { "<i>", "<b>", "<u>", "<strike>" };
            List<string> lBaliseClose = new List<string>() { "</i>", "</b>", "</u>", "</strike>" };
            int lCount = 0;

            foreach (char lChar in lCharToFind)
            {
                lCount = 0;

                foreach (char lLetter in pText)
                {
                    if (lLetter == lChar)
                    {
                        lCount++;
                    }
                }

                int lNbPass = 0;

                for (int i = 0; i < (lCount % 2 == 0 ? lCount : lCount - 1); i++)
                {
                    for (int j = 0; j < pText.Length; j++)
                    {
                        if (pText[j] == lChar)
                        {
                            if (lNbPass % 2 == 0)
                            {
                                pText = pText.Remove(j, 1);
                                pText = pText.Insert(j, lBalise[lCharToFind.IndexOf(lChar)]);
                                lNbPass++;
                                break;
                            }
                            else
                            {
                                pText = pText.Remove(j, 1);
                                pText = pText.Insert(j, lBaliseClose[lCharToFind.IndexOf(lChar)]);
                                lNbPass++;
                                break;
                            }
                        }
                    }
                }
            }

            List<string> txtList = pText.Split(' ').ToList();
            string txt = "";

            foreach (string t in txtList)
            {
                switch (t)
                {
                    case ":)":
                    case ":-)":
                    case ":D":
                    case ":-D":
                        txt += " <img src=\"/Ressources/img/emote_happy.png\"/>";
                        break;
                    case ":(":
                    case ":-(":
                    case ":'(":
                    case ":-<":
                        txt += " <img src=\"/Ressources/img/emote_sad.png\"/>";
                        break;
                    case ";)":
                    case ";-)":
                    case ";D":
                    case ";p":
                        txt += " <img src=\"/Ressources/img/emote_wink.png\"/>";
                        break;
                    case ":p":
                    case ":P":
                    case ":-p":
                    case ":-P":
                        txt += " <img src=\"/Ressources/img/emote_tongue.png\"/>";
                        break;
                    default:
                        txt =txt +" "+ t;
                        break;
                }
            }
            
            return txt;
        }
    }
}