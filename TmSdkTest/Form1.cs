using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TaskManager.ApiSdk;
using TaskManager.ApiSdk.Factory;
using TaskManager.ApiSdk.Sdk;

namespace TmSdkTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sdk = this.CreateSdk();
            //var result = sdk.HeartBeat("0bcd7d9d-89ef-44a7-9e16-1d9445e928af");
            //this.textBox1.Text = result.Data.ToString();


            var xxx = sdk.DownloadTaskFile("94d9bc96-b6a3-49e2-b7ef-accbdf05ce72");
        }

        private ITmSdk CreateSdk()
        {
            return SdkFactory.CreateSdk(new SdkConfig("http://localhost/"));
        }
    }
}
