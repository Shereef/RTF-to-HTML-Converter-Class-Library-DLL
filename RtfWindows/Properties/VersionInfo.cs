// -- FILE ------------------------------------------------------------------
// name       : VersionInfo.cs
// project    : RTF Framelet
// created    : Jani Giannoudis - 2008.07.29
// language   : c#
// environment: .NET 2.0
// copyright  : (c) 2004-2010 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Reflection;

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
[assembly: AssemblyVersion( "1.3.0.0" )]

namespace Itenso.Solutions.Community.RtfConverter.RtfWindows
{

	// ------------------------------------------------------------------------
	public sealed class VersionInfo
	{

		/// <value>Provides easy access to the assemblies version as a string.</value>
		public static readonly string AssemblyVersion = typeof( VersionInfo ).Assembly.GetName().Version.ToString();

	} // class VersionInfo

} // namespace Itenso.Solutions.Community.RtfConverter.RtfWindows
// -- EOF -------------------------------------------------------------------
