﻿// -- FILE ------------------------------------------------------------------
// name       : RtfXmlConverter.cs
// project    : RTF Framelet
// created    : Jani Giannoudis - 2008.06.10
// language   : c#
// environment: .NET 2.0
// copyright  : (c) 2004-2010 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Xml;
using Itenso.Rtf.Support;

namespace Itenso.Rtf.Converter.Xml
{

	// ------------------------------------------------------------------------
	public class RtfXmlConverter : RtfVisualVisitorBase
	{

		// ----------------------------------------------------------------------
		public const string DefaultXmlFileExtension = ".xml";

		// ----------------------------------------------------------------------
		public RtfXmlConverter( IRtfDocument rtfDocument, XmlWriter writer )
			: this( rtfDocument, writer, new RtfXmlConvertSettings() )
		{
		} // RtfXmlConverter

		// ----------------------------------------------------------------------
		public RtfXmlConverter( IRtfDocument rtfDocument, XmlWriter writer, RtfXmlConvertSettings settings )
		{
			if ( rtfDocument == null )
			{
				throw new ArgumentNullException( "rtfDocument" );
			}
			if ( writer == null )
			{
				throw new ArgumentNullException( "writer" );
			}
			if ( settings == null )
			{
				throw new ArgumentNullException( "settings" );
			}

			this.rtfDocument = rtfDocument;
			this.writer = writer;
			this.settings = settings;
		} // RtfXmlConverter

		// ----------------------------------------------------------------------
		public IRtfDocument RtfDocument
		{
			get { return rtfDocument; }
		} // RtfDocument

		// ----------------------------------------------------------------------
		public XmlWriter Writer
		{
			get { return writer; }
		} // Writer

		// ----------------------------------------------------------------------
		public RtfXmlConvertSettings Settings
		{
			get { return settings; }
		} // Settings

		// ----------------------------------------------------------------------
		public void Convert()
		{
			WriteStartElement( "rtfVisuals" );

			foreach ( IRtfVisual visual in rtfDocument.VisualContent )
			{
				visual.Visit( this );
			}

			WriteEndElement();
		} // Convert

		// ----------------------------------------------------------------------
		protected override void DoVisitText( IRtfVisualText visualText )
		{
			// suppress hidden text
			if ( visualText.Format.IsHidden && settings.IsShowHiddenText == false )
			{
				return;
			}

			WriteStartElement( "rtfVisualText" );

			WriteStartElement( "format" );
			WriteElementString( "fontSize", visualText.Format.FontSize.ToString() );
			WriteColor( "backgroundColor", visualText.Format.BackgroundColor );
			WriteColor( "foregroundColor", visualText.Format.ForegroundColor );
			WriteElementString( "alignment", visualText.Format.Alignment.ToString() );
			WriteElementString( "superScript", visualText.Format.SuperScript.ToString() );
			WriteElementString( "isBold", visualText.Format.IsBold.ToString() );
			WriteElementString( "isItalic", visualText.Format.IsItalic.ToString() );
			WriteElementString( "isStrikeThrough", visualText.Format.IsStrikeThrough.ToString() );
			WriteElementString( "isUnderline", visualText.Format.IsUnderline.ToString() );
			WriteEndElement();

			WriteStartElement( "font" );
			WriteElementString( "id", visualText.Format.Font.Id );
			WriteElementString( "kind", visualText.Format.Font.Kind.ToString() );
			WriteElementString( "name", visualText.Format.Font.Name );
			WriteElementString( "charSet", visualText.Format.Font.CharSet.ToString() );
			WriteElementString( "codePage", visualText.Format.Font.CodePage.ToString() );
			WriteElementString( "pitch", visualText.Format.Font.Pitch.ToString() );
			WriteEndElement();

			WriteElementString( "text", visualText.Text );
			WriteEndElement();
		} // DoVisitText

		// ----------------------------------------------------------------------
		protected override void DoVisitImage( IRtfVisualImage visualImage )
		{
			WriteStartElement( "rtfVisualImage" );

			WriteElementString( "format", visualImage.Format.ToString() );
			WriteElementString( "width", visualImage.Width.ToString() );
			WriteElementString( "height", visualImage.Height.ToString() );
			WriteElementString( "desiredWidth", visualImage.DesiredWidth.ToString() );
			WriteElementString( "desiredHeight", visualImage.DesiredHeight.ToString() );
			WriteElementString( "scaleWidthPercent", visualImage.ScaleWidthPercent.ToString() );
			WriteElementString( "scaleHeightPercent", visualImage.ScaleHeightPercent.ToString() );
			WriteElementString( "alignment", visualImage.Alignment.ToString() );

			WriteElementString( "image", visualImage.ImageDataHex );

			WriteEndElement();
		} // DoVisitImage

		// ----------------------------------------------------------------------
		protected override void DoVisitSpecial( IRtfVisualSpecialChar visualSpecialChar )
		{
			WriteStartElement( "rtfVisualSpecialChar" );

			WriteElementString( "charKind", visualSpecialChar.CharKind.ToString() );

			WriteEndElement();
		} // DoVisitSpecial

		// ----------------------------------------------------------------------
		protected override void DoVisitBreak( IRtfVisualBreak visualBreak )
		{
			WriteStartElement( "rtfVisualBreak" );

			WriteElementString( "breakKind", visualBreak.BreakKind.ToString() );

			WriteEndElement();
		} // DoVisitBreak

		// ----------------------------------------------------------------------
		private void WriteColor( string name, IRtfColor color )
		{
			WriteStartElement( name );
			WriteElementString( "red", color.Red.ToString() );
			WriteElementString( "green", color.Green.ToString() );
			WriteElementString( "blue", color.Blue.ToString() );
			WriteEndElement();
		} // WriteColor

		// ----------------------------------------------------------------------
		private void WriteStartElement( string localName )
		{
			if ( string.IsNullOrEmpty( settings.Prefix ) && string.IsNullOrEmpty( settings.Ns ) )
			{
				writer.WriteStartElement( localName );
				return;
			}

			if ( string.IsNullOrEmpty( settings.Prefix ) )
			{
				writer.WriteStartElement( localName, settings.Ns );
				return;
			}

			writer.WriteStartElement( settings.Prefix, localName, settings.Ns );
		} // WriteStartElement

		// ----------------------------------------------------------------------
		private void WriteElementString( string localName, string value )
		{
			if ( string.IsNullOrEmpty( settings.Prefix ) && string.IsNullOrEmpty( settings.Ns ) )
			{
				writer.WriteElementString( localName, value );
				return;
			}

			if ( string.IsNullOrEmpty( settings.Prefix ) )
			{
				writer.WriteElementString( localName, settings.Ns, value );
				return;
			}

			writer.WriteElementString( settings.Prefix, localName, settings.Ns, value );
		} // WriteStartElement

		// ----------------------------------------------------------------------
		private void WriteEndElement()
		{
			writer.WriteEndElement();
		} // WriteEndElement

		// ----------------------------------------------------------------------
		// members
		private readonly IRtfDocument rtfDocument;
		private readonly XmlWriter writer;
		private readonly RtfXmlConvertSettings settings;

	} // class RtfXmlConverter

} // namespace Itenso.Rtf.Converter.Xml
// -- EOF -------------------------------------------------------------------
