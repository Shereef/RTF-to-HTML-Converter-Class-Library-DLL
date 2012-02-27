// -- FILE ------------------------------------------------------------------
// name       : RtfHtmlConvertSettings.cs
// project    : RTF Framelet
// created    : Jani Giannoudis - 2008.06.02
// language   : c#
// environment: .NET 2.0
// copyright  : (c) 2004-2010 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Specialized;
using Itenso.Rtf.Converter.Image;

namespace Itenso.Rtf.Converter.Html
{

	// ------------------------------------------------------------------------
	public class RtfHtmlConvertSettings
	{

		// ----------------------------------------------------------------------
		public const string DefaultDocumentHeader =
			"<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//DE\" \"http://www.w3.org/TR/html4/loose.dtd\">";
		public const string DefaultDocumentCharacterSet = "UTF-8";

		// regex souce: http://msdn.microsoft.com/en-us/library/aa159903.aspx
		public const string DefaultVisualHyperlinkPattern =
			@"[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*";

		// ----------------------------------------------------------------------
		public RtfHtmlConvertSettings()
			: this( new RtfVisualImageAdapter(), RtfHtmlConvertScope.All )
		{
		} // RtfHtmlConvertSettings

		// ----------------------------------------------------------------------
		public RtfHtmlConvertSettings( RtfHtmlConvertScope convertScope )
			: this( new RtfVisualImageAdapter(), convertScope )
		{
		} // RtfHtmlConvertSettings

		// ----------------------------------------------------------------------
		public RtfHtmlConvertSettings( IRtfVisualImageAdapter imageAdapter )
			: this( imageAdapter, RtfHtmlConvertScope.All )
		{
		} // RtfHtmlConvertSettings

		// ----------------------------------------------------------------------
		public RtfHtmlConvertSettings( IRtfVisualImageAdapter imageAdapter, RtfHtmlConvertScope convertScope )
		{
			if ( imageAdapter == null )
			{
				throw new ArgumentNullException( "imageAdapter" );
			}

			this.imageAdapter = imageAdapter;
			this.convertScope = convertScope;
			visualHyperlinkPattern = DefaultVisualHyperlinkPattern;
		} // RtfHtmlConvertSettings

		// ----------------------------------------------------------------------
		public IRtfVisualImageAdapter ImageAdapter
		{
			get { return imageAdapter; }
		} // ImageAdapter

		// ----------------------------------------------------------------------
		public RtfHtmlConvertScope ConvertScope
		{
			get { return convertScope; }
			set { convertScope = value; }
		} // ConvertScope

		// ----------------------------------------------------------------------
		public bool HasStyles
		{
			get { return styles != null && styles.Count > 0; }
		} // HasStyles

		// ----------------------------------------------------------------------
		public RtfHtmlCssStyleCollection Styles
		{
			get { return styles ?? ( styles = new RtfHtmlCssStyleCollection() ); }
		} // Styles

		// ----------------------------------------------------------------------
		public bool HasStyleSheetLinks
		{
			get { return styleSheetLinks != null && styleSheetLinks.Count > 0; }
		} // HasStyleSheetLinks

		// ----------------------------------------------------------------------
		public StringCollection StyleSheetLinks
		{
			get { return styleSheetLinks ?? ( styleSheetLinks = new StringCollection() ); }
		} // StyleSheetLinks

		// ----------------------------------------------------------------------
		public string DocumentHeader
		{
			get { return documentHeader; }
			set { documentHeader = value; }
		} // DocumentHeader

		// ----------------------------------------------------------------------
		public string Title
		{
			get { return title; }
			set { title = value; }
		} // Title

		// ----------------------------------------------------------------------
		public string CharacterSet
		{
			get { return characterSet; }
			set { characterSet = value; }
		} // CharacterSet

		// ----------------------------------------------------------------------
		public string VisualHyperlinkPattern
		{
			get { return visualHyperlinkPattern; }
			set { visualHyperlinkPattern = value; }
		} // VisualHyperlinkPattern

		// ----------------------------------------------------------------------
		public string SpecialCharsRepresentation
		{
			get { return specialCharsRepresentation; }
			set { specialCharsRepresentation = value; }
		} // SpecialCharsRepresentation

		// ----------------------------------------------------------------------
		public bool IsShowHiddenText
		{
			get { return showHiddenText; }
			set { showHiddenText = value; }
		} // IsShowHiddenText

		// ----------------------------------------------------------------------
		public bool ConvertVisualHyperlinks
		{
			get { return convertVisualHyperlinks; }
			set { convertVisualHyperlinks = value; }
		} // ConvertVisualHyperlinks

		// ----------------------------------------------------------------------
		public bool UseNonBreakingSpaces
		{
			get { return useNonBreakingSpaces; }
			set { useNonBreakingSpaces = value; }
		} // UseNonBreakingSpaces

		// ----------------------------------------------------------------------
		public string ImagesPath
		{
			get { return imagesPath; }
			set { imagesPath = value; }
		} // ImagesPath

		// ----------------------------------------------------------------------
		public string GetImageUrl( int index, RtfVisualImageFormat rtfVisualImageFormat )
		{
			string imageFileName = imageAdapter.ResolveFileName( index, rtfVisualImageFormat );
			return imageFileName.Replace( '\\', '/' );
		} // GetImageUrl

		// ----------------------------------------------------------------------
		// members
		private readonly IRtfVisualImageAdapter imageAdapter;
		private RtfHtmlConvertScope convertScope;
		private RtfHtmlCssStyleCollection styles;
		private StringCollection styleSheetLinks;
		private string imagesPath;
		private string documentHeader = DefaultDocumentHeader;
		private string title;
		private string visualHyperlinkPattern;
		private string specialCharsRepresentation;
		private string characterSet = DefaultDocumentCharacterSet;
		private bool showHiddenText;
		private bool convertVisualHyperlinks;
		private bool useNonBreakingSpaces;

	} // class RtfHtmlConvertSettings

} // namespace Itenso.Rtf.Converter.Html
// -- EOF -------------------------------------------------------------------
