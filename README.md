[![Build status](https://ci.appveyor.com/api/projects/status/92rb6muqvqw5t6xf?svg=true)](https://ci.appveyor.com/project/GrumpyBusted/grumpy-common)
[![codecov](https://codecov.io/gh/GrumpyBusted/Grumpy.Common/branch/master/graph/badge.svg)](https://codecov.io/gh/GrumpyBusted/Grumpy.Common)
[![nuget](https://img.shields.io/nuget/v/Grumpy.Common.svg)](https://www.nuget.org/packages/Grumpy.Common/)
[![downloads](https://img.shields.io/nuget/dt/Grumpy.Common.svg)](https://www.nuget.org/packages/Grumpy.Common/)

# Grumpy.Common
Extension methods for string, object etc. used in my other projects, this NuGet package do not refer other Packages or
Libraries. This library includes code that I have developed, copied from the internet and copied from a few friends. The
intention of the library is to make coding easier in other projects.

See below for a taste of what this library contains.

## XML Serialization
```csharp
public class TestPerson
{
    public int Age { get; set; }
    public string Name { get; set; }
}

const string str = "<TestPerson><Age>8</Age><Name>Sara</Name></TestPerson>";

var obj = str.DeserializeFromXml<TestPerson>();

obj.Age.Should().Be(8);
obj.Name.Should().Be("Sara");

var xmlString = obj.SerializeToXml();
```

## In Function
```csharp
var i = 2;

bool res = i.In(1, 3, 5);
```