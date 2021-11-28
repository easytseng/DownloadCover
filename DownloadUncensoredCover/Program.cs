using CommonFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DownloadUncensoredCover
{
    internal class Program
    {
        private static List<string> extNames = new List<string>(new string[] { ".mp4", ".mkv" });
        private static SortedDictionary<string, int> urlMap = new SortedDictionary<string, int>();

        private static LogUtil logUtil = new LogUtil();
        private static FileUtil fileUtil = new FileUtil();

        static void Main(string[] args)
        {
            string path = @"N:\";


            urlMap["https://image.mgstage.com/images/nanpatv/{1}/{2}/pb_e_{0}.jpg"] = 1;            //200gana
            urlMap["https://image.mgstage.com/images/luxutv/{1}/{2}/pb_e_{0}.jpg"] = 1;             //259luxu
            urlMap["https://image.mgstage.com/images/ara/{1}/{2}/pb_e_{0}.jpg"] = 1;                //261ara
            urlMap["https://image.mgstage.com/images/documentv/{1}/{2}/pb_e_{0}.jpg"] = 1;          //277dcv
            urlMap["https://image.mgstage.com/images/ehitodumadx/{1}/{2}/pb_e_{0}.jpg"] = 1;        //299ewdx
            urlMap["https://image.mgstage.com/images/prestigepremium/{1}/{2}/pb_e_{0}.jpg"] = 1;    //300MAAN
            urlMap["https://image.mgstage.com/images/itteq/{1}/{2}/pb_e_{0}.jpg"] = 1;              //324srtd
            urlMap["https://image.mgstage.com/images/percentoff/{1}/{2}/pb_e_{0}.jpg"] = 1;         //330per
            urlMap["https://image.mgstage.com/images/kanbi/{1}/{2}/pb_e_{0}.jpg"] = 1;              //336knb
            urlMap["https://image.mgstage.com/images/shiroutomanman/{1}/{2}/pb_e_{0}.jpg"] = 1;     //345simm
            urlMap["https://image.mgstage.com/images/reiwashirouto/{1}/{2}/pb_e_{0}.jpg"] = 1;      //383reiw
            urlMap["https://image.mgstage.com/images/jackson/{1}/{2}/pb_e_{0}.jpg"] = 1;            //390jac
            urlMap["https://image.mgstage.com/images/insta/{1}/{2}/pb_e_{0}.jpg"] = 1;              //413inst
            urlMap["https://image.mgstage.com/images/hoihoiz/{1}/{2}/pb_e_{0}.jpg"] = 1;            //420hoi
            urlMap["https://image.mgstage.com/images/sukekiyo/{1}/{2}/pb_e_{0}.jpg"] = 1;           //428SUKE
            urlMap["https://image.mgstage.com/images/shiroutolovetube/{1}/{2}/pb_e_{0}.jpg"] = 1;   //430mmh
            urlMap["https://image.mgstage.com/images/moonforce/{1}/{2}/pb_e_{0}.jpg"] = 1;          //435mfc
            urlMap["https://image.mgstage.com/images/uratalknouratalk/{1}/{2}/pb_e_{0}.jpg"] = 1;   //451hhh
            urlMap["https://image.mgstage.com/images/diego/{1}/{2}/pb_e_{0}.jpg"] = 1;              //459ten
            urlMap["https://image.mgstage.com/images/shiroutopakkuncho/{1}/{2}/pb_e_{0}.jpg"] = 1;  //460spcy
            urlMap["https://image.mgstage.com/images/hamechan/{1}/{2}/pb_e_{0}.jpg"] = 1;           //483sgk
            urlMap["https://image.mgstage.com/images/goodbyecherryboy/{1}/{2}/pb_e_{0}.jpg"] = 1;   //485gcb
            urlMap["https://image.mgstage.com/images/nanpadehamehame/{1}/{2}/pb_e_{0}.jpg"] = 1;    //499ndh
            urlMap["https://image.mgstage.com/images/seikyouiku/{1}/{2}/pb_e_{0}.jpg"] = 1;         //502sei
            urlMap["https://image.mgstage.com/images/shiroutoshikakatan/{1}/{2}/pb_e_{0}.jpg"] = 1; //520ssk
            urlMap["https://javpic1.xyz/uncen/{0}.jpg"] = 4;                                        //fc2ppv
            urlMap["https://javgigahd.blog.2nt.com/?mode=image&filename{0}.jpg"] = 4;               //fc2ppv
            urlMap["https://image.mgstage.com/images/shirouto/{1}/{2}/pb_e_{0}.jpg"] = 1;           //siro

            DirectoryInfo rootDir = new DirectoryInfo(path);
            fileUtil.WalkDirectoryTree(rootDir, executeFile);

        }

        private static void executeFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (extNames.Contains(file.Extension))
            {
                Console.WriteLine(filePath);
                string fileName = fileUtil.getReplaceFileName(file);
                string coverPath = Path.Combine(file.DirectoryName, fileName + ".jpg");
                if (!File.Exists(coverPath))
                {
                    List<string> downloadUrls = new List<string>();

                    foreach (KeyValuePair<string, int> kvp in urlMap)
                    {
                        string downloadUrl = string.Empty;
                        string urlFormat = kvp.Key;
                        switch (kvp.Value)
                        {
                            case 0:
                                downloadUrl = string.Format(urlFormat, fileName.Replace("-", string.Empty));
                                break;
                            case 1:
                                if (fileName.Contains("-"))
                                {
                                    string[] names = Regex.Split(fileName, "-");
                                    downloadUrl = string.Format(urlFormat, fileName, names[0], names[1]);
                                }
                                else
                                {
                                    downloadUrl = string.Format(urlFormat, fileName, fileName, fileName);
                                }
                                break;
                            case 2:
                                downloadUrl = string.Format(urlFormat, fileName.Replace("-0", string.Empty));
                                break;
                            case 3:
                                downloadUrl = string.Format(urlFormat, fileName.Replace("-", "00"));
                                break;
                            case 4:
                                downloadUrl = string.Format(urlFormat, fileName);
                                break;

                        }
                        downloadUrls.Add(downloadUrl);
                    }

                    Boolean isSuccess = false;
                    foreach (string downloadUrl in downloadUrls)
                    {
                        try
                        {
                            Console.WriteLine("try download from " + downloadUrl);
                            // WriteLog(logFileName, DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "try download from " + downloadUrl);

                            WebClient myWebClient = new WebClient();
                            // Concatenate the domain with the Web resource filename.

                            myWebClient.DownloadFile(downloadUrl, coverPath);

                            if (fileUtil.checkFileSize(coverPath))
                            {
                                isSuccess = true;
                                break;
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    if (!isSuccess)
                    {
                        logUtil.WriteLog(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "download " + fileName + " jpg fail ");
                    }
                }
                else
                {
                    Console.WriteLine("RRRRRRRRRRRRRRR");
                }

            }
        }
    }
}
