using CommonFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DownloadCover
{
    internal class Program
    {
        private static List<string> extNames = new List<string>(new string[] { ".mp4", ".mkv" });
        private static SortedDictionary<string, int> urlMap = new SortedDictionary<string, int>();

        private static LogUtil logUtil = new LogUtil();
        private static FileUtil fileUtil = new FileUtil();

        static void Main(string[] args)
        {

            string path = @"O:\";

            urlMap["https://www.jav321.com/images/prestige/{1}/{2}/pf_o1_{0}.jpg"] = 1;         //abw
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/{0}/{0}pl.jpg"] = 0;                //一般
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/118{0}/118{0}pl.jpg"] = 0;          //abp
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_237{0}/h_237{0}pl.jpg"] = 0;      //nacr
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_067{0}/h_067{0}pl.jpg"] = 0;      //nash
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/{0}so/{0}sopl.jpg"] = 0;            //nsps
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/13{0}/13{0}pl.jpg"] = 0;            //gvg
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/1{0}/1{0}pl.jpg"] = 0;              //hawa
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_1100{0}/h_1100{0}pl.jpg"] = 0;    //hzgd
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/84{0}/84{0}pl.jpg"] = 0;            //mdtm
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_1371{0}/h_1371{0}pl.jpg"] = 0;    //zmen
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_479{0}/h_479{0}pl.jpg"] = 0;      //gah
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_068{0}/h_068{0}pl.jpg"] = 0;      //mxgs
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/3{0}/3{0}pl.jpg"] = 0;              //wanz
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/57{0}/57{0}pl.jpg"] = 0;            //its
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_113{0}/h_113{0}pl.jpg"] = 0;      //bcp
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/2{0}/2{0}pl.jpg"] = 0;              //dfe
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_826{0}so/h_826{0}sopl.jpg"] = 0;  //avop
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/49{0}/49{0}pl.jpg"] = 0;            //ekdv
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/29{0}/29{0}pl.jpg"] = 0;            //gxaz
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_254{0}/h_254{0}pl.jpg"] = 0;      //hzgd
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_1107{0}/h_1107{0}pl.jpg"] = 0;    //knmd
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_1352{0}/h_1352{0}pl.jpg"] = 0;    //knmd
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_918{0}/h_918{0}pl.jpg"] = 0;      //kud
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_771{0}/h_771{0}pl.jpg"] = 0;      //ongp
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/18{0}/18{0}pl.jpg"] = 0;            //rtvn
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_244{0}/h_244{0}pl.jpg"] = 0;      //sama
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_635{0}/h_635{0}pl.jpg"] = 0;      //sw
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_910{0}/h_910{0}pl.jpg"] = 0;      //vrtm
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/1{0}/1{0}pl.jpg"] = 0;              //dandy
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/41{0}/41{0}pl.jpg"] = 0;            //hodv
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/24{0}/24{0}pl.jpg"] = 0;            //bld
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_1359{0}/h_1359{0}pl.jpg"] = 0;    //omt
            urlMap["https://pics.dmm.co.jp/mono/movie/adult/h_086{0}/h_086{0}pl.jpg"] = 2;      //ypaa,zeaa
            urlMap["https://pics.dmm.co.jp/digital/video/{0}/{0}pl.jpg"] = 3;                   //vrkm
            urlMap["https://image.mgstage.com/images/moonforce/{1}/{2}/pb_e_{0}.jpg"] = 1;      //435mfc
            urlMap["https://image.mgstage.com/images/prestigepremium/{1}/{2}/pb_e_{0}.jpg"] = 1;//300MAAN
            urlMap["https://image.mgstage.com/images/sukekiyo/{1}/{2}/pb_e_{0}.jpg"] = 1;       //428SUKE
            urlMap["https://image.mgstage.com/images/jackson/{1}/{2}/pb_e_{0}.jpg"] = 1;        //390jac
            urlMap["https://image.mgstage.com/images/luxutv/{1}/{2}/pb_e_{0}.jpg"] = 1;         //259luxu
            urlMap["https://image.mgstage.com/images/shiroutomanman/{1}/{2}/pb_e_{0}.jpg"] = 1; //345simm

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
