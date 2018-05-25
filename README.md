VS2017MSBuildAutoVersioning
===========================

As the name suggests.. bring back the asterisks!!

## Usage

**1)** Install `VS2017MSBuildAutoVersioning` [via NuGet](https://www.nuget.org/packages/VS2017MSBuildAutoVersioning).

**2)** Append a line like `<BaseVersion>x.x.x.x-suffix<BaseVersion>` to the `PropertyGroup` of your `*proj` file.

Accepted formats are: `<int>.<int>`, `<int>.<int>.*`, `<int>.<int>.*.*`, `<int>.<int>.*.*-<suffix>`.
 
For example, `1.2`, `1.2.*`, and `1.2.*.*` yield the same result and `1.2.*.*-alpha` appends the suffix to it.

During build, the build/revision version will be automatically generated in the same manner MSBuild had been doing; the former is the number of days since y2k, and the latter is the number of seconds since midnight, divided by 2.

*Only leaving the revision number wildcarded is possible and will work expectedly, but generally not recommended.*

**3)** `Version`, `AssemblyVersion`, `FileVersion` and `PackageVersion` are now set automatically and appropriately, so you can forget bumping them. Yay!

## Platforms

The new MSBuild format (introduced in VS2017), net46 or netstandard2.0. Should work regardless of OS.

## License

Apache 2.0. See LICENSE.txt.

