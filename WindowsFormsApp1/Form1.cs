using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string data = string.Empty;
            using (var httpClient = new HttpClient())
            {
                //url
                var url = new Uri("http://192.168.1.14:8088/chart/getq195");
                //设置webapi的用户名和密码
                // response
                var response = httpClient.GetAsync(url).Result;
                 data = response.Content.ReadAsStringAsync().Result;
              
            }

            // 设置曲线的样式
            Series series = chart1.Series[0];
            // 画样条曲线（Spline）
            series.ChartType = SeriesChartType.Spline;
            // 线宽2个像素
            series.BorderWidth = 3;
            // 线的颜色：红色
            series.Color = System.Drawing.Color.Red;
            // 图示上的文字
            series.LegendText = "Q195";

            // 准备数据

            IList<Q195> jo = JSONStringToList<Q195>(data);
            // 在chart中显示数据
            foreach (var v in jo)
            {
                series.Points.AddXY(v.PublishDate, v.Price);
            }

            // 设置显示范围
            ChartArea chartArea = chart1.ChartAreas[0];

            chartArea.AxisY.Minimum = 2500;
   
        }

        /// <summary>
        /// JSON格式数组转化为对应的List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonStr">JSON格式数组</param>
        /// <returns></returns>
        public static List<T> JSONStringToList<T>(string JsonStr)
        {
            //设置转化JSON格式时字段长度
            List<T> objs = JsonConvert.DeserializeObject<List<T>>(JsonStr);
            return objs;
        }
    }

    public class Q195
    {
        public string PublishDate { get; set; }
        public double Price { get; set; }
    }
}
