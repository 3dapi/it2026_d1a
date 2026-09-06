// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

static class G2Util
{
	public static string FindFilePath(string filePath)
	{
		if (System.IO.Path.IsPathRooted(filePath))
		{
			if (File.Exists(filePath))
			{
				return System.IO.Path.GetFullPath(filePath);
			}
			throw new FileNotFoundException($"파일을 찾을 수 없습니다: {filePath}");
		}
		string path = System.IO.Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, filePath));
		if (System.IO.File.Exists(path))
		{
			return path;
		}
		path = System.IO.Path.GetFullPath(filePath);
		if (System.IO.File.Exists(path))
		{
			return path;
		}
		System.IO.DirectoryInfo? directory = new(System.AppContext.BaseDirectory);
		while (directory != null)
		{
			if (directory.GetFiles("*.csproj").Length > 0)
			{
				path = System.IO.Path.GetFullPath(System.IO.Path.Combine(directory.FullName, filePath));
				if (File.Exists(path))
				{
					return path;
				}
				break;
			}
			directory = directory.Parent;
		}
		throw new FileNotFoundException($"파일을 찾을 수 없습니다: {filePath}");
	}
}
