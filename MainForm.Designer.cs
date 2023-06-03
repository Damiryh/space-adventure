/*
 * Created by SharpDevelop.
 * User: damir
 * Date: 14.09.2021
 * Time: 17:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SpaceAdventure
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.CanvasFrame = new System.Windows.Forms.Panel();
			this.ticker = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// CanvasFrame
			// 
			this.CanvasFrame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CanvasFrame.Location = new System.Drawing.Point(0, 0);
			this.CanvasFrame.Margin = new System.Windows.Forms.Padding(0);
			this.CanvasFrame.Name = "CanvasFrame";
			this.CanvasFrame.Size = new System.Drawing.Size(794, 560);
			this.CanvasFrame.TabIndex = 0;
			// 
			// ticker
			// 
			this.ticker.Interval = 20;
			this.ticker.Tick += new System.EventHandler(this.TickerTick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(794, 560);
			this.Controls.Add(this.CanvasFrame);
			this.Name = "MainForm";
			this.Text = "SpaceAdventure";
			this.ResizeBegin += new System.EventHandler(this.MainFormResizeBegin);
			this.ResizeEnd += new System.EventHandler(this.MainFormResizeEnd);
			this.Enter += new System.EventHandler(this.MainFormEnter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFormKeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainFormKeyUp);
			this.Leave += new System.EventHandler(this.MainFormLeave);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Timer ticker;
		private System.Windows.Forms.Panel CanvasFrame;
	}
}
