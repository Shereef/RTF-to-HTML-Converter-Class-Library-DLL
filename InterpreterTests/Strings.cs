// -- FILE ------------------------------------------------------------------
// name       : CodeResources.cs
// project    : RTF Framelet
// created    : Leon Poyyayil - 2008.05.19
// language   : c#
// environment: .NET 2.0
// copyright  : (c) 2004-2010 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Resources;
using Itenso.Sys;

namespace Itenso.Rtf.InterpreterTests
{

	// ------------------------------------------------------------------------
	/// <summary>Provides strongly typed resource access for this namespace.</summary>
	internal sealed class Strings : StringsBase
	{

		// ----------------------------------------------------------------------
		public static ResourceManager Instance
		{
			get { return inst; }
		} // Instance

		// ----------------------------------------------------------------------
		public static string ProgramPressAnyKeyToQuit
		{
			get { return inst.GetString( "ProgramPressAnyKeyToQuit" ); }
		} // ProgramPressAnyKeyToQuit

		// ----------------------------------------------------------------------
		// members
		private static readonly ResourceManager inst = NewInst( typeof( Strings ) );

	} // class CodeResources

} // namespace Itenso.Rtf.InterpreterTests
// -- EOF -------------------------------------------------------------------
