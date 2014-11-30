using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAO.Firebird.Providers;
using DAO.Firebird.Entities;

namespace FirebirdHelper
{
    public partial class MainForm : Form
    {
        private Timer _timer;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(timer_Tick);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            FillGrids();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            StartTimer();
            FillGrids();
        }

        private void StartTimer()
        {
            _timer.Start();
        }

        private void FillGrids()
        {
            DateTime d = DateTime.Now;

            int firstDisplayedRowReplicas = gvReplicas.FirstDisplayedScrollingRowIndex;
            int firstDisplayedRowDocumentsLogs = gvDocumentsLog.FirstDisplayedScrollingRowIndex;
            int firstDisplayedRowSendBlockLogs = gvSendBlockLog.FirstDisplayedScrollingRowIndex;

            if (ReplicasProvider.Count != gvReplicas.Rows.Count)
            {
                gvReplicas.DataSource = ReplicasProvider.GetList();
            }
            if (DocumentsLogProvider.Count != gvDocumentsLog.Rows.Count)
            {
                gvDocumentsLog.DataSource = DocumentsLogProvider.GetList();
            }
            if (SendBlockLogProvider.Count != gvSendBlockLog.Rows.Count)
            {
                gvSendBlockLog.DataSource = SendBlockLogProvider.GetList();
            }

            if (firstDisplayedRowReplicas != -1)
            {
                gvReplicas.FirstDisplayedScrollingRowIndex = firstDisplayedRowReplicas;
            }
            if (firstDisplayedRowDocumentsLogs != -1)
            {
                gvDocumentsLog.FirstDisplayedScrollingRowIndex = firstDisplayedRowDocumentsLogs;
            }
            if (firstDisplayedRowSendBlockLogs != -1)
            {
                gvSendBlockLog.FirstDisplayedScrollingRowIndex = firstDisplayedRowSendBlockLogs;
            }

            TimeSpan s = DateTime.Now - d;
            //MessageBox.Show(s.ToString());
        }
    }
}
