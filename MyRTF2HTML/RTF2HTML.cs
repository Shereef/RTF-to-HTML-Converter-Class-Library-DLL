using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Itenso.Rtf.Interpreter;
using Itenso.Rtf.Converter.Image;
using System.Drawing.Imaging;
using Itenso.Rtf;
using Itenso.Rtf.Support;
using Itenso.Rtf.Converter.Html;
using Itenso.Rtf.Parser;

namespace RTF2HTML
{
	public class RTF2HTML
	{
		#region Methods (2) 

		#region A to F (1) 

		public static string ConvertRtf2Html(StreamReader rtfStream)
		{
			// image converter
			// convert all images to JPG
			RtfVisualImageAdapter imageAdapter =
			   new RtfVisualImageAdapter(ImageFormat.Jpeg);
			RtfImageConvertSettings imageConvertSettings =
						   new RtfImageConvertSettings(imageAdapter);
			imageConvertSettings.ScaleImage = true; // scale images
			RtfImageConverter imageConverter =
					new RtfImageConverter(imageConvertSettings);

			RtfParserListenerStructureBuilder structureBuilder = new RtfParserListenerStructureBuilder();
			RtfParser parser = new RtfParser(structureBuilder);
			IRtfGroup rtfStructure;
			parser.IgnoreContentAfterRootGroup = true; // support WordPad documents
			parser.Parse(new RtfSource(rtfStream));
			rtfStructure = structureBuilder.StructureRoot;
			// interpreter
			IRtfDocument rtfDocument = InterpretRtf(rtfStructure);

			// html converter
			RtfHtmlConvertSettings htmlConvertSettings =
				   new RtfHtmlConvertSettings(imageAdapter);
			htmlConvertSettings.StyleSheetLinks.Add("default.css");
			RtfHtmlConverter htmlConverter = new RtfHtmlConverter(rtfDocument,
														 htmlConvertSettings);
			return htmlConverter.Convert();
		}

		#endregion A to F 
		#region G to L (1) 

		private static IRtfDocument InterpretRtf(IRtfGroup rtfStructure)
		{
			IRtfDocument rtfDocument;
			RtfInterpreterListenerFileLogger interpreterLogger = null;
			try
			{
				// image converter
				RtfImageConverter imageConverter = null;
				// rtf parser
				// interpret the rtf structure using the extractors
				rtfDocument = RtfInterpreterTool.BuildDoc(rtfStructure, interpreterLogger, imageConverter);

			}
			catch (Exception e)
			{
				if (interpreterLogger != null)
				{
					interpreterLogger.Dispose();
				}

				Console.WriteLine("error while interpreting rtf: " + e.Message);
				return null;
			}

			return rtfDocument;
		}

		#endregion G to L 

		#endregion Methods 
	}
}
