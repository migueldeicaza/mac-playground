﻿using System;
using System.Drawing.Mac;
#if XAMARINMAC
using AppKit;
#else
using MonoMac.AppKit;
#endif

namespace System.Windows.Forms
{
	public partial class Button : IMacNativeControl
	{
		NSButton button;

		public NSView CreateView()
		{
			button = new NSButton();
			/*button.AttributedTitle = GetAttributedString(Text, '&', b.font, b.TextAlign);
			button.Alignment = TextAlign.ToNSTextAlignment();
			cell.Highlighted = b.ButtonState == ButtonState.Pushed;
			cell.Bezeled = false;
			cell.Bordered = true;
			cell.BezelStyle = NSBezelStyle.Rounded; // When Rounded is set, the button border gets its own dimensions (smaller than we want)
			cell.Enabled = b.Enabled;
			cell.isFocused = b.Focused;*/
			button.BezelStyle = NSBezelStyle.Rounded;
			button.AttributedTitle = ThemeMacOS.GetAttributedString(Text, '&', Font, TextAlign);
			button.Alignment = TextAlign.ToNSTextAlignment();
			button.Activated += (sender, e) => PerformClick();
			button.Enabled = Enabled;

			return button;
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				if (button != null)
					button.AttributedTitle = ThemeMacOS.GetAttributedString(value, '&', Font, TextAlign);
			}
		}

		public override Drawing.Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				if (button != null)
					button.AttributedTitle = ThemeMacOS.GetAttributedString(Text, '&', value, TextAlign);
			}
		}

		public override Drawing.ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
				if (button != null)
				{
					button.AttributedTitle = ThemeMacOS.GetAttributedString(Text, '&', Font, value);
					button.Alignment = value.ToNSTextAlignment();
				}
			}
		}

		protected override void OnEnabledChanged(EventArgs e)
		{
			if (button != null)
				button.Enabled = Enabled;
			base.OnEnabledChanged(e);
		}

#if XAMARINMAC
		public override Drawing.Size GetPreferredSize(Drawing.Size proposedSize)
		{
			if (this.AutoSize)
			{
				var b = this.button ?? CreateView();
				return ((NSControl)b).SizeThatFits(proposedSize.ToCGSize()).ToSDSize();
			}
			return base.GetPreferredSize(proposedSize);
		}
#endif
	}
}
