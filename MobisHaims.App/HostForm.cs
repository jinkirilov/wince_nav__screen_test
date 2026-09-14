using System;
using System.Drawing;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;

namespace HaimsPda
{
    /// <summary>
    /// 호스트(서버 주소) 설정 화면. 로그인 화면의 [호스트] 버튼에서 연다.
    ///
    /// 저장하면 prefs.txt 에 남아 다음 실행에도 유지된다.
    /// 접속확인은 fn_Init 을 실제로 호출해 서버명(_ServerName)을 받아 본다.
    /// </summary>
    public partial class HostForm : Form
    {
        private static readonly Color Ok = Color.FromArgb(0x7E, 0xE0, 0xA0);
        private static readonly Color Ng = Color.FromArgb(0xFF, 0xB0, 0xA0);
        private static readonly Color Normal = Color.FromArgb(0xD4, 0xDA, 0xE0);

        // 접속확인 때문에 통신 설정을 임시로 바꾸므로, 취소 시 되돌릴 원래 값을 들고 있는다.
        private readonly string _orgUrl;
        private readonly int _orgTimeoutSec;
        private bool _busy;

        public HostForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            _orgUrl = HostConfig.Url;
            _orgTimeoutSec = HostConfig.TimeoutSec;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Ime.AttachAll(this);          // 주소 입력은 영문

            txtUrl.Text = _orgUrl;
            txtTimeout.Text = _orgTimeoutSec.ToString();

            txtUrl.Focus();
            txtUrl.SelectAll();
        }

        #region 버튼

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtUrl.Text = HostConfig.DefaultUrl;
            txtTimeout.Text = HostConfig.DefaultTimeoutSec.ToString();
            SetResult("기본값으로 되돌렸습니다. 저장을 눌러야 적용됩니다.", Normal);
            txtUrl.Focus();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (_busy) return;

            string url;
            int sec;
            if (!ReadInput(out url, out sec)) return;

            // 확인하는 동안만 이 값으로 통신한다. 저장은 별도.
            HostConfig.Apply(url, sec);

            SetBusy(true);
            SetResult("접속 확인 중...\r\n" + url, Normal);

            Async.Run(this,
                delegate
                {
                    return AuthService.GetServerName();
                },
                delegate(object result, Exception error)
                {
                    SetBusy(false);

                    if (error != null)
                    {
                        SetResult("접속 실패\r\n" + ShortError(error), Ng);
                        return;
                    }

                    string name = (result == null) ? "" : ((string)result).Trim();
                    if (name.Length == 0)
                    {
                        // 응답은 왔는데 _ServerName 이 비었다 = 주소는 붙었지만 HAIMS 서버가 아닐 수 있다
                        SetResult("응답은 받았지만 서버명이 비어 있습니다.\r\n주소를 다시 확인하십시오.", Ng);
                        return;
                    }

                    SetResult("접속 성공\r\n서버명 : " + name, Ok);
                });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_busy) return;

            string url;
            int sec;
            if (!ReadInput(out url, out sec)) return;

            if (HostConfig.IsHttps(url))
            {
                // CF 3.5 는 TLS 1.2 를 못 해서 https 는 핸드셰이크에서 죽는다
                if (MessageBox.Show(
                        "https 는 이 장비에서 접속되지 않을 수 있습니다.\r\n그래도 저장하시겠습니까?",
                        "호스트 설정", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    txtUrl.Focus();
                    return;
                }
            }

            HostConfig.Save(url, sec);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_busy) return;

            // 접속확인으로 바꿔 둔 통신 설정을 원래대로 되돌린다
            HostConfig.Apply(_orgUrl, _orgTimeoutSec);
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region 입력 검증

        private bool ReadInput(out string url, out int sec)
        {
            url = "";
            sec = HostConfig.DefaultTimeoutSec;

            string raw = txtUrl.Text.Trim();
            if (raw.Length == 0)
            {
                MessageBox.Show("서버 주소를 입력하십시오.");
                txtUrl.Focus();
                return false;
            }

            url = HostConfig.Normalize(raw);
            txtUrl.Text = url;            // 보정 결과를 눈으로 확인시킨다

            string t = txtTimeout.Text.Trim();
            if (t.Length == 0)
            {
                MessageBox.Show("타임아웃을 입력하십시오.");
                txtTimeout.Focus();
                return false;
            }

            try { sec = int.Parse(t); }
            catch
            {
                MessageBox.Show("타임아웃은 숫자만 입력하십시오.");
                txtTimeout.Focus();
                txtTimeout.SelectAll();
                return false;
            }

            if (sec < HostConfig.MinTimeoutSec || sec > HostConfig.MaxTimeoutSec)
            {
                MessageBox.Show("타임아웃은 " + HostConfig.MinTimeoutSec
                              + " ~ " + HostConfig.MaxTimeoutSec + "초 사이로 입력하십시오.");
                txtTimeout.Focus();
                txtTimeout.SelectAll();
                return false;
            }

            return true;
        }

        private static string ShortError(Exception ex)
        {
            if (ex is System.Net.WebException) return "서버에 연결할 수 없습니다.";
            return ex.Message;
        }

        #endregion

        private void SetResult(string text, Color color)
        {
            lblResult.ForeColor = color;
            lblResult.Text = text;
            lblResult.Invalidate();   // CF 는 색만 바뀔 때 다시 그려주지 않는다
        }

        private void SetBusy(bool busy)
        {
            _busy = busy;
            txtUrl.Enabled = !busy;
            txtTimeout.Enabled = !busy;
            btnTest.Enabled = !busy;
            btnDefault.Enabled = !busy;
            btnSave.Enabled = !busy;
            btnCancel.Enabled = !busy;
            btnTest.Text = busy ? "확인중" : "접속확인";
            Cursor.Current = busy ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}
