properties {
  $base_dir = resolve-path .
  $build_dir = "$base_dir\Build"
  $buildartifacts_dir = "$build_dir\"
  $sln_file = "$base_dir\TimeServices.sln"
  $version = "1.0.0.0"
  $humanReadableversion = "1.0"
  $release_dir = "$base_dir\Release"
  $tools_dir = "$base_dir\Tools"
}

task default -depends Release

task Clean {
  remove-item -force -recurse $buildartifacts_dir -ErrorAction SilentlyContinue
  remove-item -force -recurse $release_dir -ErrorAction SilentlyContinue
}

task Init -depends Clean {
	new-item $release_dir -itemType directory
	new-item $buildartifacts_dir -itemType directory
}

task Compile -depends Init {
  & msbuild "$sln_file" "/p:OutDir=$build_dir\\" /p:Configuration=Release
  if ($lastExitCode -ne 0) {
        throw "Error: Failed to execute msbuild"
  }
}

task Test -depends Compile {
  $old = pwd
  cd $build_dir
  #exec "$tools_dir\xunit\xunit.console.exe" "$build_dir\Rhino.Security.Tests.dll"
  cd $old
}

task Release -depends Test {
	& $tools_dir\7zip\7za.exe a `
		$release_dir\TimeServices.Core-$humanReadableversion.7z `
		$build_dir\TimeServices.Core.dll `
		$build_dir\TimeServices.Core.xml `
		license.txt
	if ($lastExitCode -ne 0) {
		throw "Error: Failed to execute ZIP command"
	}
}