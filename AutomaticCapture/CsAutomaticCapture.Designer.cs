namespace AutomaticCapture
{
    partial class CsAutomaticCapture
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.CvCapturePictureBox = new System.Windows.Forms.PictureBox();
            this.CvSendButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CvCapturePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CvCapturePictureBox
            // 
            this.CvCapturePictureBox.Location = new System.Drawing.Point(12, 12);
            this.CvCapturePictureBox.Name = "CvCapturePictureBox";
            this.CvCapturePictureBox.Size = new System.Drawing.Size(640, 480);
            this.CvCapturePictureBox.TabIndex = 0;
            this.CvCapturePictureBox.TabStop = false;
            // 
            // CvSendButton
            // 
            this.CvSendButton.Location = new System.Drawing.Point(656, 12);
            this.CvSendButton.Name = "CvSendButton";
            this.CvSendButton.Size = new System.Drawing.Size(93, 39);
            this.CvSendButton.TabIndex = 1;
            this.CvSendButton.Text = "SEND";
            this.CvSendButton.UseVisualStyleBackColor = true;
            this.CvSendButton.Click += new System.EventHandler(this.CvSendButton_Click);
            // 
            // CsAutomaticCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 502);
            this.Controls.Add(this.CvSendButton);
            this.Controls.Add(this.CvCapturePictureBox);
            this.KeyPreview = true;
            this.Name = "CsAutomaticCapture";
            this.Text = "AutomaticCapture";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CsAutomaticCapture_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.CvCapturePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox CvCapturePictureBox;
        private System.Windows.Forms.Button CvSendButton;
    }
}

