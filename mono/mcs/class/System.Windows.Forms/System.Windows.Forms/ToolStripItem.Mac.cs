﻿using System;
using System.Drawing.Mac;
#if XAMARINMAC
using AppKit;
#else
using MonoMac.AppKit;
#endif

namespace System.Windows.Forms
{
	public partial class ToolStripItem
	{
		protected internal virtual NSMenuItem ToNSMenuItem()
		{
			var nsMenuItem = new NSMenuItem();
			if (DisplayStyle == ToolStripItemDisplayStyle.ImageAndText || DisplayStyle == ToolStripItemDisplayStyle.Text)
				nsMenuItem.Title = (Text ?? String.Empty).Replace("&", "");
			nsMenuItem.Enabled = Enabled;
			nsMenuItem.Hidden = !InternalVisible;
			if (Image != null && (DisplayStyle == ToolStripItemDisplayStyle.ImageAndText || DisplayStyle == ToolStripItemDisplayStyle.Image))
			{
				var nsImage = Image.ToNSImage();
				if (Image.Size.Width > 16)
					nsImage.Size = new CoreGraphics.CGSize(nsImage.Size.Width / 2, nsImage.Size.Height / 2);
				nsMenuItem.Image = nsImage;
			}
			// TODO: Checked, submenu
			nsMenuItem.Activated += (sender, e) => this.OnClick(e);
			return nsMenuItem;
		}
	}
}
