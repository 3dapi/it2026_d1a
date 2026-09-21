// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

#define ACTIVE_GLC2DLIB

internal static class AppMain
{
#if ACTIVE_GLC2DLIB

	[STAThread]
	private static void Main()
	{
		ApplicationConfiguration.Initialize();

		using GameMain app = new();
		app.Run();
	}
#endif
}
