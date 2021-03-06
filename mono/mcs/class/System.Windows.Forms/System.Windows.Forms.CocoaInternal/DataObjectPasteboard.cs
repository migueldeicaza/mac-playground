﻿using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Mac;
using System.Windows.Forms.Extensions.IO;
using System.Windows.Forms.CocoaInternal;

#if XAMARINMAC
using Foundation;
using AppKit;
#elif MONOMAC
using MonoMac.Foundation;
using MonoMac.AppKit;
#endif

namespace System.Windows.Forms.CocoaInternal
{
	internal class DataObjectPasteboard : IDataObject
	{
		NSPasteboard pboard;

		public DataObjectPasteboard(NSPasteboard pboard)
		{
			this.pboard = pboard;
		}

		#region IDataObject

		public object GetData(string format)
		{
			return GetData(format, pboard, true);
		}

		public object GetData(string format, bool autoConvert)
		{
			return GetData(format, pboard, true);
		}

		public object GetData(Type format)
		{
			return GetData(format.FullName, pboard, true);
		}

		public bool GetDataPresent(string format)
		{
			return -1 != Array.IndexOf(GetFormats(), format);
		}

		public bool GetDataPresent(string format, bool autoConvert)
		{
			return -1 != Array.IndexOf(GetFormats(), format);
		}

		public bool GetDataPresent(Type format)
		{
			return false;
		}

		public string[] GetFormats()
		{
			return GetFormats(pboard);
		}

		public string[] GetFormats(bool autoConvert)
		{
			return GetFormats(pboard);
		}

		public void SetData(object data)
		{
		}

		public void SetData(string format, bool autoConvert, object data)
		{
		}

		public void SetData(string format, object data)
		{
		}

		public void SetData(Type format, object data)
		{
		}

		#endregion

		#region internals

		internal static string[] GetFormats(NSPasteboard pboard)
		{
			var types = new List<string>();
			foreach(var type in pboard.Types)
			{
				switch (type)
				{
					case Pasteboard.NSPasteboardTypeText:
						types.Add(DataFormats.Text);
						types.Add(DataFormats.UnicodeText);
						break;
					case Pasteboard.NSPasteboardTypeURL:
						if (Uri.TryCreate(pboard.GetStringForType(type), UriKind.Absolute, out Uri uri))
						    types.Add(Pasteboard.UniformResourceLocatorW);
						break;
					case Pasteboard.NSPasteboardTypeHTML:
						types.Add(DataFormats.Html);
						break;
					case Pasteboard.NSPasteboardTypeRTF:
						types.Add(DataFormats.Rtf);
						break;
					case Pasteboard.NSPasteboardTypePNG:
					case Pasteboard.NSPasteboardTypeTIFF:
						types.Add(DataFormats.Bitmap);
						break;
				}
			}

			return types.ToArray();
		}

		internal object GetData(string format, NSPasteboard pboard, bool autoConvert)
		{
			switch (format)
			{
				case DataFormats.Text:
				case DataFormats.UnicodeText:
					return pboard.GetStringForType(Pasteboard.NSPasteboardTypeText);
				case DataFormats.Rtf:
					return GetRtf(pboard);
				case DataFormats.Html:
					return GetHtml(pboard);
				case DataFormats.HtmlStream:
					return GetHtml(pboard)?.ToStream(Encoding.UTF8);
				case Pasteboard.UniformResourceLocatorW:
					return GetUri(pboard);
				case DataFormats.Bitmap:
					return GetBitmap(pboard);
			}

			return null;
		}

		internal Uri GetUri(NSPasteboard pboard)
		{
			if (Uri.TryCreate(pboard.GetStringForType(Pasteboard.NSPasteboardTypeText), UriKind.Absolute, out Uri uri))
				return uri;

			return null;
		}

		internal string GetRtf(NSPasteboard pboard)
		{
			var data = pboard.GetDataForType(Pasteboard.NSPasteboardTypeRTF);
			if (data != null)
				return NSString.FromData(data, NSStringEncoding.ASCIIStringEncoding)?.ToString();

			return null;
		}

		internal Image GetBitmap(NSPasteboard pboard)
		{
			var cgimage = new NSImage(pboard)?.CGImage;
			if (cgimage != null)
				return cgimage.ToBitmap();

			return null;
		}

		internal string GetHtml(NSPasteboard pboard)
		{
			string html = pboard.GetStringForType(Pasteboard.NSPasteboardTypeHTML);

			if (html != null)
				return HtmlClip.AddMetadata(html);

			return null;
		}

		#endregion
	}
}
